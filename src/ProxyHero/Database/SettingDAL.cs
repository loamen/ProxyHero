using LiteDB;
using ProxyHero.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyHero
{
    public class SettingDAL
    {
        static string TABLE_NAME = "Settings"; //table name

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(Setting model)
        {
            return LiteDBHelper<Setting>.Insert(model, TABLE_NAME);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Setting model)
        {
            return LiteDBHelper<Setting>.Update(model, TABLE_NAME);
        }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>

        public bool Exists(Query query)
        {
            return LiteDBHelper<Setting>.Exists(query, TABLE_NAME);
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public bool Delete(int docId)
        {
            return LiteDBHelper<Setting>.Delete(docId, TABLE_NAME);
        }

        /// <summary>
        /// Query one model
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public Setting FindOne(Query query)
        {
            return LiteDBHelper<Setting>.FindOne(query, TABLE_NAME);
        }

        /// <summary>
        /// Find all list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Setting> FindAll()
        {
            return LiteDBHelper<Setting>.FindAll(TABLE_NAME);
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return LiteDBHelper<Setting>.Count(TABLE_NAME);
        }
    }
}
