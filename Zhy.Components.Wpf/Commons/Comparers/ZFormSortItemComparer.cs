using System.Collections.Generic;
using Zhy.Components.Wpf.Attributes.Interfaces;

namespace Zhy.Components.Wpf.Commons.Comparers
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
