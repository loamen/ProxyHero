using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using ProxyHero.Entity;

namespace ProxyHero
{
    public class PluginDAL
    {
        static string TABLE_NAME = "Plugins"; //table name

        /// <summary>
        /// Insert
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Insert(ProxyHero.Entity.Plugin model)
        {
            return LiteDBHelper<ProxyHero.Entity.Plugin>.Insert(model, TABLE_NAME);
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(ProxyHero.Entity.Plugin model)
        {
            return LiteDBHelper<ProxyHero.Entity.Plugin>.Update(model, TABLE_NAME);
        }

        /// <summary>
        /// Exists
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>

        public bool Exists(string fileName)
        {
            // Open data file (or create if not exits)
            using (var db = new LiteDatabase(Config.SettingDataFileName))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection<ProxyHero.Entity.Plugin>(TABLE_NAME);
                var doc = col.Exists(m=>m.FileName == fileName);
                return doc;
            }
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="docId"></param>
        /// <returns></returns>
        public bool Delete(int docId)
        {
            return LiteDBHelper<ProxyHero.Entity.Plugin>.Delete(docId, TABLE_NAME);
        }

        /// <summary>
        /// Query one model
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public ProxyHero.Entity.Plugin FindOne(string fileName)
        {
            // Open data file (or create if not exits)
            using (var db = new LiteDatabase(Config.SettingDataFileName))
            {
                // Get a collection (or create, if not exits)
                var col = db.GetCollection<ProxyHero.Entity.Plugin>(TABLE_NAME);
                var doc = col.FindOne(m=>m.FileName == fileName);
                return doc;
            }
        }

        /// <summary>
        /// Find all list
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProxyHero.Entity.Plugin> FindAll()
        {
            return LiteDBHelper<ProxyHero.Entity.Plugin>.FindAll(TABLE_NAME);
        }

        /// <summary>
        /// Count
        /// </summary>
        /// <returns></returns>
        public int Count()
        {
            return LiteDBHelper<ProxyHero.Entity.Plugin>.Count(TABLE_NAME);
        }
    }
}
