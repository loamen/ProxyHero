namespace ProxyHero.Model
{
    /// <summary>
    ///     Json返回实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class JsonResult<T>
    {
        /// <summary>
        ///     错误代码
        /// </summary>
        public int err_code { get; set; }

        /// <summary>
        ///     错误信息
        /// </summary>
        public string err_msg { get; set; }

        /// <summary>
        ///     数据
        /// </summary>
        public T data { get; set; }
    }
}