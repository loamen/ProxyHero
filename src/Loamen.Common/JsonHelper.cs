using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

/// <summary>
/// 实体转换辅助类
/// </summary>
/// Copyright (c) 2011
/// 创 建 人：杨栋(dong_yang1)
/// 创建日期：2012-02-23
/// 修 改 人：
/// 修改日期：
/// 版 本：

namespace Loamen.Common
{
    public class JsonHelper
    {
        /// <summary>
        ///     把对象序列化 JSON 字符串
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="model">对象实体</param>
        /// <returns>JSON字符串</returns>
        public static string ModelToJson<T>(T model)
        {
            //记住 添加引用 System.ServiceModel.Web 
            /**
             * 如果不添加上面的引用,System.Runtime.Serialization.Json; Json是出不来的哦
             * */
            var json = new DataContractJsonSerializer(typeof (T));
            using (var ms = new MemoryStream())
            {
                json.WriteObject(ms, model);
                string strJson = Encoding.UTF8.GetString(ms.ToArray());
                return strJson;
            }
        }

        /// <summary>
        ///     把JSON字符串还原为对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="json">JSON字符串</param>
        /// <returns>对象实体</returns>
        public static T JsonToModel<T>(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                var obj = Activator.CreateInstance<T>();
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {
                    var dcj = new DataContractJsonSerializer(typeof (T));
                    return (T) dcj.ReadObject(ms);
                }
            }
            return default(T);
        }
    }
}