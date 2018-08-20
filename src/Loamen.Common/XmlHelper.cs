using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;

namespace Loamen.Common
{
    /// <summary>
    ///     XML操作类
    /// </summary>
    public class XmlHelper
    {
        #region 变量

        protected XmlDocument objXmlDoc = new XmlDocument();
        protected string strXmlFile;

        #endregion

        #region Constructors

        public XmlHelper(string XmlFile)
        {
            try
            {
                objXmlDoc.Load(XmlFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            strXmlFile = XmlFile;
        }

        #endregion

        #region StaticMethod

        /// <summary>
        ///     序列化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool Serialize(string path, object obj)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    IFormatter format = new BinaryFormatter();

                    format.Serialize(stream, obj);
                    stream.Close();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     序列化
        /// </summary>
        /// <param name="path"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool XmlSerialize(string path, object obj, Type type)
        {
            try
            {
                if (!File.Exists(path))
                {
                    var fi = new FileInfo(path);
                    if (!fi.Directory.Exists)
                    {
                        fi.Directory.Create();
                        GC.Collect();
                    }
                }

                using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    var format = new XmlSerializer(type);

                    format.Serialize(stream, obj);
                    stream.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                //TxtHelper.WriteLog(ex.Message);
                return false;
            }
        }

        /// <summary>
        ///     反序列化
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static object Deserialize(string path)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    IFormatter formatter = new BinaryFormatter();
                    stream.Seek(0, SeekOrigin.Begin);
                    object obj = formatter.Deserialize(stream);
                    stream.Close();
                    return obj;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        ///     反序列化
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static object XmlDeserialize(string path, Type type)
        {
            try
            {
                using (Stream stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var formatter = new XmlSerializer(type);
                    stream.Seek(0, SeekOrigin.Begin);
                    object obj = formatter.Deserialize(stream);
                    stream.Close();
                    return obj;
                }
            }
            catch(Exception ex)
            {
                Debug.Write(ex.Message);
                return null;
            }
        }

        //public static XmlDocument Serialize(T entity)
        //{
        //    XmlSerializer lizer = new XmlSerializer(entity.GetType());
        //    MemoryStream ms = new MemoryStream();
        //    lizer.Serialize(ms, entity);
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(ms);
        //    return doc;
        //}


        /// <summary>
        ///     反序列化
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object DeserializeXml(string xml, Type type)
        {
            try
            {
                var doc = new XmlDocument();
                doc.LoadXml(xml);
                var lizer = new XmlSerializer(type);
                using (var reader = new StringReader(doc.OuterXml))
                {
                    object obj = lizer.Deserialize(reader);
                    reader.Close();
                    return obj;
                }
            }
            catch
            {
                return null;
            }
        }

        #endregion

        #region PublicMethod

        /// <summary>
        ///     读取节点内容
        /// </summary>
        /// <param name="XmlPathNode"></param>
        /// <param name="Attrib"></param>
        /// <returns></returns>
        public string Read(string XmlPathNode, string Attrib)
        {
            string value = "";
            try
            {
                XmlNode xn = objXmlDoc.SelectSingleNode(XmlPathNode);
                value = (Attrib.Equals("") ? xn.InnerText : xn.Attributes[Attrib].Value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return value;
        }

        /// <summary>
        ///     获取节点下的DataSet
        /// </summary>
        /// <param name="XmlPathNode"></param>
        /// <returns></returns>
        public DataSet GetData(string XmlPathNode)
        {
            var ds = new DataSet();
            var read = new StringReader(objXmlDoc.SelectSingleNode(XmlPathNode).OuterXml);
            ds.ReadXml(read);
            return ds;
        }

        /// <summary>
        ///     替换某节点的内容
        /// </summary>
        /// <param name="XmlPathNode"></param>
        /// <param name="Content"></param>
        public void Replace(string XmlPathNode, string Content)
        {
            objXmlDoc.SelectSingleNode(XmlPathNode).InnerText = Content;
        }

        /// <summary>
        ///     删除节点
        /// </summary>
        /// <param name="Node"></param>
        public void Delete(string Node)
        {
            string mainNode = Node.Substring(0, Node.LastIndexOf("/"));
            objXmlDoc.SelectSingleNode(mainNode).RemoveChild(objXmlDoc.SelectSingleNode(Node));
        }

        /// <summary>
        ///     插入一节点和此节点的一子节点
        /// </summary>
        /// <param name="MainNode"></param>
        /// <param name="ChildNode"></param>
        /// <param name="Element"></param>
        /// <param name="Content"></param>
        public void InsertNode(string MainNode, string ChildNode, string Element, string Content)
        {
            XmlNode objRootNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objChildNode = objXmlDoc.CreateElement(ChildNode);
            objRootNode.AppendChild(objChildNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objChildNode.AppendChild(objElement);
        }

        /// <summary>
        ///     插入一个节点带一个属性
        /// </summary>
        /// <param name="MainNode"></param>
        /// <param name="Element"></param>
        /// <param name="Attrib"></param>
        /// <param name="AttribContent"></param>
        /// <param name="Content"></param>
        public void InsertElement(string MainNode, string Element, string Attrib, string AttribContent, string Content)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.SetAttribute(Attrib, AttribContent);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
        }

        /// <summary>
        ///     插入
        /// </summary>
        /// <param name="MainNode"></param>
        /// <param name="Element"></param>
        /// <param name="Content"></param>
        public void InsertElement(string MainNode, string Element, string Content)
        {
            XmlNode objNode = objXmlDoc.SelectSingleNode(MainNode);
            XmlElement objElement = objXmlDoc.CreateElement(Element);
            objElement.InnerText = Content;
            objNode.AppendChild(objElement);
        }

        /// <summary>
        ///     保存XML
        /// </summary>
        public void Save()
        {
            try
            {
                objXmlDoc.Save(strXmlFile);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            objXmlDoc = null;
        }

        #endregion

        #region Model与XML互相转换

        /// <summary>
        ///     Model转化为XML的方法
        /// </summary>
        /// <param name="model">要转化的Model</param>
        /// <returns></returns>
        public static string ModelToXML(object model)
        {
            var xmldoc = new XmlDocument();
            XmlElement ModelNode = xmldoc.CreateElement("Model");
            xmldoc.AppendChild(ModelNode);

            if (model != null)
            {
                foreach (PropertyInfo property in model.GetType().GetProperties())
                {
                    XmlElement attribute = xmldoc.CreateElement(property.Name);
                    if (property.GetValue(model, null) != null)
                        attribute.InnerText = property.GetValue(model, null).ToString();
                    else
                        attribute.InnerText = "[Null]";
                    ModelNode.AppendChild(attribute);
                }
            }

            return xmldoc.OuterXml;
        }

        /// <summary>
        ///     XML转化为Model的方法
        /// </summary>
        /// <param name="xml">要转化的XML</param>
        /// <param name="SampleModel">Model的实体示例，New一个出来即可</param>
        /// <returns></returns>
        public static object XMLToModel(string xml, object SampleModel)
        {
            if (string.IsNullOrEmpty(xml))
                return SampleModel;
            else
            {
                var xmldoc = new XmlDocument();
                xmldoc.LoadXml(xml);

                XmlNodeList attributes = xmldoc.SelectSingleNode("Model").ChildNodes;
                foreach (XmlNode node in attributes)
                {
                    foreach (PropertyInfo property in SampleModel.GetType().GetProperties())
                    {
                        if (node.Name == property.Name)
                        {
                            if (node.InnerText != "[Null]")
                            {
                                if (property.PropertyType == typeof (Guid))
                                    property.SetValue(SampleModel, new Guid(node.InnerText), null);
                                else
                                    property.SetValue(SampleModel,
                                                      Convert.ChangeType(node.InnerText, property.PropertyType), null);
                            }
                            else
                                property.SetValue(SampleModel, null, null);
                        }
                    }
                }
                return SampleModel;
            }
        }

        #endregion
    }
}