using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Loamen.Common;
using Microsoft.CSharp;

namespace Loamen.PluginFramework
{
    public class PluginEngine
    {
        #region Variables

        private readonly IApp application;
        private readonly string content;
        private readonly List<string> errors;
        private readonly string filename;
        private Assembly compiledAssembly;

        #endregion

        #region Properties

        /// <summary>
        ///     插件文件的完整名称
        /// </summary>
        public string FileName
        {
            get { return filename; }
        }

        /// <summary>
        ///     插件内容
        /// </summary>
        public string Content
        {
            get { return content; }
        }

        /// <summary>
        ///     错误信息
        /// </summary>
        public List<string> Errors
        {
            get { return errors; }
        }

        /// <summary>
        ///     编译过的程序集
        /// </summary>
        public Assembly CompiledAssembly
        {
            get { return compiledAssembly; }
        }

        #endregion

        #region Plugin Property

        /// <summary>
        ///     插件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     作者名称
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        ///     插件版本
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///     适用于代理公布器的代理
        /// </summary>
        public string LPHVersion { get; set; }

        /// <summary>
        ///     插件描述
        /// </summary>
        public string Description { get; set; }

        #endregion

        #region Constructor

        public PluginEngine(string fileName, IApp app)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Not found", fileName);
            var fi = new FileInfo(fileName);
            application = app;
            fileName = Path.GetFileName(fileName);
            filename = fi.FullName;
            content = LoadFile(fi.FullName);
            errors = new List<string>();

            if (Path.GetExtension(fileName) == ".dll")
            {
                try
                {
                    compiledAssembly = Assembly.LoadFile(fi.FullName);
                }
                catch
                {
                    //p_Errors.Add("Invalid assembly");
                }
            }
            else compiledAssembly = null;
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     编译
        /// </summary>
        /// <param name="filename">完整文件名</param>
        /// <returns></returns>
        public bool Compile(string fileName)
        {
            var provider = new CSharpCodeProvider(new Dictionary<string, string> {{"CompilerVersion", "v4.0"}});
            var parameters = new CompilerParameters();

            parameters.CompilerOptions = "/optimize";
            parameters.GenerateExecutable = false;
            parameters.GenerateInMemory = true;
            parameters.TreatWarningsAsErrors = true;
            parameters.ReferencedAssemblies.Add(Assembly.GetExecutingAssembly().Location);
            parameters.ReferencedAssemblies.AddRange(new[]
                {
                    "System.dll",
                    "System.Data.dll",
                    "System.Xml.dll",
                    "System.Drawing.dll",
                    "System.Windows.Forms.dll",
                    "System.Core.dll",
                    "System.Design.dll",
                    Application.StartupPath + @"\Loamen.PluginFramework.dll",
                    Application.StartupPath + @"\WeifenLuo.WinFormsUI.Docking.dll"
                });
            if (fileName != String.Empty) parameters.OutputAssembly = fileName;

            CompilerResults results = provider.CompileAssemblyFromSource(parameters, content);

            if (results.Errors.HasErrors)
            {
                errors.Clear();
                compiledAssembly = null;

                foreach (CompilerError error in results.Errors)
                {
                    errors.Add("Line " + error.Line + " Column " + error.Column + Environment.NewLine + error.ErrorText);
                }

                return false;
            }

            errors.Clear();
            compiledAssembly = results.CompiledAssembly;
            return true;
        }

        /// <summary>
        ///     运行
        /// </summary>
        /// <returns></returns>
        public bool Run()
        {
            if (compiledAssembly == null)
            {
                errors.Add("Assembly not compiled or invalid");
                return false;
            }

            foreach (Type type in compiledAssembly.GetExportedTypes())
            {
                foreach (Type iface in type.GetInterfaces())
                {
                    if (iface == typeof (IPlugin))
                    {
                        ConstructorInfo constructor = type.GetConstructor(Type.EmptyTypes);

                        if (constructor != null && constructor.IsPublic)
                        {
                            var pluginObject = constructor.Invoke(null) as IPlugin;
                            pluginObject.App = application;
                            Name = pluginObject.Name;
                            Author = pluginObject.Author;
                            Version = pluginObject.Version;
                            LPHVersion = pluginObject.LPHVersion;
                            Description = pluginObject.Description;
                            errors.Clear();
                            return true;
                        }
                        else
                        {
                            errors.Add("Constructor not found or is not public");
                            return false;
                        }
                    }
                }
            }

            errors.Add("Class does not inherit IPlugin interface");
            return false;
        }

        #endregion

        #region Private Methods

        /// <summary>
        ///     加载文件
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        private string LoadFile(string fileName)
        {
            using (var reader = new StreamReader(fileName))
            {
                string result = reader.ReadToEnd();
                if (!result.Contains("using"))
                {
                    result = SecurityHelper.DecryptDES(result, "Don.Yang");
                }
                return result;
            }
        }

        #endregion
    }
}