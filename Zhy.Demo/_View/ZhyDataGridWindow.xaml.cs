using System.Windows;
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
