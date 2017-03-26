using System;
using System.Collections.Generic;
using System.Data;
using EquityMetrics.DataAccess;

namespace EquityMetrics.Model {
    public abstract class BaseCollection<T>: List<T> where T : BaseObject, new() {
        protected static BaseCollection<T> _instance;
        protected DataServiceBase DataService;

        static public void SetDataService(DataServiceBase DataService) {
            _instance.DataService = DataService;
        }

        public virtual void Initialize() {

        }

        public T Get(int Index) {
            if (Count >= Index) {
                return this[Index];
            } else {
                //        throw EIndexOutOfRange
                return null;
            }
        }
        ////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        ////////////////////////////////////////////////////////////////////////////////////////////
        public bool MapObjects(DataSet ds) {
            if (ds != null && ds.Tables.Count > 0) {
                return MapObjects(ds.Tables[0]);
            } else {
                return false;
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        ////////////////////////////////////////////////////////////////////////////////////////////
        public bool MapObjects(DataTable dt) {
            Clear();
            for (int i = 0; i < dt.Rows.Count; i++) {
                T obj = new T();
                obj.MapData(dt.Rows[i]);
                this.Add(obj);                
            }
            return true;
        }
    }
}
