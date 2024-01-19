/****************************************
 * FileName:	TestItem
 * Creater: 	shaozhy
 * Create Date:	2023/8/23 15:47:11
 * Version: 	v0.0.1
 * Description:	
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Zhy.Demo._Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TestItem : ObservableObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _path;
        public string Path
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }
        private bool _isChecked;
        public bool Checked
        {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value); }
        }

        public RelayCommand<TestItem> SelectPathCommand => new RelayCommand<TestItem>(SelectPath);

        public void SelectPath(TestItem testItem)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? dr = fileDialog.ShowDialog();
            if (!(bool)dr)
                return;
            testItem.Path = fileDialog.FileName;
        }
        
    }
}
