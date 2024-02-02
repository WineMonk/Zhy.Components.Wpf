using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zhy.Demo._Model;
using Zhy.Demo._ViewModel;

namespace Zhy.Demo._View
{
    /// <summary>
    /// ZhyTreeViewWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ZhyTreeViewWindow : Window
    {
        private ZhyTreeViewWindowViewModel _vm;
        public ZhyTreeViewWindow()
        {
            InitializeComponent();
            _vm = new ZhyTreeViewWindowViewModel();
            this.DataContext = _vm;
        }

        private void addNodeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _vm.AddNode();
        }

        private void deleteNodeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _vm.DeleteNode();
        }

        private void updateNodeMenuItem_Click(object sender, RoutedEventArgs e)
        {
            _vm.UpdateNode();
        }

        private void tv_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            _vm.SelectedTreeItem = e.NewValue as Tree;
        }
    }
}
