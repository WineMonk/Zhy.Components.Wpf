/****************************************
 * FileName:	ZFormSortItemComparer
 * Creater: 	shaozhy
 * Create Date:	2023/8/29 11:15:05
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System.Collections.Generic;
using Zhy.Components.Wpf._Attribute;

namespace Zhy.Components.Wpf._Common._Comparer
{
    /// <summary>
    /// IZFormSortItem 排序比较器
    /// </summary>
    internal class ZFormSortItemComparer : IComparer<IZFormItemSortable>
    {
        private bool _isInverted;
        public ZFormSortItemComparer(bool isInverted = false)
        {
            _isInverted = isInverted;
        }

        public int Compare(IZFormItemSortable? x, IZFormItemSortable? y)
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
