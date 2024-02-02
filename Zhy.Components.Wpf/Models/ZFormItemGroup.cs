using System.Collections.Generic;
using Zhy.Components.Wpf.Views.Windows.ViewModel;

namespace Zhy.Components.Wpf.Models
{
    /// <summary>
    /// 表单项组
    /// </summary>
    public class ZFormItemGroup : ViewModelBase
    {
        /// <summary>
        /// 表单项组键
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { SetProperty(ref _key, value); }
        }
        /// <summary>
        /// 表单项组名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// 表单项
        /// </summary>
        public List<ZFormItem> ZFormItems
        {
            get { return _zFormItems; }
            set { SetProperty(ref _zFormItems, value); }
        }

        private string _key;
        private string _name;
        private List<ZFormItem> _zFormItems =
            new List<ZFormItem>();
    }
}
