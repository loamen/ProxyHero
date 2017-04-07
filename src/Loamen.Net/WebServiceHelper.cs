using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Web.Services.Description;
using Microsoft.CSharp;

namespace Loamen.Net
{
    /* 调用方式
        *   string url = "http://www.webservicex.net/globalweather.asmx" ;
        *   string[] args = new string[2] ;
        *   args[0] = "Hangzhou";
        *   args[1] = "China" ;
        *   object result = WebServiceHelper.InvokeWebService(url ,"GetWeather" ,args) ;
        *   Response.Write(result.ToString());
        */

    public class WebServiceHelper
    {
        #region InvokeWebService

        /// <summary>
        ///     动态调用web服务
        /// </summary>
        /// <param name="url">WSDL服务地址</param>
        /// <param name="methodname">方法名</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object InvokeWebService(string url, string methodname, object[] args)
        {
            return InvokeWebService(url, null, methodname, args);
        }

        /// <summary>
        ///     动态调用web服务
        /// </summary>
        /// <param name="url">WSDL服务地址</param>
        /// <param name="classname">类名</param>
        /// <param name="methodname">方法名</param>
        /// <param name="args">参数</param>
        /// <returns></returns>
        public static object InvokeWebService(string url, string classname, string methodname, object[] args)
        {
            string @namespace = "BBSQ.WebService.DynamicWebCalling";
            if ((classname == null) || (classname == ""))
            {
                classname = GetWsClassName(url);
            }

            //try
            //{
            //获取WSDL
            var wc = new WebClient();
            wc.Proxy = null;
            Stream stream = wc.OpenRead(url + "?wsdl");

            ServiceDescription sd = ServiceDescription.Read(stream);
            var sdi = new ServiceDescriptionImporter();
            sdi.AddServiceDescription(sd, "", "");
            var cn = new CodeNamespace(@namespace);

            //生成客户端代理类代码
            var ccu = new CodeCompileUnit();
            ccu.Namespaces.Add(cn);
            sdi.Import(cn, ccu);
            var icc = new CSharpCodeProvider();

            //设定编译参数
            var cplist = new CompilerParameters();
            cplist.GenerateExecutable = false;
            cplist.GenerateInMemory = true;
            cplist.ReferencedAssemblies.Add("System.dll");
            cplist.ReferencedAssemblies.Add("System.XML.dll");
            cplist.ReferencedAssemblies.Add("System.Web.Services.dll");
            cplist.ReferencedAssemblies.Add("System.Data.dll");

            //编译代理类
            CompilerResults cr = icc.CompileAssemblyFromDom(cplist, ccu);
            if (cr.Errors.HasErrors)
            {
                var sb = new StringBuilder();
                foreach (CompilerError ce in cr.Errors)
                {
                    sb.Append(ce);
                    sb.Append(Environment.NewLine);
                }
                throw new Exception(sb.ToString());
            }

            //生成代理实例，并调用方法
            Assembly assembly = cr.CompiledAssembly;
            Type t = assembly.GetType(@namespace + "." + classname, true, true);
            object obj = Activator.CreateInstance(t);
            MethodInfo mi = t.GetMethod(methodname);

            return mi.Invoke(obj, args);

            /*
            PropertyInfo propertyInfo = type.GetProperty(propertyname);
            return propertyInfo.GetValue(obj, null);
            */
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.InnerException.Message, new Exception(ex.InnerException.StackTrace));
            //}
        }

        private static string GetWsClassName(string wsUrl)
        {
            string[] parts = wsUrl.Split('/');
            string[] pps = parts[parts.Length - 1].Split('.');

            return pps[0];
        }

        #endregion
    }
}