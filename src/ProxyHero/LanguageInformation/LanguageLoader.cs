using System;
using System.Reflection;

namespace ProxyHero.LanguageInformation
{
    public class LanguageLoader
    {
        public void Load(object model, Type type, object Form)
        {
            FieldInfo[] mainFormFieldInfos = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
                //取得当然窗体的字段数组

            foreach (FieldInfo fieldInfo in mainFormFieldInfos)
            {
                foreach (PropertyInfo property in model.GetType().GetProperties())
                {
                    string Value;
                    if (property.GetValue(model, null) != null)
                    {
                        Value = property.GetValue(model, null).ToString();
                        if (fieldInfo.Name == property.Name) //控件名称（Name）
                        {
                            Object valueObject = Value;
                            Object fiObject = fieldInfo.GetValue(Form); //这里非要用this
                            PropertyInfo iMyProper = fieldInfo.FieldType.GetProperty("Text"); //取得这个控件的Text属性
                            iMyProper.SetValue(fiObject, valueObject, null); //设置这个属性的值
                            break;
                        }
                    }
                }
            }
        }
    }
}