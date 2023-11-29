/****************************************
 * FileName:	TypeUtils
 * Creater: 	shaozhy
 * Create Date:	2023/9/8 10:46:25
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Zhy.Components.Wpf._Common._Utils
{
    /// <summary>
    /// 
    /// </summary>
    internal class TypeUtils
    {
        public static Type GetIListItemType(IEnumerable list)
        {
            if (!list.GetType().IsGenericType)
                return null;
            Type sourceItemType = list.GetType().GetGenericArguments()[0];
            return sourceItemType;
        }

        public static bool IsAsyncMethod(MethodInfo method)
        {
            Type attType = typeof(AsyncStateMachineAttribute);
            var attrib = method.GetCustomAttribute(attType) as AsyncStateMachineAttribute;
            return (attrib != null);
        }
    }
}
