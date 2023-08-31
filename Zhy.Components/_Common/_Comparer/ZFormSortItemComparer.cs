/****************************************
 * FileName:	ZFormSortItemComparer
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 11:15:05
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhy.Components._Attribute;

namespace Zhy.Components._Common._Comparer
{
    /// <summary>
    /// 
    /// </summary>
    public class ZFormSortItemComparer : IComparer<IZFormSortItem>
    {
        private bool _isInverted;
        public ZFormSortItemComparer(bool isInverted = false) 
        {
            _isInverted = isInverted;
        }

        public int Compare(IZFormSortItem? x, IZFormSortItem? y)
        {
            if (x == null && y == null) return _isInverted ? -1 : 1;
            else if (x == null && y != null) return _isInverted ? -1 : 1;
            else if (x != null && y == null) return _isInverted ? 1 : -1;
            else if (x.Index == y.Index) return _isInverted ? -1 : 1;
            else if (x.Index == -1) return _isInverted ? -1 : 1;
            else if (y.Index == -1) return _isInverted ? 1 : -1;
            else if (x.Index > y.Index) return _isInverted ? -1 : 1;
            else return _isInverted ? 1 : -1;
        }
    }
}
