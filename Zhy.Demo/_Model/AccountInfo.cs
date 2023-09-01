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
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Zhy.Components.Wpf._Attribute._Base;
using Zhy.Components.Wpf._Attribute._ZFormColumn;
using Zhy.Components.Wpf._Attribute._ZFormItem;
using Zhy.Components.Wpf._Enum;
using Zhy.Components.Wpf._View._Window;

namespace Zhy.Demo._Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountInfo:ObservableObject
    {
        private int _no;
        private bool _isChecked;
        private string _username;
        private string _createDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        private string _role;
        private string _phone;
        private string _archivesPath;
        private List<Permission> _permission = new List<Permission>()
        {
            new Permission(false, "人事审批","人事审批权限"),
            new Permission(false, "财务审批","财务审批权限"),
            new Permission(false, "后勤审批","后勤审批权限"),
            new Permission(false, "行政审批","行政审批权限"),
            new Permission(false, "公章审批","公章审批权限")
        };
        private List<string> _roles = new List<string>()
        {
            "系统管理员","主管","员工"
        };

        [ZFormTextColumn("序 号", Index = 1, IsReadOnlyColumn = true, IsHideFormItem = true, WidthUnit = DataGridLengthUnitType.Auto)]
        public int NO { get => _no; set => SetProperty(ref _no, value); }
        [ZFormCheckColumn("选 择", Index = 0, IsHideFormItem = true, WidthUnit = DataGridLengthUnitType.Auto)]
        public bool IsChecked { get => _isChecked; set => SetProperty(ref _isChecked, value); }
        [ZFormTextColumn("姓 名", Index = 1, WidthUnit = DataGridLengthUnitType.Auto, IsSearchProperty = true)]
        public string Username { get => _username; set => SetProperty(ref _username, value); }
        [ZFormTextColumn("创建日期", Index = 2, IsReadOnlyColumn =true, WidthUnit = DataGridLengthUnitType.Auto)]
        public string CreateDate { get => _createDate; set => SetProperty(ref _createDate, value); }
        [ZFormComboColumn("角 色", Index = 3, IsSearchProperty = true, ItemsSourceProperty = nameof(Roles), WidthUnit = DataGridLengthUnitType.Auto)]
        public string Role { get => _role; set => SetProperty(ref _role, value); }
        [ZFormTextColumn("联系电话", Index = 4, IsSearchProperty = true, WidthUnit = DataGridLengthUnitType.Auto)]
        public string Phone { get => _phone; set => SetProperty(ref _phone, value); }
        [ZFormTextButtonColumn("档案路径", Index = 5, ButtonContent = "更 改",RelayCommandName = nameof(CommandModifyArchivesPath), WidthUnit = DataGridLengthUnitType.Star)]
        public string ArchivesPath { get => _archivesPath; set => SetProperty(ref _archivesPath, value); }
        [ZFormMultiCheckItem("权 限", "IsChecked", "Name")]
        public List<Permission> Permission { get => _permission; set => SetProperty(ref _permission, value); }

        [ZFormFuncButton(ButtonContent = "查看信息", Index = 0, ButtonStyle = ZFormButtonStyle.InfoButton)]
        public RelayCommand<object[]> CommandViewItem => new RelayCommand<object[]>(ViewItem);
        [ZFormFuncButton(ButtonContent = "删 除", Index = 1, ButtonStyle = ZFormButtonStyle.ErrorButton)]
        public RelayCommand<object[]> CommandDeleteItem => new RelayCommand<object[]>(DeleteItem);

        public RelayCommand<AccountInfo> CommandModifyArchivesPath => new RelayCommand<AccountInfo>(ModifyArchivesPath);
        public List<string> Roles { get => _roles; set => _roles = value; }

        [ZFormFuncButton(ButtonContent = "添 加", Index = 0, ButtonStyle = ZFormButtonStyle.InfoButton)]
        public static void AddItem(IEnumerable items)
        {
            AccountInfo accountInfo = new AccountInfo();
            ZFormDialog zFormDialog = new ZFormDialog(accountInfo);
            zFormDialog.Title = "Add";
            bool dr = (bool)zFormDialog.ShowDialog();
            if (!dr)
                return;
            IList<AccountInfo> accountInfos = items as IList<AccountInfo>;
            accountInfos.Add(accountInfo);
            accountInfo.NO = accountInfos.IndexOf(accountInfo) + 1;
        }
        [ZFormFuncButton(ButtonContent = "批量删除", Index = 1, ButtonStyle = ZFormButtonStyle.ErrorButton)]
        public static void BatchDeleteItem(IEnumerable items)
        {
            List<AccountInfo> rm = new List<AccountInfo>();
            foreach (AccountInfo accountInfo in items)
                if (accountInfo.IsChecked)
                    rm.Add(accountInfo);
            if (rm.Count < 1)
                return;
            MessageBoxResult messageBoxResult = MessageBox.Show("确认删除？！", "提示", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes)
                return;
            IList<AccountInfo> accountInfos = items as IList<AccountInfo>;
            foreach (var item in rm)
                accountInfos.Remove(item);
        }

        private void DeleteItem(object[] param)
        {
            AccountInfo accountInfo = param[0] as AccountInfo;
            IList<AccountInfo> accountInfos = param[1] as IList<AccountInfo>;
            MessageBoxResult messageBoxResult = MessageBox.Show("确认删除？！", "提示", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes)
                return;
            accountInfos.Remove(accountInfo);
        }
        private void ViewItem(object[] param)
        {
            AccountInfo accountInfo = param[0] as AccountInfo;
            if (accountInfo == null)
                return;
            string msg = null;
            PropertyInfo[] propertyInfos = accountInfo.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                ZFormItemAttribute? zFormItemAttribute = propertyInfo.GetCustomAttribute<ZFormItemAttribute>();
                if (zFormItemAttribute == null)
                    continue;
                if (zFormItemAttribute is ZFormMultiCheckItemAttribute)
                {
                    ZFormMultiCheckItemAttribute zFormMultiCheckItem = (ZFormMultiCheckItemAttribute)zFormItemAttribute;
                    PropertyInfo? propertyInfo1 = accountInfo.GetType().GetProperty(propertyInfo.Name);
                    object? val = propertyInfo1.GetValue(accountInfo);
                    IList list = val as IList;
                    msg += zFormItemAttribute.Title + ": ";
                    foreach (var item in list)
                    {
                        PropertyInfo? propertyInfo2 = item.GetType().GetProperty(zFormMultiCheckItem.MemberPath);
                        PropertyInfo? propertyInfo3 = item.GetType().GetProperty(zFormMultiCheckItem.ContentProperty);
                        object? vval = propertyInfo2.GetValue(item);
                        bool check = (bool)vval;
                        if (check)
                            msg += propertyInfo3.GetValue(item) + " ";
                    }
                }
                else if (zFormItemAttribute is ZFormTextButtonColumnAttribute || zFormItemAttribute is ZFormComboColumnAttribute)
                {
                    if (string.IsNullOrEmpty(zFormItemAttribute.MemberPath))
                        msg += zFormItemAttribute.Title + ": " + propertyInfo.GetValue(accountInfo) + "\r\n";
                    else
                    {
                        object? val = propertyInfo.GetValue(accountInfo);
                        if (val == null) continue;
                        PropertyInfo? propertyInfo2 = val.GetType().GetProperty(zFormItemAttribute.MemberPath);
                        msg += zFormItemAttribute.Title + ": " + propertyInfo2.GetValue(val) + "\r\n";
                    }
                }
                else
                {
                    msg += zFormItemAttribute.Title + ": " + propertyInfo.GetValue(accountInfo) + "\r\n";
                }
            }
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
