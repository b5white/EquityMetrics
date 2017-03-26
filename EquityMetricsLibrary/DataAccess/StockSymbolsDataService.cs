using System;
using System.Data;
using System.Data.SqlClient;
using EquityMetrics.Common;

namespace EquityMetrics.DataAccess {

    public class StockSymbolsDataService: DataServiceBase
    {
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///	Creates a new DataService
        /// </summary>
        ////////////////////////////////////////////////////////////////////////
        public StockSymbolsDataService() : base() { }

        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///	Creates a new DataService and specifies a transaction with
        ///	which to operate
        /// </summary>
        ////////////////////////////////////////////////////////////////////////
        public StockSymbolsDataService(IDbTransaction txn) : base(txn) { }


        public DataSet GetAll()
        {
            return ExecuteDataSet("StockSymbols_GetAll", null);
        }

        public DataSet GetByID(int ID) {
            return ExecuteDataSet("StockSymbols_GetByID",
                CreateParameter("@ID", SqlDbType.Int, ID));
        }

        public DataSet GetBySymbol(string Symbol) {
            return ExecuteDataSet("StockSymbols_GetBySymbol",
                CreateParameter("@ID", SqlDbType.Char, Symbol.Substring(0,6)));
        }

        //public void Person_Save(ref int personID, string nameFirst, string nameLast, DateTime dob)
        //{
        //    SqlCommand cmd;
        //    ExecuteNonQuery(out cmd, "Person_Save",
        //        CreateParameter("@PersonID", SqlDbType.Int, personID, ParameterDirection.InputOutput),
        //        CreateParameter("@NameFirst", SqlDbType.NVarChar, nameFirst),
        //        CreateParameter("@NameLast", SqlDbType.NVarChar, nameLast),
        //        CreateParameter("@DOB", SqlDbType.DateTime, dob));            
        //    personID = (int)cmd.Parameters["@PersonID"].Value;
        //    cmd.Dispose();
        //}       
    } //class

} //namespace