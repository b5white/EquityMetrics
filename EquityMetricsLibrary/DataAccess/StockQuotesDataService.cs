using System;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json.Linq;
using EquityMetrics.Common;

namespace EquityMetrics.DataAccess {

   public class StockQuotesDataService: DataServiceBase
   {
        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///	Creates a new DataService
        /// </summary>
        ////////////////////////////////////////////////////////////////////////
      public StockQuotesDataService() : base() { }

        ////////////////////////////////////////////////////////////////////////
        /// <summary>
        ///	Creates a new DataService and specifies a transaction with
        ///	which to operate
        /// </summary>
        ////////////////////////////////////////////////////////////////////////
      public StockQuotesDataService(IDbTransaction txn) : base(txn) { }

      public DataSet GetAll()
      {
         return ExecuteDataSet("StockQuotes_GetAll", null);
      }

      public DataSet GetByID(int ID) {
         return ExecuteDataSet("StockQuotes_GetByID",
            CreateParameter("@ID", SqlDbType.Int, ID));
      }

      public DataSet GetBySymbol(string Symbol) {
         return ExecuteDataSet("StockQuotes_GetBySymbol",
            CreateParameter("@ID", SqlDbType.Char, Symbol.Substring(0,6)));
      }

      public void Save(JObject JSON) {
         SqlCommand cmd;
         JToken quote = JSON["Quote"];
         JToken quotedata = quote["QuoteData"];
         JToken all = quotedata["All"];
         JToken product = quotedata["Product"];

         ExecuteNonQuery(out cmd, "StockQuote_Save",
            CreateParameter("@aStockId", SqlDbType.Int, product["StockId"]),
            CreateParameter("@aAdjNonAdjFlag", SqlDbType.Bit, all["AdjNonAdjFlag"]),
            CreateParameter("@aAnnualDividend", SqlDbType.Float, all["AnnualDividend"]),
            CreateParameter("@aAsk", SqlDbType.Float, all["Ask"]),
            CreateParameter("@aAskExchange", SqlDbType.Char, all["AskExchange"]),
            CreateParameter("@aAskSize", SqlDbType.BigInt, all["AskSize"]),
            CreateParameter("@aAskTime", SqlDbType.DateTime, all["AskTime"]),
            CreateParameter("@aBeta", SqlDbType.Float, all["Beta"]),
            CreateParameter("@aBid", SqlDbType.Float, all["Bid"]),
            CreateParameter("@aBidExchange", SqlDbType.Char, all["BidExchange"]),
            CreateParameter("@aBidSize", SqlDbType.BigInt, all["BidSize"]),
            CreateParameter("@aBidTime", SqlDbType.DateTime, all["BidTime"]),
            CreateParameter("@aChgClose", SqlDbType.Float, all["ChgClose"]),
            CreateParameter("@aChgClosePrcn", SqlDbType.Float, all["ChgClosePrcn"]),
            CreateParameter("@aCompanyName", SqlDbType.Char, all["CompanyName"]),
            CreateParameter("@aDaysToExpiration", SqlDbType.BigInt, all["DaysToExpiration"]),
            CreateParameter("@aDirLast", SqlDbType.Char, all["DirLast"]),
            CreateParameter("@aDividend", SqlDbType.Float, all["Dividend"]),
            CreateParameter("@aEps", SqlDbType.Float, all["Eps"]),
            CreateParameter("@aEstEarnings", SqlDbType.Float, all["EstEarnings"]),
            CreateParameter("@aExDivDate", SqlDbType.Char, all["ExDivDate"]),
            CreateParameter("@aExchgLastTrade", SqlDbType.Char, all["ExchgLastTrade"]),
            CreateParameter("@aFsi", SqlDbType.Char, all["Fsi"]),
            CreateParameter("@aHigh", SqlDbType.Float, all["High"]),
            CreateParameter("@aHigh52", SqlDbType.Float, all["High52"]),
            CreateParameter("@aHighAsk", SqlDbType.Float, all["HighAsk"]),
            CreateParameter("@aHighBid", SqlDbType.Float, all["HighBid"]),
            CreateParameter("@aLastTrade", SqlDbType.Float, all["LastTrade"]),
            CreateParameter("@aLow", SqlDbType.Float, all["Low"]),
            CreateParameter("@aLow52", SqlDbType.Float, all["Low52"]),
            CreateParameter("@aLowAsk", SqlDbType.Float, all["LowAsk"]),
            CreateParameter("@aLowBid", SqlDbType.Float, all["LowBid"]),
            CreateParameter("@aNumTrades", SqlDbType.BigInt, all["NumTrades"]),
            CreateParameter("@aOpen", SqlDbType.Float, all["Open"]),
            CreateParameter("@aOpenInterest", SqlDbType.BigInt, all["OpenInterest"]),
            CreateParameter("@aOptionStyle", SqlDbType.Char, all["OptionStyle"]),
            CreateParameter("@aOptionUnderlier", SqlDbType.Char, all["OptionUnderlier"]),
            CreateParameter("@aPrevClose", SqlDbType.Float, all["PrevClose"]),
            CreateParameter("@aPrevDayVolume", SqlDbType.BigInt, all["PrevDayVolume"]),
            CreateParameter("@aPrimaryExchange", SqlDbType.Char, all["PrimaryExchange"]),
            CreateParameter("@aSymbolDesc", SqlDbType.Char, all["SymbolDesc"]),
            CreateParameter("@aTodayClosed", SqlDbType.Float, all["TodayClose"]),
            CreateParameter("@aTotalVolume", SqlDbType.BigInt, all["TotalVolume"]),
            CreateParameter("@aUpc", SqlDbType.BigInt, all["Upc"]),
            CreateParameter("@aVolume10Day", SqlDbType.BigInt, all["Volume10Day"]),
            CreateParameter("@aDateTime", SqlDbType.DateTime, quotedata["DateTime"]),
            CreateParameter("@aSymbol", SqlDbType.Char, product["Symbol"]),
            CreateParameter("@aType", SqlDbType.Char, product["Type"]),
            CreateParameter("@aExchange", SqlDbType.Char, product["Exchange"]));

         //CreateParameter("@aStockId", SqlDbType.Int, product["StockId"]);
         //CreateParameter("@aAdjNonAdjFlag", SqlDbType.Bit, (all["AdjNonAdjFlag"]));
         //CreateParameter("@aAnnualDividend", SqlDbType.Float, all["AnnualDividend"]);
         //CreateParameter("@aAsk", SqlDbType.Float, all["Ask"]);
         //CreateParameter("@aAskExchange", SqlDbType.Char, all["AskExchange"]);
         //CreateParameter("@aAskSize", SqlDbType.BigInt, all["AskSize"]);
         //CreateParameter("@aAskTime", SqlDbType.DateTime, all["AskTime"]);
         //CreateParameter("@aBeta", SqlDbType.Float, all["Beta"]);
         //CreateParameter("@aBid", SqlDbType.Float, all["Bid"]);
         //CreateParameter("@aBidExchange", SqlDbType.Char, all["BidExchange"]);
         //CreateParameter("@aBidSize", SqlDbType.BigInt, all["BidSize"]);
         //CreateParameter("@aBidTime", SqlDbType.DateTime, all["BidTime"]);
         //CreateParameter("@aChgClose", SqlDbType.Float, all["ChgClose"]);
         //CreateParameter("@aChgClosePrcn", SqlDbType.Float, all["ChgClosePrcn"]);
         //CreateParameter("@aCompanyName", SqlDbType.Char, all["CompanyName"]);
         //CreateParameter("@aDaysToExpiration", SqlDbType.BigInt, all["DaysToExpiration"]);
         //CreateParameter("@aDirLast", SqlDbType.Char, all["DirLast"]);
         //CreateParameter("@aDividend", SqlDbType.Float, all["Dividend"]);
         //CreateParameter("@aEps", SqlDbType.Float, all["Eps"]);
         //CreateParameter("@aEstEarnings", SqlDbType.Float, all["EstEarnings"]);
         //CreateParameter("@aExDivDate", SqlDbType.Char, all["ExDivDate"]);
         //CreateParameter("@aExchgLastTrade", SqlDbType.Char, all["ExchgLastTrade"]);
         //CreateParameter("@aFsi", SqlDbType.Char, all["Fsi"]);
         //CreateParameter("@aHigh", SqlDbType.Float, all["High"]);
         //CreateParameter("@aHigh52", SqlDbType.Float, all["High52"]);
         //CreateParameter("@aHighAsk", SqlDbType.Float, all["HighAsk"]);
         //CreateParameter("@aHighBid", SqlDbType.Float, all["HighBid"]);
         //CreateParameter("@aLastTrade", SqlDbType.Float, all["LastTrade"]);
         //CreateParameter("@aLow", SqlDbType.Float, all["Low"]);
         //CreateParameter("@aLow52", SqlDbType.Float, all["Low52"]);
         //CreateParameter("@aLowAsk", SqlDbType.Float, all["LowAsk"]);
         //CreateParameter("@aLowBid", SqlDbType.Float, all["LowBid"]);
         //CreateParameter("@aNumTrades", SqlDbType.BigInt, all["NumTrades"]);
         //CreateParameter("@aOpen", SqlDbType.Float, all["Open"]);
         //CreateParameter("@aOpenInterest", SqlDbType.BigInt, all["OpenInterest"]);
         //CreateParameter("@aOptionStyle", SqlDbType.Char, all["OptionStyle"]);
         //CreateParameter("@aOptionUnderlier", SqlDbType.Char, all["OptionUnderlier"]);
         //CreateParameter("@aPrevClose", SqlDbType.Float, all["PrevClose"]);
         //CreateParameter("@aPrevDayVolume", SqlDbType.BigInt, all["PrevDayVolume"]);
         //CreateParameter("@aPrimaryExchange", SqlDbType.Char, all["PrimaryExchange"]);
         //CreateParameter("@aSymbolDesc", SqlDbType.Char, all["SymbolDesc"]);
         //CreateParameter("@aTodayClosed", SqlDbType.Float, all["TodayClosed"]);
         //CreateParameter("@aTotalVolume", SqlDbType.BigInt, all["TotalVolume"]);
         //CreateParameter("@aUpc", SqlDbType.BigInt, all["Upc"]);
         //CreateParameter("@aVolume10Day", SqlDbType.BigInt, all["Volume10Day"]);
         //CreateParameter("@aDateTime", SqlDbType.DateTime, quotedata["DateTime"]);
         //CreateParameter("@aSymbol", SqlDbType.Char, product["Symbol"]);
         //CreateParameter("@aType", SqlDbType.Char, product["Type"]);
         //CreateParameter("@aExchange", SqlDbType.Char, product["Exchange"]);       
      }
   } //class

} //namespace