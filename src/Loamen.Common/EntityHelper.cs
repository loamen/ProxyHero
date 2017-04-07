using System.Reflection;

namespace Loamen.Common
{
    public class EntityHelper
    {
        /// <summary>
        ///     复制实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T Copy<T>(T source, T target)
        {
            PropertyInfo[] targetProperties = target.GetType().GetProperties();
            PropertyInfo[] sourceProperties = source.GetType().GetProperties();

            foreach (PropertyInfo tProperty in targetProperties)
            {
                foreach (PropertyInfo sProperty in sourceProperties)
                {
                    if (sProperty != null)
                    {
                        if (sProperty.Name == tProperty.Name)
                        {
                            tProperty.SetValue(target, sProperty.GetValue(source, null), null);
                        }
                    }
                }
            }

            return target;
        }
    }
}