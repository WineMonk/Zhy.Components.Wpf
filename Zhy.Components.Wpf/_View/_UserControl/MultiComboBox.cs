/****************************************
 * FileName:	MultiComboBox
 * Creater: 	shaozhy
 * Create Date:	2023/9/7 13:50:29
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Collections;
using Zhy.Components.Wpf._Common._Utils;

namespace Zhy.Components.Wpf._View._UserControl
{
    /// <summary>
    /// 
    /// </summary>
    internal class MultiComboBox:ComboBox
    {
        static MultiComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiComboBox), new FrameworkPropertyMetadata(typeof(MultiComboBox)));
        }

        public MultiComboBox() : base()
        {
            CheckedMemberPath = "IsChecked";
        }


        private static void OnChekedItemsPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            d.SetValue(e.Property, e.NewValue);
        }

        /// <summary>
        /// 选中项列表
        /// </summary>
        public IList ChekedItems
        {
            get { return (IList)GetValue(ChekedItemsProperty); }
            set { SetValue(ChekedItemsProperty, value); }
        }
        /// <summary>
        /// 选中项列表依赖项
        /// </summary>
        public static readonly DependencyProperty ChekedItemsProperty =
            DependencyProperty.Register("ChekedItems", typeof(IList), typeof(MultiComboBox), new PropertyMetadata(null, OnChekedItemsPropertyChanged));


        /// <summary>
        /// 源数据项选中属性路径
        /// </summary>
        public string CheckedMemberPath
        {
            get { return (string)GetValue(CheckedMemberPathProperty); }
            set { SetValue(CheckedMemberPathProperty, value); }
        }
        /// <summary>
        /// 源数据项选中属性路径依赖属性
        /// </summary>
        public static readonly DependencyProperty CheckedMemberPathProperty =
            DependencyProperty.Register("CheckedMemberPath", typeof(string), typeof(MultiComboBox), new PropertyMetadata(string.Empty));



        /// <summary>
        /// ListBox竖向列表
        /// </summary>
        private ListBox _ListBoxV;

        /// <summary>
        /// ListBox横向列表
        /// </summary>
        private ListBox _ListBoxH;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _ListBoxV = Template.FindName("PART_ListBox", this) as ListBox;
            _ListBoxH = Template.FindName("PART_ListBoxChk", this) as ListBox;
            _ListBoxH.ItemsSource = ChekedItems;
            _ListBoxV.SelectionChanged += _ListBoxV_SelectionChanged;
            _ListBoxH.SelectionChanged += _ListBoxH_SelectionChanged;

            if (ItemsSource != null && !string.IsNullOrEmpty(CheckedMemberPath))
            {
                foreach (var item in ItemsSource)
                {
                    Type itemType = TypeUtils.GetIListItemType(ItemsSource);
                    System.Reflection.PropertyInfo? propertyInfo = itemType.GetProperty(CheckedMemberPath);
                    if(propertyInfo == null)
                        continue;
                    object? val = propertyInfo.GetValue(item);
                    if (val == null)
                        continue;
                    bool isChecked = false;
                    bool.TryParse(val.ToString(), out isChecked);
                    if (isChecked)
                        _ListBoxV.SelectedItems.Add(item);
                }
            }
        }

        private void _ListBoxH_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.RemovedItems)
            {
                for (int i = 0; i < _ListBoxV.SelectedItems.Count; i++)
                {
                    object datachklist = _ListBoxV.SelectedItems[i];
                    if (datachklist == item)
                    {
                        _ListBoxV.SelectedItems.Remove(_ListBoxV.SelectedItems[i]);
                    }
                }
            }
        }

        void _ListBoxV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Type type = this.ItemsSource.GetType();
            ChekedItems = Activator.CreateInstance(type) as IList;
            foreach (var item in e.AddedItems)
            {
                try
                {
                    if (ChekedItems.IndexOf(item) < 0)
                        ChekedItems.Add(item);
                    item.GetType().GetProperty(CheckedMemberPath).SetValue(item, true);
                }
                catch (Exception ex)
                {

                }
            }
            foreach (var item in e.RemovedItems)
            {
                try
                {
                    ChekedItems.Remove(item);
                    item.GetType().GetProperty(CheckedMemberPath).SetValue(item, false);
                }
                catch (Exception ex)
                {

                }
            }
        }

        //public class MultiCbxBaseData
        //{
        //    private int _id;
        //    /// <summary>
        //    /// 关联主键
        //    /// </summary>
        //    public int ID
        //    {
        //        get { return _id; }
        //        set { _id = value; }
        //    }

        //    private string _viewName;
        //    /// <summary>
        //    /// 显示名称
        //    /// </summary>
        //    public string ViewName
        //    {
        //        get { return _viewName; }
        //        set
        //        {
        //            _viewName = value;
        //        }
        //    }

        //    private bool _isCheck;
        //    /// <summary>
        //    /// 是否选中
        //    /// </summary>
        //    public bool IsCheck
        //    {
        //        get { return _isCheck; }
        //        set { _isCheck = value; }
        //    }
        //}
    }
}
