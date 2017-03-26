//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace EquityMetrics.Retrieve {
//    public abstract class Base2 {
//        protected static Base2 _instance;

//        static Base2() {
//            Console.WriteLine("In Base2 constructor");
//        }
//        public static Base2 Instance {
//            get {
//                return _instance;
//            }
//        }

//        public virtual void Initialize() {

//        }

//    }
//    public class StockSymbols: Base2 {
//        // public class StockSymbols: BaseCollection<StockSymbol> {
//        /// <summary>
//        /// Prevents a default instance of the <see cref="Stocks"/> class from being created.
//        /// </summary>
//        static StockSymbols() {
//            Console.WriteLine("In StockSymbols constructor");
//            _instance = new StockSymbols();
//            //            SetDataService(new StockSymbolsDataService());
//        }

//        public override void Initialize() {
//            //            GetStockSymbols();
//            Console.WriteLine("In StockSymbols Initializer2");
//            Console.WriteLine(_instance.GetType().ToString());
//        }

//        public static void EmptyStaticMethod() {
//            Console.WriteLine("In StockSymbols EmptyStaticMethod");
//            Console.WriteLine(_instance.GetType().ToString());
//        }
//    }
//    public class StockQuotes: Base2 {
//        //        public class StockQuotes: BaseCollection<StockQuote> {

//        static StockQuotes() {
//            Console.WriteLine("In StockQuotes Constructor");
//            _instance = new StockQuotes();
//        }
//        public override void Initialize() {
//            Console.WriteLine("In StockQuotes Initializer2");
//            Console.WriteLine(_instance.GetType().ToString());
//        }
//        public static void EmptyStaticMethod() {
//            Console.WriteLine("In StockQuotes EmptyStaticMethod");
//            Console.WriteLine(_instance.GetType().ToString());
//        }
//    }
//}