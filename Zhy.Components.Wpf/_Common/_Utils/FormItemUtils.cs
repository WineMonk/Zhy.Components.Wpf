/****************************************
 * FileName:	FormItemUtils
 * Creater: 	shaozhy
 * Create Date:	2023/9/14 15:38:19
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Zhy.Components.Wpf._Attribute._Base;
using Zhy.Components.Wpf._Attribute._ZFormColumn;
using Zhy.Components.Wpf._Attribute._ZFormItem;

namespace Zhy.Components.Wpf._Common._Utils
{
    /// <summary>
    /// 表单项工具类
    /// </summary>
    public class FormItemUtils
    {
        /// <summary>
        /// 打印表单项
        /// </summary>
        /// <param name="obj">表单项</param>
        /// <returns>表单项信息</returns>
        public static string Print(object obj)
        {
            string msg = string.Empty;
            PropertyInfo[] propertyInfos = obj.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                ZFormItemAttribute? zFormItemAttribute = propertyInfo.GetCustomAttribute<ZFormItemAttribute>();
                if (zFormItemAttribute == null)
                    continue;
                if (zFormItemAttribute is ZFormMultiCheckItemAttribute)
                {
                    ZFormMultiCheckItemAttribute zFormMultiCheckItem = (ZFormMultiCheckItemAttribute)zFormItemAttribute;
                    PropertyInfo? propertyInfo1 = obj.GetType().GetProperty(propertyInfo.Name);
                    object? val = propertyInfo1.GetValue(obj);
                    IList list = val as IList;
                    msg += zFormItemAttribute.Title + ": ";
                    foreach (var item in list)
                    {
                        PropertyInfo? propertyInfo2 = item.GetType().GetProperty(zFormMultiCheckItem.MemberPath);
                        PropertyInfo? propertyInfo3 = item.GetType().GetProperty(zFormMultiCheckItem.ContentProperty);
                        object? vval = propertyInfo2.GetValue(item);
                        bool check = (bool)vval;
                        if (check)
                            msg += propertyInfo3.GetValue(item) + " ";
                    }
                }
                else if (zFormItemAttribute is ZFormTextButtonColumnAttribute || zFormItemAttribute is ZFormComboColumnAttribute)
                {
                    if (string.IsNullOrEmpty(zFormItemAttribute.MemberPath))
                        msg += zFormItemAttribute.Title + ": " + propertyInfo.GetValue(obj) + "\r\n";
                    else
                    {
                        object? val = propertyInfo.GetValue(obj);
                        if (val == null) continue;
                        PropertyInfo? propertyInfo2 = val.GetType().GetProperty(zFormItemAttribute.MemberPath);
                        msg += zFormItemAttribute.Title + ": " + propertyInfo2.GetValue(val) + "\r\n";
                    }
                }
                else
                {
                    msg += zFormItemAttribute.Title + ": " + propertyInfo.GetValue(obj) + "\r\n";
                }
            }
            return msg;
        }
    }
}
