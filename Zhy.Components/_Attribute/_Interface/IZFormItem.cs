﻿/****************************************
 * FileName:	IZFormItem
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 10:07:14
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhy.Components._Attribute
{
    /// <summary>
    /// 
    /// </summary>
    public interface IZFormItem : IZFormSortItem
    {
        string Title { get; set; }
        string MemberPath { get; set; }
        bool IsReadOnlyItem { get; set; }
        object Data { get; set; }
    }
}