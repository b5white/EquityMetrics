using System;
using System.Collections.Generic;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json.Linq;
using EquityMetrics.DataAccess;

namespace EquityMetrics.Model {

   public class StockQuote: BaseObject {
      public QuoteResponse Quote;

      public StockQuote() { }
      public StockQuote(int stockId) {
         Quote = new QuoteResponse();
         Quote.QuoteData = new QuoteData();
         Quote.QuoteData.All = new All();
         Quote.QuoteData.Product = new Product();
         Quote.QuoteData.Product.StockId = stockId;
      }
      public static StockQuote ReadStockQuote(int Id, string inputString) {
         StringReader reader = new StringReader(inputString);
         return ReadStockQuote(Id, reader);
      }

      public static StockQuote ReadStockQuote(int Id, StringReader reader) {
         XmlSerializer serializer = new XmlSerializer(typeof(QuoteResponse));
         StockQuote quote = new StockQuote();
         quote.Quote = (QuoteResponse)serializer.Deserialize(reader);
         quote.Quote.QuoteData.Product.StockId = Id;
         quote.Quote.QuoteData.DateTime = DateTime.Now.ToString("HH:mm:ss EDT MM-dd-yyyy");
         StockQuotes.Instance.Add(quote);
         return quote;
      }

      public void Parse(string line) {
         char[] delim = { ',' };
         string[] fields = line.Split(delim, StringSplitOptions.None);
         Quote.QuoteData.DateTime = fields[0];
         Quote.QuoteData.All.Open = fields[1];
         Quote.QuoteData.All.High = fields[2];
         Quote.QuoteData.All.Low = fields[3];
         Quote.QuoteData.All.TodayClose = fields[4];
         Quote.QuoteData.All.TotalVolume = fields[5];  //??
         Quote.QuoteData.All.ChgClose = fields[6];
         Quote.QuoteData.All.ChgClosePrcn = fields[7].Replace('%', ' ');
         Quote.QuoteData.All.Dividend = fields[8];  //??
         Quote.QuoteData.All.LastTrade = fields[9];  //??
         Quote.QuoteData.All.NumTrades = fields[10];  //??

//         date open  high low   close volume   changed changep  adjclose tradeval tradevol

      }

      public void Save(StockQuotesDataService DataService) {
         //DataSet ds = ((StockSymbolsDataService)DataService).GetAll();
         DataService.Save(ToJSON());
      }

      public JObject ToJSON() {
         return (JObject)JToken.FromObject(this);
      }

      [XmlRoot(ElementName = "all")]
      public class All {
         [XmlElement(ElementName = "adjNonAdjFlag")]
         public string AdjNonAdjFlag { get; set; }
         [XmlElement(ElementName = "annualDividend")]
         public string AnnualDividend { get; set; }
         [XmlElement(ElementName = "ask")]
         public string Ask { get; set; }
         [XmlElement(ElementName = "askExchange")]
         public string AskExchange { get; set; }
         [XmlElement(ElementName = "askSize")]
         public string AskSize { get; set; }
         [XmlElement(ElementName = "askTime")]
         public string AskTime { get; set; }
         [XmlElement(ElementName = "bid")]
         public string Bid { get; set; }
         [XmlElement(ElementName = "bidExchange")]
         public string BidExchange { get; set; }
         [XmlElement(ElementName = "bidSize")]
         public string BidSize { get; set; }
         [XmlElement(ElementName = "bidTime")]
         public string BidTime { get; set; }
         [XmlElement(ElementName = "chgClose")]
         public string ChgClose { get; set; }
         [XmlElement(ElementName = "chgClosePrcn")]
         public string ChgClosePrcn { get; set; }
         [XmlElement(ElementName = "companyName")]
         public string CompanyName { get; set; }
         [XmlElement(ElementName = "daysToExpiration")]
         public string DaysToExpiration { get; set; }
         [XmlElement(ElementName = "dirLast")]
         public string DirLast { get; set; }
         [XmlElement(ElementName = "dividend")]
         public string Dividend { get; set; }
         [XmlElement(ElementName = "eps")]
         public string Eps { get; set; }
         [XmlElement(ElementName = "estEarnings")]
         public string EstEarnings { get; set; }
         [XmlElement(ElementName = "exDivDate")]
         public string ExDivDate { get; set; }
         [XmlElement(ElementName = "exchgLastTrade")]
         public string ExchgLastTrade { get; set; }
         [XmlElement(ElementName = "fsi")]
         public string Fsi { get; set; }
         [XmlElement(ElementName = "high")]
         public string High { get; set; }
         [XmlElement(ElementName = "high52")]
         public string High52 { get; set; }
         [XmlElement(ElementName = "highAsk")]
         public string HighAsk { get; set; }
         [XmlElement(ElementName = "highBid")]
         public string HighBid { get; set; }
         [XmlElement(ElementName = "lastTrade")]
         public string LastTrade { get; set; }
         [XmlElement(ElementName = "low")]
         public string Low { get; set; }
         [XmlElement(ElementName = "low52")]
         public string Low52 { get; set; }
         [XmlElement(ElementName = "lowAsk")]
         public string LowAsk { get; set; }
         [XmlElement(ElementName = "lowBid")]
         public string LowBid { get; set; }
         [XmlElement(ElementName = "numTrades")]
         public string NumTrades { get; set; }
         [XmlElement(ElementName = "open")]
         public string Open { get; set; }
         [XmlElement(ElementName = "openInterest")]
         public string OpenInterest { get; set; }
         [XmlElement(ElementName = "optionStyle")]
         public string OptionStyle { get; set; }
         [XmlElement(ElementName = "optionUnderlier")]
         public string OptionUnderlier { get; set; }
         [XmlElement(ElementName = "prevClose")]
         public string PrevClose { get; set; }
         [XmlElement(ElementName = "prevDayVolume")]
         public string PrevDayVolume { get; set; }
         [XmlElement(ElementName = "primaryExchange")]
         public string PrimaryExchange { get; set; }
         [XmlElement(ElementName = "symbolDesc")]
         public string SymbolDesc { get; set; }
         [XmlElement(ElementName = "todayClose")]
         public string TodayClose { get; set; }
         [XmlElement(ElementName = "totalVolume")]
         public string TotalVolume { get; set; }
         [XmlElement(ElementName = "upc")]
         public string Upc { get; set; }
         [XmlElement(ElementName = "volume10Day")]
         public string Volume10Day { get; set; }
      }

      [XmlRoot(ElementName = "product")]
      public class Product {
         [XmlElement(ElementName = "stockid")]
         public int StockId { get; set; }
         [XmlElement(ElementName = "symbol")]
         public string Symbol { get; set; }
         [XmlElement(ElementName = "type")]
         public string Type { get; set; }
         [XmlElement(ElementName = "exchange")]
         public string Exchange { get; set; }
      }

      [XmlRoot(ElementName = "QuoteData")]
      public class QuoteData {
         [XmlElement(ElementName = "all")]
         public All All { get; set; }
         [XmlElement(ElementName = "dateTime")]
         public string DateTime { get; set; }
         [XmlElement(ElementName = "product")]
         public Product Product { get; set; }
      }

      [XmlRoot(ElementName = "QuoteResponse")]
      public class QuoteResponse {
         [XmlElement(ElementName = "QuoteData")]
         public QuoteData QuoteData { get; set; }
      }
   }

   public class StockQuotes: BaseCollection<StockQuote> {

      static StockQuotes() {
         Console.WriteLine("In StockQuotes Constructor");
         _instance = new StockQuotes();
         SetDataService(new StockQuotesDataService());
      }

      /// <summary>
      /// Gets the instance of the class.
      /// </summary>
      public static StockQuotes Instance {
         get {
            return _instance as StockQuotes;
         }
      }

      public override void Initialize() {
         Console.WriteLine("In StockQuotes Initializer2");
         Console.WriteLine(_instance.GetType().ToString());
         //Add
      }

      public new void Add(StockQuote quote) {
         quote.Save((StockQuotesDataService)DataService);
         base.Add(quote);
      }
      public static void EmptyStaticMethod() {
      }
   }
}
