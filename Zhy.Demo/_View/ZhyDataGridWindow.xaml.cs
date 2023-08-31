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
using Zhy.Demo._ViewModel;

namespace Zhy.Demo._View
{
    /// <summary>
    /// ZhyDataGridWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ZhyDataGridWindow : Window
    {
        public ZhyDataGridWindow()
        {
            InitializeComponent();
            this.DataContext = new ZhyDataGridWindowViewModel();
        }
    }
}
