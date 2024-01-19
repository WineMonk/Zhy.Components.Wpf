/****************************************
 * FileName:	AccountInfo
 * Creater: 	shaozhy
 * Create Date:	2023/8/30 17:21:29
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zhy.Components.Wpf._Attribute._ZFormColumn;
using Zhy.Components.Wpf._Common._Utils;
using Zhy.Components.Wpf._Enum;
using Zhy.Components.Wpf._View._Window;

namespace Zhy.Demo._Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountInfo : ObservableObject
    {
        private int _no;
        private bool _isChecked;
        private string _username;
        private string _createDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        private string _role;
        private bool _enable;
        private string _phone;
        private string _archivesPath;
        private List<Permission> _permission = new List<Permission>()
        {
            new Permission(false, "测试项1","测试项1"),
            new Permission(false, "测试项2","测试项2"),
            new Permission(false, "测试项3","测试项3"),
            new Permission(false, "测试项4","测试项4"),
            new Permission(false, "测试项5","测试项5")
        };
        private List<string> _roles = new List<string>()
        {
            "系统管理员", "角色1"
        };

        [ZFormTextColumn("序 号", Index = 1, IsReadOnlyColumn = true, IsHideFormItem = true, WidthUnit = DataGridLengthUnitType.Auto)]
        public int NO { get => _no; set => SetProperty(ref _no, value); }
        [ZFormCheckColumn("选 择", Index = 0, IsHideFormItem = true, WidthUnit = DataGridLengthUnitType.Auto)]
        public bool IsChecked { get => _isChecked; set => SetProperty(ref _isChecked, value); }
        [ZFormTextColumn("姓 名", Index = 1, WidthUnit = DataGridLengthUnitType.Auto, IsSearchProperty = true)]
        public virtual string Username { get => _username; set => SetProperty(ref _username, value); }
        [ZFormDateColumn("创建日期", Index = 2, WidthUnit = DataGridLengthUnitType.Auto)]
        public string CreateDate { get => _createDate; set => SetProperty(ref _createDate, value); }
        [ZFormComboColumn("角 色", Index = 3, IsSearchProperty = true, ItemsSourceProperty = nameof(Roles), WidthUnit = DataGridLengthUnitType.Auto)]
        public string Role { get => _role; set => SetProperty(ref _role, value); }
        [ZFormCheckColumn("状 态", Index = 3, WidthUnit = DataGridLengthUnitType.Auto)]
        public bool Enable { get => _enable; set => SetProperty(ref _enable, value); }
        [ZFormTextColumn("联系电话", Index = 4, IsSearchProperty = true, WidthUnit = DataGridLengthUnitType.Auto)]
        public string Phone { get => _phone; set => SetProperty(ref _phone, value); }
        [ZFormTextButtonColumn("档案路径", Index = 5, ButtonContent = "更 改", RelayCommandName = nameof(CommandModifyArchivesPath), WidthUnit = DataGridLengthUnitType.Star)]
        public string ArchivesPath { get => _archivesPath; set => SetProperty(ref _archivesPath, value); }
        [ZFormMultiCheckColumn("权 限", "IsChecked", "Name", IsSearchProperty = true, WidthUnit = DataGridLengthUnitType.Auto)]
        public List<Permission> Permission { get => _permission; set => SetProperty(ref _permission, value); }

        [ZFormOperColumnButton("查看信息", Index = 0, ButtonStyle = ZFormButtonStyle.InfoButton)]
        public RelayCommand<Tuple<object?, IList?>> CommandViewItem => new RelayCommand<Tuple<object?, IList?>>(ViewItem);
        [ZFormOperColumnButton("删 除", Index = 1, ButtonStyle = ZFormButtonStyle.ErrorButton)]
        public RelayCommand<Tuple<object?, IList?>> CommandDeleteItem => new RelayCommand<Tuple<object?, IList?>>(DeleteItem);

        public RelayCommand<AccountInfo> CommandModifyArchivesPath => new RelayCommand<AccountInfo>(ModifyArchivesPath);
        public List<string> Roles { get => _roles; set => _roles = value; }

        [ZFormToolButton("全 选", Index = 0, Location = ButtonLocation.Bottom)]
        public RelayCommand<IList?> CommandCheckTotalItem => new RelayCommand<IList?>(CheckTotalItem);
        public void CheckTotalItem(IList? items)
        {
            if (items == null) return;
            foreach (AccountInfo? item in items)
            {
                if (item == null) continue;
                item.IsChecked = true;
            }
        }
        [ZFormToolButton("全不选", Index = 1, Location = ButtonLocation.Bottom)]
        public RelayCommand<IList?> CommandUncheckTotalItem => new RelayCommand<IList?>(UncheckTotalItem);
        private void UncheckTotalItem(IList? items)
        {
            if (items == null) return;
            foreach (AccountInfo? item in items)
            {
                if (item == null) continue;
                item.IsChecked = false;
            }
        }
        [ZFormToolButton("添 加", Index = 0, ButtonStyle = ZFormButtonStyle.InfoButton)]
        public RelayCommand<IList?> CommandAddItem => new RelayCommand<IList?>(AddItem);
        private async void AddItem(IList? items)
        {
            if (items == null) return;
            AccountInfo accountInfo = new AccountInfo();
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ZFormDialog zFormDialog = new ZFormDialog(accountInfo);
                    zFormDialog.Title = "Add";
                    bool dr = (bool)zFormDialog.ShowDialog();
                    if (!dr)
                        return;
                });
            });
            items.Add(accountInfo);
            accountInfo.NO = items.IndexOf(accountInfo) + 1;
        }
        [ZFormToolButton("批量删除", Index = 1, ButtonStyle = ZFormButtonStyle.ErrorButton)]
        public RelayCommand<IList?> CommandBatchDeleteItem => new RelayCommand<IList?>(BatchDeleteItem);
        private void BatchDeleteItem(IList? items)
        {
            if (items == null) return;
            List<AccountInfo> rm = new List<AccountInfo>();
            foreach (AccountInfo accountInfo in items)
                if (accountInfo?.IsChecked == true)
                    rm.Add(accountInfo);
            if (rm.Count < 1)
                return;
            MessageBoxResult messageBoxResult = MessageBox.Show("确认删除？！", "提示", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes)
                return;
            foreach (var item in rm)
                items.Remove(item);
        }

        private void DeleteItem(Tuple<object?, IList?> param)
        {
            AccountInfo accountInfo = param.Item1 as AccountInfo;
            IList? accountInfos = param.Item2 as IList;
            MessageBoxResult messageBoxResult = MessageBox.Show("确认删除？！", "提示", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes)
                return;
            accountInfos?.Remove(accountInfo);
        }
        private void ViewItem(Tuple<object?, IList?> param)
        {
            AccountInfo accountInfo = param.Item1 as AccountInfo;
            if (accountInfo == null)
                return;
            string msg = FormItemUtils.Print(accountInfo);
            MessageBox.Show(msg, "属性信息", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        private void ModifyArchivesPath(AccountInfo accountInfo)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? dr = fileDialog.ShowDialog();
            if (!(bool)dr)
                return;
            accountInfo.ArchivesPath = fileDialog.FileName;
        }
    }
}
