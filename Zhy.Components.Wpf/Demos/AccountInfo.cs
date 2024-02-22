﻿using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Zhy.Components.Wpf.Attributes.ZFormColumns;
using Zhy.Components.Wpf.Commons.Utils;
using Zhy.Components.Wpf.Enums;
using Zhy.Components.Wpf.Views.Windows.ViewModel;
using Zhy.Components.Wpf.Views.Windows.Zhys;

namespace Zhy.Components.Wpf.Demos
{
    /// <summary>
    /// 账户信息
    /// </summary>
    public class AccountInfo : ViewModelBase
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
        /// <summary>
        /// 序号
        /// </summary>
        [ZFormTextColumn("序 号", Index = 1, IsReadOnlyColumn = true, IsHideFormItem = true, Width = 30, WidthUnit = DataGridLengthUnitType.Pixel)]
        public int NO { get => _no; set => SetProperty(ref _no, value); }
        /// <summary>
        /// 选择
        /// </summary>
        [ZFormCheckColumn("选 择", Index = 0, IsHideFormItem = true, Width = 30, WidthUnit = DataGridLengthUnitType.Pixel)]
        public bool IsChecked { get => _isChecked; set => SetProperty(ref _isChecked, value); }
        /// <summary>
        /// 用户名
        /// </summary>
        [ZFormTextColumn("用户名", Index = 1, IsHideFormColumn = false, IsReadOnlyColumn = false, IsHideFormItem = false, IsReadOnlyItem = false, IsSearchProperty = true, MemberPath = null, Width = 100, WidthUnit = DataGridLengthUnitType.Pixel)]
        public virtual string Username { get => _username; set => SetProperty(ref _username, value); }
        /// <summary>
        /// 创建日期
        /// </summary>
        [ZFormDateColumn("创建日期", Index = 2, Width = 200, WidthUnit = DataGridLengthUnitType.Pixel)]
        public string CreateDate { get => _createDate; set => SetProperty(ref _createDate, value); }
        /// <summary>
        /// 用户角色
        /// </summary>
        [ZFormComboColumn("角 色", Index = 3, IsSearchProperty = true, ItemsSourceProperty = nameof(Roles), Width = 100, WidthUnit = DataGridLengthUnitType.Pixel)]
        public string Role { get => _role; set => SetProperty(ref _role, value); }
        /// <summary>
        /// 全部角色
        /// </summary>
        public List<string> Roles { get => _roles; set => _roles = value; }
        /// <summary>
        /// 可用状态
        /// </summary>
        [ZFormCheckColumn("状 态", Index = 3, Width = 30, WidthUnit = DataGridLengthUnitType.Pixel)]
        public bool Enable { get => _enable; set => SetProperty(ref _enable, value); }
        /// <summary>
        /// 联系电话
        /// </summary>
        [ZFormTextColumn("联系电话", Index = 4, IsSearchProperty = true, Width = 100, WidthUnit = DataGridLengthUnitType.Pixel)]
        public string Phone { get => _phone; set => SetProperty(ref _phone, value); }
        /// <summary>
        /// 档案路径
        /// </summary>
        [ZFormTextButtonColumn("档案路径", Index = 5, ButtonContent = "更 改", RelayCommandName = nameof(CommandModifyArchivesPath), Width = 200, WidthUnit = DataGridLengthUnitType.Pixel)]
        public string ArchivesPath { get => _archivesPath; set => SetProperty(ref _archivesPath, value); }
        /// <summary>
        /// 更改档案路径命令
        /// </summary>
        public RelayCommand CommandModifyArchivesPath => new RelayCommand(ModifyArchivesPath);
        private void ModifyArchivesPath(object param)
        {
            AccountInfo accountInfo = param as AccountInfo;
            if (accountInfo == null)
            {
                return;
            }
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? dr = fileDialog.ShowDialog();
            if (dr == false)
            {
                return;
            }
            accountInfo.ArchivesPath = fileDialog.FileName;
        }
        /// <summary>
        /// 权限信息
        /// </summary>
        [ZFormMultiCheckColumn("权 限", "IsChecked", "Name", IsSearchProperty = true, WidthUnit = DataGridLengthUnitType.Auto)]
        public List<Permission> Permission { get => _permission; set => SetProperty(ref _permission, value); }
        /// <summary>
        /// 查看信息命令
        /// </summary>
        [ZFormOperColumnButton("查看", Index = 0, ButtonStyle = ZFormButtonStyle.InfoButton)]
        public RelayCommand CommandViewItem => new RelayCommand(ViewItem);
        private void ViewItem(object param)
        {
            Tuple<object, IList> tuple = param as Tuple<object, IList>;
            if (tuple == null)
            {
                return;
            }
            AccountInfo accountInfo = tuple.Item1 as AccountInfo;
            if (accountInfo == null)
            {
                return;
            }
            string msg = FormItemUtils.Print(accountInfo);
            MessageBox.Show(msg, "属性信息", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        /// <summary>
        /// 更新命令
        /// </summary>
        [ZFormOperColumnButton("更新", Index = 0, ButtonStyle = ZFormButtonStyle.WarnButton)]
        public RelayCommand CommandUpdateItem => new RelayCommand(UpdateItem);
        private void UpdateItem(object param)
        {
            Tuple<object, IList> tuple = param as Tuple<object, IList>;
            MessageBox.Show("更新");
        }
        /// <summary>
        /// 导出命令
        /// </summary>
        [ZFormOperColumnButton("导出", Index = 0, ButtonStyle = ZFormButtonStyle.SuccessButton)]
        public RelayCommand CommandExportItem => new RelayCommand(ExportItem);
        private void ExportItem(object param)
        {
            Tuple<object, IList> tuple = param as Tuple<object, IList>;
            MessageBox.Show("导出");
        }
        /// <summary>
        /// 删除命令
        /// </summary>
        [ZFormOperColumnButton("删 除", Index = 1, ButtonStyle = ZFormButtonStyle.ErrorButton)]
        public RelayCommand CommandDeleteItem => new RelayCommand(DeleteItem);
        private void DeleteItem(object param)
        {
            Tuple<object, IList> tuple = param as Tuple<object, IList>;
            if (tuple == null)
            {
                return;
            }
            AccountInfo accountInfo = tuple.Item1 as AccountInfo;
            IList accountInfos = tuple.Item2 as IList;
            MessageBoxResult messageBoxResult = MessageBox.Show("确认删除？！", "提示", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (messageBoxResult != MessageBoxResult.Yes)
            {
                return;
            }
            accountInfos?.Remove(accountInfo);
        }
        /// <summary>
        /// 全选命令
        /// </summary>
        [ZFormToolButton("全 选", Index = 0, Dock = Dock.Right, ButtonStyle = ZFormButtonStyle.DefaultButton, Location = ButtonLocation.Bottom)]
        public RelayCommand CommandCheckTotalItem => new RelayCommand(CheckTotalItem);
        private void CheckTotalItem(object param)
        {
            IList items = param as IList;
            if (items == null) return;
            foreach (AccountInfo item in items)
            {
                if (item == null) continue;
                item.IsChecked = true;
            }
        }
        /// <summary>
        /// 全不选命令
        /// </summary>
        [ZFormToolButton("全不选", Index = 1, Location = ButtonLocation.Bottom)]
        public RelayCommand CommandUncheckTotalItem => new RelayCommand(UncheckTotalItem);
        private void UncheckTotalItem(object param)
        {
            IList items = param as IList;
            if (items == null) return;
            foreach (AccountInfo item in items)
            {
                if (item == null) continue;
                item.IsChecked = false;
            }
        }
        /// <summary>
        /// 添加用户命令
        /// </summary>
        [ZFormToolButton("添 加", Index = 0, ButtonStyle = ZFormButtonStyle.InfoButton)]
        public RelayCommand CommandAddItem => new RelayCommand(AddItem);
        private async void AddItem(object param)
        {
            IList items = param as IList;
            if (items == null) return;
            AccountInfo accountInfo = new AccountInfo();
            await Task.Run(() =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ZFormDialog zFormDialog = new ZFormDialog(accountInfo);
                    zFormDialog.Title = "Add";
                    bool? dr = zFormDialog.ShowDialog();
                    if (dr == false)
                    {
                        return;
                    }
                });
            });
            items.Add(accountInfo);
            accountInfo.NO = items.IndexOf(accountInfo) + 1;
        }
        /// <summary>
        /// 批量删除命令
        /// </summary>
        [ZFormToolButton("批量删除", Index = 1, ButtonStyle = ZFormButtonStyle.ErrorButton)]
        public RelayCommand CommandBatchDeleteItem => new RelayCommand(BatchDeleteItem);
        private void BatchDeleteItem(object param)
        {
            IList items = param as IList;
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
    }
}
