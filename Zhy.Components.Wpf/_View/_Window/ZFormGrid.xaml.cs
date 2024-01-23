using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Windows;
using Zhy.Components.Wpf._Common._Utils;
using Zhy.Components.Wpf._Model;

namespace Zhy.Components.Wpf._View._Window
{
    /// <summary>
    /// ZFormGrid.xaml 的交互逻辑
    /// </summary>
    public partial class ZFormGrid : Window
    {
        private ZFormGridViewModel vm = null;

        /// <summary>
        /// ZFormGrid构造函数
        /// </summary>
        /// <param name="zFormItems">表单项</param>
        public ZFormGrid(List<ZFormItem> zFormItems)
        {
            InitializeComponent();
            vm = new ZFormGridViewModel(zFormItems, dr => this.DialogResult = dr);
            this.DataContext = vm;
        }
    }

    internal class ZFormGridViewModel : ObservableObject
    {
        private List<ZFormItem> zFormItems = new List<ZFormItem>();
        public List<ZFormItem> ZFormItems
        {
            get { return zFormItems; }
            set { SetProperty(ref zFormItems, value); }
        }

        private Action<bool> _actionDr;

        internal ZFormGridViewModel(List<ZFormItem> zFormItems, Action<bool> actionDr)
        {
            for (int i = 0; i < zFormItems.Count; i++)
            {
                zFormItems[i].Oid = i + 1;
            }
            ZFormItems = zFormItems;
            _actionDr = actionDr;
        }

        public RelayCommand OKCommand => new RelayCommand(OK);
        public RelayCommand CancelCommand => new RelayCommand(Cancel);
        private void OK()
        {
            foreach (var item in zFormItems)
            {
                if (item.VerifyFunc != null)
                {
                    if (item.VerifyFunc(item))
                    {
                        continue;
                    }
                    if (!string.IsNullOrEmpty(item.Tip))
                    {
                        MessageBox.Show(item.Tip, $"第 {item.Oid} 项值异常！", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    return;
                }
            }
            _actionDr(true);
        }
        private void Cancel()
        {
            _actionDr(false);
        }
    }
}
