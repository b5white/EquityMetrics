using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json.Linq;
using EquityMetrics.Common;
using System.Globalization;

namespace EquityMetrics.DataAccess {

   ////////////////////////////////////////////////////////////////////////////
   /// <summary>
   ///   Defines common DataService routines for transaction management, 
   ///   stored procedure execution, parameter creation, and null value 
   ///   checking
   /// </summary>	
   ////////////////////////////////////////////////////////////////////////////
   public class DataServiceBase {

      ////////////////////////////////////////////////////////////////////////
      // Fields
      ////////////////////////////////////////////////////////////////////////
      private bool _isOwner = false;   //True if service owns the transaction        
      private SqlTransaction _txn;     //Reference to the current transaction


      ////////////////////////////////////////////////////////////////////////
      // Properties 
      ////////////////////////////////////////////////////////////////////////
      public IDbTransaction Txn {
         get { return (IDbTransaction)_txn; }
         set { _txn = (SqlTransaction)value; }
      }


      ////////////////////////////////////////////////////////////////////////
      // Constructors
      ////////////////////////////////////////////////////////////////////////

      public DataServiceBase() : this(null) { }


      public DataServiceBase(IDbTransaction txn) {
         if (txn == null) {
            _isOwner = true;
         } else {
            _txn = (SqlTransaction)txn;
            _isOwner = false;
         }
      }


      ////////////////////////////////////////////////////////////////////////
      // Connection and Transaction Methods
      ////////////////////////////////////////////////////////////////////////
      protected static string GetConnectionString() {
         string str = ConfigurationManager.ConnectionStrings["DB"].ConnectionString;
         return str;
      }


      public static IDbTransaction BeginTransaction() {
         SqlConnection txnConnection =
             new SqlConnection(GetConnectionString());
         txnConnection.Open();
         return txnConnection.BeginTransaction();
      }


      ////////////////////////////////////////////////////////////////////////
      // ExecuteDataSet Methods
      ////////////////////////////////////////////////////////////////////////
      protected DataSet ExecuteDataSet(string procName,
          params IDataParameter[] procParams) {
         SqlCommand cmd;
         return ExecuteDataSet(out cmd, procName, procParams);
      }


      protected DataSet ExecuteDataSet(out SqlCommand cmd, string procName,
          params IDataParameter[] procParams) {
         SqlConnection cnx = null;
         DataSet ds = new DataSet();
         SqlDataAdapter da = new SqlDataAdapter();
         cmd = null;

         try {
            //Setup command object
            cmd = new SqlCommand(procName);
            cmd.CommandType = CommandType.StoredProcedure;
            if (procParams != null) {
               for (int index = 0; index < procParams.Length; index++) {
                  cmd.Parameters.Add(procParams[index]);
               }
            }
            da.SelectCommand = (SqlCommand)cmd;

            //Determine the transaction owner and process accordingly
            if (_isOwner) {
               cnx = new SqlConnection(GetConnectionString());
               cmd.Connection = cnx;
               cnx.Open();
            } else {
               cmd.Connection = _txn.Connection;
               cmd.Transaction = _txn;
            }

            //Fill the dataset
            da.Fill(ds);
         } catch {
            throw;
         } finally {
            if (da != null) da.Dispose();
            if (cmd != null) cmd.Dispose();
            if (_isOwner) {
               cnx.Dispose(); //Implicitly calls cnx.Close()
            }
         }
         return ds;
      }


      ////////////////////////////////////////////////////////////////////////
      // ExecuteNonQuery Methods
      ////////////////////////////////////////////////////////////////////////
      protected void ExecuteNonQuery(string procName,
          params IDataParameter[] procParams) {
         SqlCommand cmd;
         ExecuteNonQuery(out cmd, procName, procParams);
      }


      protected void ExecuteNonQuery(out SqlCommand cmd, string procName,
          params IDataParameter[] procParams) {
         //Method variables
         SqlConnection cnx = null;
         cmd = null;  //Avoids "Use of unassigned variable" compiler error

         try {
            //Setup command object
            cmd = new SqlCommand(procName);
            cmd.CommandType = CommandType.StoredProcedure;
            for (int index = 0; index < procParams.Length; index++) {
               cmd.Parameters.Add(procParams[index]);
            }

            //Determine the transaction owner and process accordingly
            if (_isOwner) {
               cnx = new SqlConnection(GetConnectionString());
               cmd.Connection = cnx;
               cnx.Open();
            } else {
               cmd.Connection = _txn.Connection;
               cmd.Transaction = _txn;
            }

            //Execute the command
            cmd.ExecuteNonQuery();
         } finally {
            if (_isOwner) {
               cnx.Dispose(); //Implicitly calls cnx.Close()
            }
            if (cmd != null) cmd.Dispose();
         }
      }


      ////////////////////////////////////////////////////////////////////////
      // CreateParameter Methods
      ////////////////////////////////////////////////////////////////////////
      protected SqlParameter CreateParameter(string paramName, SqlDbType paramType, JToken paramValue) {
         object value;
         if (paramValue == null)
            value = DBNull.Value;
         else
            value = (object)((JValue)paramValue).Value;
         return CreateParameter(paramName, paramType, value);
      }

      protected SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue) {
         SqlParameter param = new SqlParameter(paramName, paramType);
         if (paramValue == null)
            paramValue = DBNull.Value;
         if (paramValue != DBNull.Value) {
            try {
               switch (paramType) {
                  case SqlDbType.VarChar:
                  case SqlDbType.NVarChar:
                  case SqlDbType.Char:
                  case SqlDbType.NChar:
                  case SqlDbType.Text:
                     paramValue = CheckParamValue((string)paramValue);
                     break;
                  case SqlDbType.DateTime:
                     if (paramValue is string) {     // "00:00:00 EDT 03-15-2011"
                        String[] formats = { "HH:mm:ss EDT MM-dd-yyyy",
                                          "HH:mm:ss EST MM-dd-yyyy",
                                          "yyyy-MM-dd" };
                        paramValue = (object)DateTime.ParseExact((string)paramValue, formats, null, DateTimeStyles.AllowWhiteSpaces);
                     } else paramValue = CheckParamValue((DateTime)paramValue);
                     break;
                  case SqlDbType.Int:
                     if (paramValue is string) {
                        DateTime val;
                        DateTime.TryParse((string)paramValue, out val);
                        paramValue = (object)val;
                     } else paramValue = CheckParamValue((int)paramValue);
                     break;
                  case SqlDbType.BigInt:
                     if (paramValue is string) {
                        Int64 val;
                        Int64.TryParse((string)paramValue, out val);
                        paramValue = (object)val;
                     } else paramValue = CheckParamValue((Int64)paramValue);
                     break;
                  case SqlDbType.UniqueIdentifier:
                     paramValue = CheckParamValue(GetGuid(paramValue));
                     break;
                  case SqlDbType.Bit:
                     if (paramValue is bool) paramValue = (int)((bool)paramValue ? 1 : 0);
                     else if (paramValue is string) paramValue = ((string)paramValue == "true" ? 1 : 0);
                     if ((int)paramValue < 0 || (int)paramValue > 1) paramValue = Constants.NullInt;
                     paramValue = CheckParamValue((int)paramValue);
                     break;
                  case SqlDbType.Float:
                     paramValue = CheckParamValue(Convert.ToSingle(paramValue));
                     break;
                  case SqlDbType.Decimal:
                     if (paramValue is string) {
                        decimal val;
                        decimal.TryParse((string)paramValue, out val);
                        paramValue = (object)val;
                     } else paramValue = CheckParamValue((decimal)paramValue);
                     break;
               }
            } catch {
               paramValue = DBNull.Value;
            }
         }
         param.Value = paramValue;
         return param;
      }

      protected SqlParameter CreateParameter(string paramName, SqlDbType paramType, ParameterDirection direction) {
         SqlParameter returnVal = CreateParameter(paramName, paramType, DBNull.Value);
         returnVal.Direction = direction;
         return returnVal;
      }

      protected SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue, ParameterDirection direction) {
         SqlParameter returnVal = CreateParameter(paramName, paramType, paramValue);
         returnVal.Direction = direction;
         return returnVal;
      }

      protected SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue, int size) {
         SqlParameter returnVal = CreateParameter(paramName, paramType, paramValue);
         returnVal.Size = size;
         return returnVal;
      }

      protected SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue, int size, ParameterDirection direction) {
         SqlParameter returnVal = CreateParameter(paramName, paramType, paramValue);
         returnVal.Direction = direction;
         returnVal.Size = size;
         return returnVal;
      }

      protected SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue, int size, byte precision) {
         SqlParameter returnVal = CreateParameter(paramName, paramType, paramValue);
         returnVal.Size = size;
         ((SqlParameter)returnVal).Precision = precision;
         return returnVal;
      }

      protected SqlParameter CreateParameter(string paramName, SqlDbType paramType, object paramValue, int size, byte precision, ParameterDirection direction) {
         SqlParameter returnVal = CreateParameter(paramName, paramType, paramValue);
         returnVal.Direction = direction;
         returnVal.Size = size;
         returnVal.Precision = precision;
         return returnVal;
      }


      ////////////////////////////////////////////////////////////////////////
      // CheckParamValue Methods
      ////////////////////////////////////////////////////////////////////////
      protected Guid GetGuid(object value) {
         Guid returnVal = Constants.NullGuid;
         if (value is string) {
            returnVal = new Guid((string)value);
         } else if (value is Guid) {
            returnVal = (Guid)value;
         }
         return returnVal;
      }

      protected object CheckParamValue(string paramValue) {
         if (string.IsNullOrEmpty(paramValue)) {
            return DBNull.Value;
         } else {
            return paramValue;
         }
      }

      protected object CheckParamValue(Guid paramValue) {
         if (paramValue.Equals(Constants.NullGuid)) {
            return DBNull.Value;
         } else {
            return paramValue;
         }
      }

      protected object CheckParamValue(DateTime paramValue) {
         if (paramValue.Equals(Constants.NullDateTime)) {
            return DBNull.Value;
         } else {
            return paramValue;
         }
      }

      protected object CheckParamValue(double paramValue) {
         if (paramValue.Equals(Constants.NullDouble)) {
            return DBNull.Value;
         } else {
            return paramValue;
         }
      }

      protected object CheckParamValue(float paramValue) {
         if (paramValue.Equals(Constants.NullFloat)) {
            return DBNull.Value;
         } else {
            return paramValue;
         }
      }

      protected object CheckParamValue(Decimal paramValue) {
         if (paramValue.Equals(Constants.NullDecimal)) {
            return DBNull.Value;
         } else {
            return paramValue;
         }
      }

      protected object CheckParamValue(int paramValue) {
         if (paramValue.Equals(Constants.NullInt)) {
            return DBNull.Value;
         } else {
            return paramValue;
         }
      }

      protected object CheckParamValue(Int64 paramValue) {
         if (paramValue.Equals(Constants.NullInt)) {
            return DBNull.Value;
         } else {
            return paramValue;
         }
      }

   } //class 

} //namespace