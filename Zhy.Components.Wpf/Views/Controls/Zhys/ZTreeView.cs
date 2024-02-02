using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zhy.Components.Wpf.Commons.Utils;

namespace Zhy.Components.Wpf.Views.Controls.Zhys
{
    public class ZTreeView : TreeView
    {
        public ZTreeView():base()
        {
            if (IZFormItemUtils.FindResource("ZTreeViewItemStyle") is Style zTreeViewItemStyle)
            {
                ItemContainerStyle = zTreeViewItemStyle;
            }
        }
    }
}
