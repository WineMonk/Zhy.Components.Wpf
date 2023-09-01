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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Zhy.Components.Wpf._View._Window;
using Zhy.Demo._Common;
using Zhy.Demo._Model;
using Zhy.Demo._View;

namespace Zhy.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonZhyDataGrid_Click(object sender, RoutedEventArgs e)
        {
            ZhyDataGridWindow zhyDataGridWindow = new ZhyDataGridWindow();
            zhyDataGridWindow.ShowDialog();
        }

        private void buttonZhyFormGrid_Click(object sender, RoutedEventArgs e)
        {
            AccountInfo accountInfo = new AccountInfo();
            accountInfo = new AccountInfo();
            accountInfo.Phone = CommonUtils.GenerateRandomPhoneNumber();
            accountInfo.Username = CommonUtils.GenerateRandomName();
            accountInfo.ArchivesPath = "D:\\admin\\admin.ad";
            accountInfo.Role = accountInfo.Roles.FirstOrDefault(r => r == "员工");
            accountInfo.Permission.ForEach(permission => permission.IsChecked = true);
            ZFormDialog zFormDialog = new ZFormDialog(accountInfo);
            bool dr = (bool)zFormDialog.ShowDialog();
            if (!dr)
                return;
        }
    }
}
