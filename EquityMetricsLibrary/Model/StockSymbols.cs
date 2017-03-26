using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.Xml.Serialization;
using EquityMetrics.DataAccess;

namespace EquityMetrics.Model {

    public class StockSymbol : BaseObject {
        private int id;
        private string symbol;
        private DateTime? startDate;
        private float? size;
        private string description;
        private string exchange;
        private string industry;
        private string sector;

        public int Id {
            get {
                return id;
            }

            set {
                id = value;
            }
        }

        public string Symbol {
            get {
                return symbol;
            }

            set {
                symbol = value;
            }
        }

        public DateTime? StartDate {
            get {
                return startDate;
            }

            set {
                startDate = value;
            }
        }

        public float? Size {
            get {
                return size;
            }

            set {
                size = value;
            }
        }

        public string Description {
            get {
                return description;
            }

            set {
                description = value;
            }
        }

        public string Exchange {
            get {
                return exchange;
            }

            set {
                exchange = value;
            }
        }

        public string Industry {
            get {
                return industry;
            }

            set {
                industry = value;
            }
        }

        public string Sector {
            get {
                return sector;
            }

            set {
                sector = value;
            }
        }

        public override bool MapData(DataRow row) {
            Id = GetInt(row, "ID");
            Symbol = GetString(row, "Symbol");
            StartDate = GetDateTime(row, "StartDate");
            Size = GetFloat(row, "Size");
            Description = GetString(row, "Description");
            Exchange = GetString(row, "Exchange");
            Industry = GetString(row, "Industry");
            Sector = GetString(row, "Sector");
            //Name = Symbol;
            return base.MapData(row);
        }
    }

 public class StockSymbols: BaseCollection<StockSymbol> {
        public int Index = 0;
        /// <summary>
        /// Prevents a default instance of the <see cref="Stocks"/> class from being created.
        /// </summary>
        static StockSymbols() {
            Console.WriteLine("In StockSymbols constructor");
            _instance = new StockSymbols();
            SetDataService(new StockSymbolsDataService());
        }

        /// <summary>
        /// Gets the instance of the class.
        /// </summary>
        public static StockSymbols Instance {
            get {
                return _instance as StockSymbols;
            }
        }

        public override void Initialize() {
            GetStockSymbols();
            Console.WriteLine("In StockSymbols Initializer2");
            Console.WriteLine(_instance.GetType().ToString());
        }

        public static void EmptyStaticMethod() {
        }

        public void GetStockSymbols() {          
            DataSet ds = ((StockSymbolsDataService)DataService).GetAll();
            MapObjects(ds);
            Index = 0;
        }

        public StockSymbol GetNextStock() {
            StockSymbol stock = Get(Index);
            Index++;
            if (Index >= Count) {
                GetStockSymbols();   // refresh list every time through.
            }            
            return stock;
        }
    }
}

