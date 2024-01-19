/****************************************
 * FileName:	ZFormItemGroup
 * Creater: 	shaozhy
 * Create Date:	2023/12/4 11:32:56
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zhy.Components.Wpf._Model
{
    /// <summary>
    /// 表单项组
    /// </summary>
    public class ZFormItemGroup : ObservableObject
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
