using System;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using EquityMetrics.Utilities;
using EquityMetrics.Model;

namespace EquityMetrics.Retrieve {

   public class ETradeController {
      OAuthRepository authRepository;
      ETradeModel eTradeModel;
      Messages _messages = Messages.Instance; // Singleton reference to the Messages class. This contains the shared event
                                              // and messages list.

      public ETradeController() {
         eTradeModel = new ETradeModel(true);  // replace the flag with an entry from the config
         _messages.AddMessage("In EtradeController constructor");
         StockSymbols.Instance.Initialize();
         StockQuotes.Instance.Initialize();
      }

      public void RetrieveQuotes(int count) {
         // make a request for data from ETrade
         StockSymbol Symbol;
         string responseXML;
         string symbol;
         StockQuote stock;
         for (int i = 0; i < count; i++) {
            Symbol = StockSymbols.Instance.GetNextStock();
            symbol = Symbol.Symbol;
            System.Threading.Thread.Sleep(250);
            responseXML = eTradeModel.GetQuote(symbol, "ALL");
            if (responseXML != null) {
               WriteXML(responseXML);
               stock = StockQuote.ReadStockQuote(Symbol.Id, responseXML);
               _messages.AddMessage("Asked for " + symbol + ", Received " + stock.Quote.QuoteData.Product.Symbol);
            } else {
               _messages.AddMessage("Asked for " + symbol + ", Received nothing.");
            }
         }
      }

      private void WriteXML(string responseXML) {
         System.IO.File.WriteAllText(@"C:\Temp\responseXML.txt", responseXML);
      }
   }
}

