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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
