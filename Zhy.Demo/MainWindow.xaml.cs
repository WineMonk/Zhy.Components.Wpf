using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Zhy.Components.Wpf._Model;
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
            List<ZFormItem> zFormItems = Enumerable.Range(0, 100).Select(idx =>
            {
                ZFormItem zFormItem = new ZFormItem()
                {
                    Name = "Name" + idx,
                    Value = "Value" + idx,
                    IsReadOnly = idx % 5 == 0
                };
                if (idx % 3 == 0)
                {
                    zFormItem.IsReadOnly = false;
                    zFormItem.SetVerify((i) =>
                    {
                        return !string.IsNullOrEmpty(i.Value);
                    }, "值不能为空！");
                }
                if(idx == 1)
                {
                    zFormItem.OnValueChanging += (sender, e) =>
                    {
                        e.NewValue = e.OldValue;
                    };
                }
                if(idx == 2)
                {
                    zFormItem.OnValueChanging += (sender, e) =>
                    {
                        bool rst = DateTime.TryParse(e.NewValue?.ToString(), out DateTime dt);
                        if (rst)
                        {
                            e.NewValue = dt.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    };
                }
                if(idx == 3)
                {
                    zFormItem.OnValueChanging += (sender, e) =>
                    {
                        e.NewValue = "hhh";
                    };
                }
                return zFormItem;
            }).ToList();
            zFormItems[2].Value = DateTime.Now.ToString();
            ZFormGrid zFormGrid = new ZFormGrid(zFormItems);
            zFormGrid.ShowDialog();
        }

        private void buttonZhyFormDialog_Click(object sender, RoutedEventArgs e)
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

        private void buttonZhyFormGroupGrid_Click(object sender, RoutedEventArgs e)
        {
            ZFormItemGroup zFormItemGroup = new ZFormItemGroup();
            zFormItemGroup.Name = "第一组";
            zFormItemGroup.ZFormItems = Enumerable.Range(0, 6).Select(index => new ZFormItem()
            {
                Name = "Name" + index,
                Value = "Value" + index,
                IsReadOnly = index % 3 == 0
            }).ToList();
            ZFormItemGroup zFormItemGroup1 = new ZFormItemGroup();
            zFormItemGroup1.Name = "第二组";
            zFormItemGroup1.ZFormItems = Enumerable.Range(0, 6).Select(index => new ZFormItem()
            {
                Name = "Name" + index,
                Value = "Value" + index,
                IsReadOnly = index % 3 == 0
            }).ToList();
            ZFormItemGroup zFormItemGroup2 = new ZFormItemGroup();
            zFormItemGroup2.Name = "第三组";
            zFormItemGroup2.ZFormItems = Enumerable.Range(0, 100).Select(index => new ZFormItem()
            {
                Name = "Name" + index,
                Value = "Value" + index,
                IsReadOnly = index % 3 == 0
            }).ToList();
            ZFormGroupGrid zFormGroupGrid = new ZFormGroupGrid(
                new List<ZFormItemGroup>() { zFormItemGroup, zFormItemGroup1, zFormItemGroup2});
            zFormGroupGrid.IsReadOnly = false;
            zFormGroupGrid.ShowDialog();
        }
    }
}
