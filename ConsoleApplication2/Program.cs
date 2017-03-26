using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquityMetrics;
using EquityMetrics.Model;
using System.Net;
using System.IO;
using EquityMetrics.DataAccess;

namespace ConsoleApplication2 {
   class Program {
     
      static void Main(string[] args) {
         StockSymbols.Instance.Initialize();
         RetrieveQuotes();
      }

      public static void RetrieveQuotes() {
         // make a request for data 
         StockSymbol Symbol;
         StockQuotesDataService DataService = new StockQuotesDataService();
         string symbol;
         do {
            Symbol = StockSymbols.Instance.GetNextStock();
            symbol = Symbol.Symbol;
            StockQuote stock = new StockQuote(Symbol.Id);             
            stock.Quote.QuoteData.Product.Symbol = symbol;
            string url = GetURL(2, symbol);
            Console.WriteLine(url);
            Stream stream = GetResponse(url);
            //            responseXML = eTradeModel.GetQuote(symbol, "ALL");
            //            WriteXML(responseXML);
            StreamReader sr = new StreamReader(stream);
            string line = sr.ReadLine();  // skip the header
            while (sr.Peek() >= 0) {
               line = sr.ReadLine();
               stock.Parse(line);
               stock.Save(DataService);            
            }
            
            //stock = StockQuote.ReadStockQuote(Symbol.Id, responseXML);
         } while (StockSymbols.Instance.Index > 0);
         //            _messages.AddMessage("Asked for " + symbol + ", Received " + stock.Quote.QuoteData.Product.Symbol);
      }

      private static Stream GetResponse(string url) {
         WebClient client = new WebClient();
         byte[] content = client.DownloadData(url);
         return new MemoryStream(content);
      }

      public static string GetURL(int years, string symbol) {
         string URL = "https://app.quotemedia.com/quotetools/getHistoryDownload.csv?&webmasterId=501&startDay={0}&startMonth={1}&startYear={2}&endDay={3}&endMonth={4}&endYear={5}&isRanged=true&symbol={6}";
         DateTime date2 = DateTime.Now;
         DateTime date1 = date2.AddYears(years * -1);
         object[] args = { date1.Day, date1.Month, date1.Year, date2.Day, date2.Month, date2.Year, symbol };
         return string.Format(URL, args);
      }
   }
}
