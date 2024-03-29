﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Zhy.Components.Wpf.Attributes.Bases;
using Zhy.Components.Wpf.Attributes.Interfaces;
using Zhy.Components.Wpf.Attributes.ZFormItems;
using Zhy.Components.Wpf.Commons.Comparers;
using Zhy.Components.Wpf.Views.Controls.Zhys;
using Zhy.Components.Wpf.Views.Windows.ViewModel;

namespace Zhy.Components.Wpf.Views.Windows.Zhys
{
    /// <summary>
    /// ZFormDialog 表单项对话框
    /// </summary>
    public partial class ZFormDialog : Window
    {
        Func<INotifyPropertyChanged, bool> _funcValuesCheck = null;
        INotifyPropertyChanged _observableObject = null;
        /// <summary>
        /// ZFormDialog构造函数
        /// </summary>
        /// <param name="observableObject">表单项实例</param>
        /// <param name="funcValuesCheck">属性验证方法，输入为当前编辑的表单项实例，如验证各项属性符合要求则返回true，否则返回false</param>
        public ZFormDialog(INotifyPropertyChanged observableObject, Func<INotifyPropertyChanged, bool> funcValuesCheck = null)
        {
            _observableObject = observableObject;
            _funcValuesCheck = funcValuesCheck;
            InitializeComponent();
            InitOwner();
            InitView();
        }
        /// <summary>
        /// 提示信息
        /// </summary>
        public string Tip
        {
            get { return textBoxTip.Text; }
            set { textBoxTip.Text = value; }
        }
        /// <summary>
        /// 提示信息前景色
        /// </summary>
        public Brush TipForeground
        {
            get { return textBoxTip.Foreground; }
            set { textBoxTip.Foreground = value; }
        }

        private void InitOwner()
        {
            WindowCollection windows = Application.Current.Windows;
            foreach (var item in windows)
            {
                if (!(item is Window))
                    continue;
                Window window = (Window)item;
                if (window.IsActive)
                    this.Owner = window;
            }
        }

        private void InitView()
        {
            this.DataContext = _observableObject;
            StackPanel stackPanel = new StackPanel();
            stackPanel.SetValue(StackPanel.OrientationProperty, Orientation.Vertical);

            int currentCount = 1;
            Type type = _observableObject.GetType();
            PropertyInfo[] propertyInfos = type.GetProperties();

            SortedDictionary<ZFormItemAttribute, PropertyInfo> sortItemTempDic =
                new SortedDictionary<ZFormItemAttribute, PropertyInfo>(new ZFormSortItemComparer());
            foreach (var propertyInfo in propertyInfos)
            {
                ZFormItemAttribute zFormItem =
                   propertyInfo.GetCustomAttribute<ZFormItemAttribute>();
                if (zFormItem == null)
                    continue;
                else if (zFormItem is IZFormColumn && ((IZFormColumn)zFormItem).IsHideFormItem)
                    continue;
                sortItemTempDic.Add(zFormItem, propertyInfo);
            }

            foreach (var item in sortItemTempDic)
            {
                ZFormItemAttribute zFormItem = item.Key;
                PropertyInfo propertyInfo = item.Value;
                if (zFormItem is ZFormTextItemAttribute)
                {
                    if (zFormItem is ZFormTextButtonItemAttribute)
                    {
                        ZFormTextButtonItemAttribute zFormTextButton = (ZFormTextButtonItemAttribute)zFormItem;
                        TextBlock textBlock = new TextBlock();
                        textBlock.SetValue(TextBlock.ForegroundProperty, this.FindResource("InfoBrush"));
                        textBlock.SetValue(TextBlock.FontSizeProperty, 15.0);
                        textBlock.SetValue(TextBlock.TextProperty, $"{currentCount++}. {zFormTextButton.Title}");
                        stackPanel.Children.Add(textBlock);
                        DockPanel dockPanel = new DockPanel();
                        dockPanel.SetValue(DockPanel.MarginProperty, new Thickness(0, 0, 0, 10));
                        TextBox textBox = new TextBox();
                        textBox.SetValue(TextBox.MinHeightProperty, 30.0);
                        textBox.SetValue(TextBox.VerticalContentAlignmentProperty, VerticalAlignment.Center);
                        textBox.SetValue(TextBox.IsReadOnlyProperty, true);
                        textBox.SetBinding(TextBox.TextProperty, GetBinding(zFormTextButton, propertyInfo.Name));
                        Button button = new Button();
                        button.SetBinding(Button.CommandProperty, zFormTextButton.RelayCommandName);
                        button.SetBinding(Button.CommandParameterProperty, new Binding("."));
                        button.SetValue(Button.ContentProperty, zFormTextButton.ButtonContent);
                        button.SetValue(DockPanel.DockProperty, Dock.Right);
                        button.SetValue(Button.StyleProperty, this.FindResource(zFormTextButton.ButtonStyle.ToString()));
                        dockPanel.Children.Add(button);
                        dockPanel.Children.Add(textBox);
                        stackPanel.Children.Add(dockPanel);
                    }
                    else
                    {
                        ZFormTextItemAttribute zFormText = (ZFormTextItemAttribute)zFormItem;
                        TextBlock textBlock = new TextBlock();
                        textBlock.SetValue(TextBlock.ForegroundProperty, this.FindResource("InfoBrush"));
                        textBlock.SetValue(TextBlock.FontSizeProperty, 15.0);
                        textBlock.SetValue(TextBlock.TextProperty, $"{currentCount++}. {zFormText.Title}");
                        stackPanel.Children.Add(textBlock);
                        DockPanel dockPanel = new DockPanel();
                        dockPanel.SetValue(DockPanel.MarginProperty, new Thickness(0, 0, 0, 10));
                        TextBox textBox = new TextBox();
                        textBox.SetValue(TextBox.MinHeightProperty, 30.0);
                        textBox.SetValue(TextBox.VerticalContentAlignmentProperty, VerticalAlignment.Center);
                        textBox.SetValue(TextBox.IsReadOnlyProperty, zFormText.IsReadOnlyItem);
                        textBox.SetBinding(TextBox.TextProperty, GetBinding(zFormText, propertyInfo.Name));
                        dockPanel.Children.Add(textBox);
                        stackPanel.Children.Add(dockPanel);
                    }
                }
                else if (zFormItem is ZFormCheckItemAttribute)
                {
                    ZFormCheckItemAttribute zFormCheck = (ZFormCheckItemAttribute)zFormItem;
                    TextBlock textBlock = new TextBlock();
                    textBlock.SetValue(TextBlock.ForegroundProperty, this.FindResource("InfoBrush"));
                    textBlock.SetValue(TextBlock.FontSizeProperty, 15.1);
                    textBlock.SetValue(TextBlock.TextProperty, $"{currentCount++}. {zFormCheck.Title}");
                    stackPanel.Children.Add(textBlock);
                    DockPanel dockPanel = new DockPanel();
                    dockPanel.SetValue(DockPanel.MarginProperty, new Thickness(0, 0, 0, 10));
                    dockPanel.SetValue(DockPanel.LastChildFillProperty, true);
                    CheckBox checkBox = new CheckBox();
                    checkBox.SetValue(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                    checkBox.SetValue(CheckBox.VerticalContentAlignmentProperty, VerticalAlignment.Center);
                    checkBox.SetBinding(CheckBox.IsCheckedProperty, GetBinding(zFormCheck, propertyInfo.Name));
                    checkBox.SetValue(CheckBox.ContentProperty, zFormCheck.Title);
                    Border border = new Border();
                    border.SetValue(Border.MinHeightProperty, 30.0);
                    border.SetValue(Border.BorderBrushProperty, new SolidColorBrush(Color.FromRgb(150, 150, 150)));
                    border.SetValue(Border.BorderThicknessProperty, new Thickness(0.8));
                    border.Child = checkBox;
                    dockPanel.Children.Add(border);
                    stackPanel.Children.Add(dockPanel);
                }
                else if (zFormItem is ZFormComboItemAttribute)
                {
                    ZFormComboItemAttribute zFormCombo = (ZFormComboItemAttribute)zFormItem;
                    TextBlock textBlock = new TextBlock();
                    textBlock.SetValue(TextBlock.ForegroundProperty, this.FindResource("InfoBrush"));
                    textBlock.SetValue(TextBlock.FontSizeProperty, 15.0);
                    textBlock.SetValue(TextBlock.TextProperty, $"{currentCount++}. {zFormCombo.Title}");
                    stackPanel.Children.Add(textBlock);
                    DockPanel dockPanel = new DockPanel();
                    dockPanel.SetValue(DockPanel.MarginProperty, new Thickness(0, 0, 0, 10));
                    ComboBox comboBox = new ComboBox();
                    comboBox.SetValue(ComboBox.MinHeightProperty, 30.0);
                    comboBox.SetValue(ComboBox.StyleProperty, this.FindResource("InfoComboBox"));
                    comboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
                    {
                        Path = new PropertyPath(zFormCombo.ItemsSourceProperty),
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });
                    comboBox.SetBinding(ComboBox.SelectedItemProperty, propertyInfo.Name);
                    if (!string.IsNullOrEmpty(zFormCombo.MemberPath))
                        comboBox.SetValue(ComboBox.DisplayMemberPathProperty, zFormCombo.MemberPath);
                    dockPanel.Children.Add(comboBox);
                    stackPanel.Children.Add(dockPanel);
                }
                else if (zFormItem is ZFormMultiCheckItemAttribute)
                {
                    ZFormMultiCheckItemAttribute zFormMultiCheck = (ZFormMultiCheckItemAttribute)zFormItem;
                    TextBlock textBlock = new TextBlock();
                    textBlock.SetValue(TextBlock.ForegroundProperty, this.FindResource("InfoBrush"));
                    textBlock.SetValue(TextBlock.FontSizeProperty, 15.0);
                    textBlock.SetValue(TextBlock.TextProperty, $"{currentCount++}. {zFormMultiCheck.Title}");
                    stackPanel.Children.Add(textBlock);
                    MultiCheckBox multiCheckBox = new MultiCheckBox();
                    multiCheckBox.SetBinding(MultiCheckBox.ItemsSourceProperty, new Binding()
                    {
                        Path = new PropertyPath(propertyInfo.Name),
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });
                    multiCheckBox.SetValue(MultiCheckBox.ContentPropertyNameProperty, zFormMultiCheck.ContentProperty);
                    multiCheckBox.SetValue(MultiCheckBox.CheckPropertyNameProperty, zFormMultiCheck.MemberPath);
                    Border border = new Border();
                    border.SetValue(Border.MinHeightProperty, 30.0);
                    border.SetValue(Border.BorderBrushProperty, new SolidColorBrush(Color.FromRgb(150, 150, 150)));
                    border.SetValue(Border.BorderThicknessProperty, new Thickness(0.8));
                    border.Child = multiCheckBox;
                    stackPanel.Children.Add(border);
                }
                else if (zFormItem is ZFormDateItemAttribute)
                {
                    ZFormDateItemAttribute zFormDate = (ZFormDateItemAttribute)zFormItem;
                    TextBlock textBlock = new TextBlock();
                    textBlock.SetValue(TextBlock.ForegroundProperty, this.FindResource("InfoBrush"));
                    textBlock.SetValue(TextBlock.FontSizeProperty, 15.0);
                    textBlock.SetValue(TextBlock.TextProperty, $"{currentCount++}. {zFormDate.Title}");
                    stackPanel.Children.Add(textBlock);
                    DockPanel dockPanel = new DockPanel();
                    dockPanel.SetValue(DockPanel.MarginProperty, new Thickness(0, 0, 0, 10));
                    DateTimePicker dateTimePicker = new DateTimePicker();
                    dateTimePicker.SetValue(Border.BorderBrushProperty, new SolidColorBrush(Color.FromRgb(150, 150, 150)));
                    dateTimePicker.SetValue(Border.BorderThicknessProperty, new Thickness(0.8));
                    dateTimePicker.SetValue(DateTimePicker.MinHeightProperty, 30.0);
                    dateTimePicker.SetValue(DateTimePicker.VerticalContentAlignmentProperty, VerticalAlignment.Center);
                    //dateTimePicker.SetValue(DateTimePicker.IsReadOnlyProperty, zFormText.IsReadOnlyItem);
                    dateTimePicker.SetBinding(DateTimePicker.DisplayDateProperty, GetBinding(zFormDate, propertyInfo.Name));
                    dockPanel.Children.Add(dateTimePicker);
                    stackPanel.Children.Add(dockPanel);
                }
            }
            gridContent.Children.Add(stackPanel);
        }

        private Binding GetBinding(IZFormItem zFormItem, string propertyName)
        {
            if (string.IsNullOrEmpty(zFormItem.MemberPath))
            {
                return new Binding()
                {
                    Path = new PropertyPath(propertyName),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
            }
            else
            {
                return new Binding()
                {
                    Path = new PropertyPath(propertyName + "." + zFormItem.MemberPath),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
            }
        }

        private void buttonOK_Click(object sender, RoutedEventArgs e)
        {
            if (_funcValuesCheck == null)
                DialogResult = true;
            else if (_funcValuesCheck(_observableObject))
                DialogResult = true;
        }

        private void buttonCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
