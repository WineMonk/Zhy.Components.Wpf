/****************************************
 * FileName:	Permission
 * Creater: 	shaozhy
 * Create Date:	2023/8/30 17:25:36
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhy.Demo._Model
{
    /// <summary>
    /// 
    /// </summary>
    public class Permission:ObservableObject
    {
        private bool _isChecked = false;
        public bool IsChecked 
        {
            get { return _isChecked; }
            set  { SetProperty(ref _isChecked, value); }
        }
        public string Name { get; set; }
        public string Desc { get; set; }

        public Permission(bool isChecked, string name, string desc)
        {
            IsChecked = isChecked;
            Name = name;
            Desc = desc;
        }
    }
}
