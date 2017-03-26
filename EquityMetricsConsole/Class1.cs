using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityMetrics.Retrieve {

    public abstract class Base {
        public string Name { get; set; }
        protected static Base _instance;
        static Base() {
            Console.WriteLine("In Base constructor");
        }
        public static Base Instance {
            get {
                return _instance;
            }
        }
    }
    public class Derived: Base {
        public static int InitDerived = 1;
        static Derived() {
            _instance = new Derived() { Name = "Derived" };
        }
    }

    public class Derived2: Base {
        public static int InitDerived = 2;
        static Derived2() {
            _instance = new Derived2() { Name = "Derived2" };
        }
    }
}
