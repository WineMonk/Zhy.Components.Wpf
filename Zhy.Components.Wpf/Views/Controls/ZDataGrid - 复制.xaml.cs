﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Zhy.Components.Wpf._Attribute;
using Zhy.Components.Wpf._Attribute._Base;
using Zhy.Components.Wpf._Attribute._ZFormColumn;
using Zhy.Components.Wpf._Common._Comparer;
using Zhy.Components.Wpf._Enum;

namespace Zhy.Components.Wpf._View._UserControl
{
    /// <summary>
    /// ZDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class ZDataGrid : UserControl
    {
        /// <summary>
        /// 数据项源
        /// </summary>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        /// <summary>
        /// 是否为只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return (bool)GetValue(IsReadOnlyProperty); }
            set { SetValue(IsReadOnlyProperty, value); }
        }
        /// <summary>
        /// 查询按钮样式
        /// </summary>
        public ZFormButtonStyle SearchButtonStyle
        {
            get { return (ZFormButtonStyle)GetValue(SearchButtonStyleProperty); }
            set { SetValue(SearchButtonStyleProperty, value); }
        }
        /// <summary>
        /// 选择项
        /// </summary>
        public object SelectedItem
        {
            get { return (object)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        /// <summary>
        /// ZDataGrid 表单控件
        /// </summary>
        public ZDataGrid()
        {
            InitializeComponent();
            InitDataGridComponent();
            InitSearchComponent();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        public void Refresh()
        {
            InitDataGridColumn();
        }
        /// <summary>
        /// 查询按钮样式依赖属性
        /// </summary>
        public static readonly DependencyProperty SearchButtonStyleProperty =
            DependencyProperty.Register("SearchButtonStyle", typeof(ZFormButtonStyle), typeof(ZDataGrid), new PropertyMetadata(ZFormButtonStyle.DefaultButton, OnSearchButtonStylePropertyChanged));
        private static void OnSearchButtonStylePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZFormButtonStyle newValue = (ZFormButtonStyle)e.NewValue;
            ZFormButtonStyle oldValue = (ZFormButtonStyle)e.OldValue;
            if (newValue == oldValue)
                return;
            ZDataGrid zDataGrid = d as ZDataGrid;
            if (zDataGrid != null)
                zDataGrid.RefreshSearchComponent();
        }
        /// <summary>
        /// 是否为只读依赖属性
        /// </summary>
        public static readonly DependencyProperty IsReadOnlyProperty =
            DependencyProperty.Register("IsReadOnly", typeof(bool), typeof(ZDataGrid), new PropertyMetadata(true, OnIsReadOnlyPropertyChanged));
        private static void OnIsReadOnlyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZDataGrid zDataGrid = d as ZDataGrid;
            bool newValue = (bool)e.NewValue;
            bool oldValue = (bool)e.OldValue;
            if (newValue != oldValue && zDataGrid != null)
                zDataGrid.Refresh();
        }
        /// <summary>
        /// 数据项源依赖属性
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList), typeof(ZDataGrid), new PropertyMetadata(default(IList), OnItemsSourcePropertyChanged));
        private static void OnItemsSourcePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZDataGrid zDataGrid = d as ZDataGrid;
            TextBlock textBlockCount = zDataGrid.FindName("textBlockCount") as TextBlock;
            if (textBlockCount == null)
                return;
            textBlockCount.DataContext = e.NewValue;
            //EventInfo collectionChangedEvent = e.NewValue.GetType().GetEvent("CollectionChanged");
            //if (collectionChangedEvent != null)
            //{
            //    NotifyCollectionChangedEventHandler handler = (sender, ee) =>
            //    {
            //        WeakReferenceMessenger.Default.Send(e.NewValue);
            //    };
            //    collectionChangedEvent.AddEventHandler(e.NewValue, handler);
            //}
        }
        /// <summary>
        /// 选择项依赖属性
        /// </summary>
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(object), typeof(ZDataGrid), new PropertyMetadata(null));

        internal void RefreshSearchComponent()
        {
            InitSearchComponent();
        }

        #region 查询
        public void SearchAll()
        {
            TextBox textBoxSearch = null;
            Button buttonCancelSearch = null;
            foreach (var item in dockPanelSearch.Children)
            {
                if (item is TextBox)
                {
                    TextBox textBoxItem = (TextBox)item;
                    if (textBoxItem.Name == "textBoxSearch")
                        textBoxSearch = textBoxItem;
                }
                else if (item is Button)
                {
                    Button buttonItem = (Button)item;
                    if (buttonItem.Name == "buttonCancelSearch")
                        buttonCancelSearch = buttonItem;
                }
            }
            textBoxSearch.Text = string.Empty;
            if (_totalObservableObjects == null ||
                _totalObservableObjects.Count < 1 ||
                textBoxSearch == null ||
                buttonCancelSearch == null)
                return;
            ItemsSource.Clear();
            foreach (var item in _totalObservableObjects)
                ItemsSource.Add(item);
            _totalObservableObjects = null;
            buttonCancelSearch.Visibility = Visibility.Collapsed;
        }
        private IList _totalObservableObjects;
        private IList _searchObservableObjects;
        private void InitSearchComponent()
        {
            dockPanelSearch.Children.Clear();
            Button buttonSearch = new Button();
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Content = "查 询";
            buttonSearch.Style = this.FindResource(SearchButtonStyle.ToString()) as Style;
            buttonSearch.SetValue(DockPanel.DockProperty, Dock.Right);
            buttonSearch.Margin = new Thickness(1, 2, 1, 2);
            TextBox textBoxSearch = new TextBox();
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.BorderThickness = new Thickness(0.8);
            textBoxSearch.Margin = new Thickness(1, 2, 1, 2);
            textBoxSearch.SetValue(_Common._Utils.TextBoxHelper.TextMarkProperty, "输入查询内容");
            textBoxSearch.VerticalContentAlignment = VerticalAlignment.Center;
            textBoxSearch.MinWidth = 200.0;
            textBoxSearch.Style = this.FindResource("InfoTextBox") as Style;
            Button buttonCancelSearch = new Button();
            buttonCancelSearch.Name = "buttonCancelSearch";
            buttonCancelSearch.Content = "取 消";
            buttonCancelSearch.Visibility = Visibility.Collapsed;
            buttonCancelSearch.Style = this.FindResource(ZFormButtonStyle.ErrorButton.ToString()) as Style;
            buttonCancelSearch.SetValue(DockPanel.DockProperty, Dock.Right);
            buttonCancelSearch.Margin = new Thickness(1, 2, 1, 2);

            buttonSearch.Click += ButtonSearch_Click;

            buttonCancelSearch.Click += (o, e) =>
            {
                textBoxSearch.Text = string.Empty;
                ButtonSearch_Click(o, e);
            };

            textBoxSearch.KeyUp += (o, e) =>
            {
                if (e.Key == Key.Enter)
                {
                    buttonSearch.Focus();
                    ButtonSearch_Click(o, e);
                    textBoxSearch.Focus();
                }
                else if (e.Key == Key.Escape)
                {
                    textBoxSearch.Text = string.Empty;
                    ButtonSearch_Click(o, e);
                }
            };

            dockPanelSearch.Children.Add(buttonSearch);
            dockPanelSearch.Children.Add(buttonCancelSearch);
            dockPanelSearch.Children.Add(textBoxSearch);
            UpdateSearchTextMark();
        }
        private void UpdateSearchTextMark()
        {
            if (ItemsSource == null || !ItemsSource.GetType().IsGenericType)
                return;
            Type sourceItemType = ItemsSource.GetType().GetGenericArguments()[0];
            PropertyInfo[] propertyInfos = sourceItemType.GetProperties();
            SortedDictionary<IZFormColumn, PropertyInfo> sortColumnTempDic =
                new SortedDictionary<IZFormColumn, PropertyInfo>(new ZFormSortItemComparer());
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Attribute? attributeColumn = propertyInfo.GetCustomAttribute(typeof(ZFormItemAttribute), true);
                if (attributeColumn != null && attributeColumn is IZFormColumn)
                {
                    IZFormColumn? zFormItemAttribute = (IZFormColumn)attributeColumn;
                    sortColumnTempDic.Add(zFormItemAttribute, propertyInfo);
                }
            }
            string mark = null;
            foreach (var item in sortColumnTempDic)
            {
                if (item.Key.IsSearchProperty)
                {
                    if (mark != null)
                        mark += " / ";
                    mark += item.Key.Title.Replace(" ", "");
                }
            }
            if (mark == null)
                return;
            mark = "查询 " + mark + " 信息";
            foreach (var item in dockPanelSearch.Children)
            {
                if (item is TextBox && ((TextBox)item).Name == "textBoxSearch")
                {
                    TextBox textBoxItem = (TextBox)item;
                    textBoxItem.SetValue(_Common._Utils.TextBoxHelper.TextMarkProperty, mark);
                    return;
                }
            }
        }
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            UpdateTotalItems();
            TextBox textBoxSearch = null;
            Button buttonCancelSearch = null;
            foreach (var item in dockPanelSearch.Children)
            {
                if (item is TextBox)
                {
                    TextBox textBoxItem = (TextBox)item;
                    if (textBoxItem.Name == "textBoxSearch")
                        textBoxSearch = textBoxItem;
                }
                else if (item is Button)
                {
                    Button buttonItem = (Button)item;
                    if (buttonItem.Name == "buttonCancelSearch")
                        buttonCancelSearch = buttonItem;
                }
            }

            if (string.IsNullOrEmpty(textBoxSearch.Text))
            {
                if (_totalObservableObjects == null || _totalObservableObjects.Count < 1)
                    return;
                ItemsSource.Clear();
                foreach (var item in _totalObservableObjects)
                    ItemsSource.Add(item);
                _totalObservableObjects = null;
                buttonCancelSearch.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (_totalObservableObjects == null || _totalObservableObjects.Count < 1)
                {
                    _totalObservableObjects = new List<object>();
                    foreach (var item in ItemsSource)
                        _totalObservableObjects.Add(item);
                }
                ItemsSource.Clear();
                List<PropertyInfo> propertyInfos = GetSearchColumnPropertyInfo(ItemsSource);
                foreach (var item in _totalObservableObjects)
                {
                    bool check = CheckItem(item, propertyInfos, textBoxSearch.Text);
                    if (check)
                        ItemsSource.Add(item);
                }
                _searchObservableObjects = new List<object>();
                foreach (var item in ItemsSource)
                    _searchObservableObjects.Add(item);
                buttonCancelSearch.Visibility = Visibility.Visible;
            }
        }
        private List<PropertyInfo> GetSearchColumnPropertyInfo(object ObservableObjects)
        {
            List<PropertyInfo> columnPropertyInfos = new List<PropertyInfo>();
            if (!ObservableObjects.GetType().IsGenericType)
                return columnPropertyInfos;
            Type sourceItemType = ObservableObjects.GetType().GetGenericArguments()[0];
            PropertyInfo[] propertyInfos = sourceItemType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IEnumerable<Attribute> attributes = propertyInfo.GetCustomAttributes();
                foreach (Attribute attribute in attributes)
                    if (attribute is IZFormColumn && ((IZFormColumn)attribute).IsSearchProperty)
                        columnPropertyInfos.Add(propertyInfo);
            }
            return columnPropertyInfos;
        }
        private bool CheckItem(object item, List<PropertyInfo> propertyInfos, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return true;
            foreach (var propertyInfo in propertyInfos)
            {
                object? val = propertyInfo.GetValue(item, null);
                if (val == null) continue;
                Attribute? attribute = propertyInfo.GetCustomAttribute(typeof(ZFormItemAttribute));
                if (attribute is ZFormComboColumnAttribute && !string.IsNullOrEmpty(((ZFormComboColumnAttribute)attribute).MemberPath))
                {
                    ZFormComboColumnAttribute zComboDataColumnAttribute = (ZFormComboColumnAttribute)attribute;
                    PropertyInfo? pi = val.GetType().GetProperty(zComboDataColumnAttribute.MemberPath);
                    if (pi == null) continue;
                    object? vval = pi.GetValue(val, null);
                    if (vval == null) continue;
                    if (vval.ToString().ToLower().Contains(searchText.ToLower()))
                        return true;
                    //ZFormComboColumnAttribute zComboDataColumnAttribute = (ZFormComboColumnAttribute)attribute;
                    //PropertyInfo? pi = item.GetType().GetProperty(zComboDataColumnAttribute.TargetProperty);
                    //object? vval = pi.GetValue(item);
                    //if (vval == null) continue;
                    //PropertyInfo? ppi = vval.GetType().GetProperty(((ZFormComboColumnAttribute)attribute).MemberPath);
                    //if (ppi == null) continue;
                    //object? vvval = propertyInfo.GetValue(vval, null);
                    //if (vvval == null) continue;
                    //if (vvval.ToString().ToLower().Contains(searchText.ToLower()))
                    //    return true;
                }
                else if (attribute is ZFormTextButtonColumnAttribute && !string.IsNullOrEmpty(((ZFormTextButtonColumnAttribute)attribute).MemberPath))
                {
                    PropertyInfo? pi = val.GetType().GetProperty(((ZFormTextButtonColumnAttribute)attribute).MemberPath);
                    if (pi == null) continue;
                    object? vval = pi.GetValue(val, null);
                    if (vval == null) continue;
                    if (vval.ToString().ToLower().Contains(searchText.ToLower()))
                        return true;
                }
                else if (attribute is ZFormMultiCheckColumnAttribute && !string.IsNullOrEmpty(((ZFormMultiCheckColumnAttribute)attribute).MemberPath))
                {
                    ZFormMultiCheckColumnAttribute zFormMultiCheck = attribute as ZFormMultiCheckColumnAttribute;
                    IEnumerable objects = val as IEnumerable;
                    if (objects == null)
                        continue;
                    foreach (var obj in objects)
                    {
                        PropertyInfo? propertyInfoMember = obj.GetType().GetProperty(zFormMultiCheck.MemberPath);
                        PropertyInfo? propertyInfoContent = obj.GetType().GetProperty(zFormMultiCheck.ContentProperty);
                        if (propertyInfoMember == null || propertyInfoContent == null)
                            continue;
                        object member = propertyInfoMember.GetValue(obj, null);
                        object content = propertyInfoContent.GetValue(obj, null);
                        if (member == null || content == null)
                            continue;
                        bool check = false;
                        bool.TryParse(member.ToString(), out check);
                        if (!check)
                            continue;
                        if (content.ToString().ToLower().Contains(searchText.ToLower()))
                            return true;
                    }
                }
                else
                {
                    if (val.ToString().ToLower().Contains(searchText.ToLower()))
                        return true;
                }
            }
            return false;
        }
        private bool CompareIList(IList list1, IList list2)
        {
            if (list1.Count != list2.Count)
                return false;
            foreach (var item in list1)
            {
                int idx = list2.IndexOf(item);
                if (idx < 0)
                    return false;
            }
            return true;
        }
        private void UpdateTotalItems()
        {
            if (_totalObservableObjects != null && _totalObservableObjects.Count > 0)
            {
                bool modify = !CompareIList(ItemsSource, _searchObservableObjects);
                if (modify)
                {
                    foreach (var item in _searchObservableObjects)
                    {
                        int idxRm = ItemsSource.IndexOf(item);
                        if (idxRm < 0)
                            _totalObservableObjects.Remove(item);
                    }
                    foreach (var item in ItemsSource)
                    {
                        int idxAdd = _searchObservableObjects.IndexOf(item);
                        if (idxAdd < 0)
                            _totalObservableObjects.Add(item);
                    }
                }
            }
        }
        #endregion 查询

        private void InitDataGridComponent()
        {
            dataGrid.CanUserAddRows = false;
            dataGrid.CanUserResizeRows = false;
            dataGrid.CanUserSortColumns = true;
            dataGrid.VerticalContentAlignment = VerticalAlignment.Center;
            dataGrid.AutoGeneratedColumns += DataGrid_AutoGeneratedColumns;
        }

        private void DataGrid_AutoGeneratedColumns(object? sender, EventArgs e)
        {
            InitDataGridColumn();
        }

        private void InitDataGridColumn()
        {
            if (ItemsSource == null || !ItemsSource.GetType().IsGenericType)
                return;
            Type sourceItemType = ItemsSource.GetType().GetGenericArguments()[0];
            PropertyInfo[] propertyInfos = sourceItemType.GetProperties();
            MethodInfo[] methodInfos = sourceItemType.GetMethods();
            SortedDictionary<IZFormColumn, PropertyInfo> sortColumnTempDic =
                new SortedDictionary<IZFormColumn, PropertyInfo>(new ZFormSortItemComparer());
            SortedDictionary<IZFormFuncButton, PropertyInfo> sortButtonColumnDic =
                new SortedDictionary<IZFormFuncButton, PropertyInfo>(new ZFormSortItemComparer());
            SortedDictionary<IZFormFuncButton, PropertyInfo> sortButtonTopDic =
                new SortedDictionary<IZFormFuncButton, PropertyInfo>(new ZFormSortItemComparer(true));
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Attribute? attributeColumn = propertyInfo.GetCustomAttribute(typeof(ZFormItemAttribute), true);
                Attribute? attributeButton = propertyInfo.GetCustomAttribute(typeof(ZFormFuncButtonAttribute), true);
                if (attributeColumn != null && attributeColumn is IZFormColumn)
                {
                    IZFormColumn? zFormItemAttribute = (IZFormColumn)attributeColumn;
                    if (!zFormItemAttribute.IsHideFormColumn)
                    {
                        sortColumnTempDic.Add(zFormItemAttribute, propertyInfo);
                    }
                }
                if (attributeButton != null)
                {
                    IZFormFuncButton zFormFuncButtonAttribute = (IZFormFuncButton)attributeButton;
                    if (attributeButton is ZFormOperColumnButtonAttribute)
                        sortButtonColumnDic.Add(zFormFuncButtonAttribute, propertyInfo);
                    else if (attributeButton is ZFormToolButtonAttribute)
                        sortButtonTopDic.Add(zFormFuncButtonAttribute, propertyInfo);
                }
            }
            //foreach (MethodInfo methodInfo in methodInfos)
            //{
            //    Attribute? attribute = methodInfo.GetCustomAttribute(typeof(ZFormFuncButtonAttribute), true);
            //    if (attribute != null)
            //    {
            //        IZFormFuncButton zFormFuncButtonAttribute = (IZFormFuncButton)attribute;
            //        sortButtonTopDic.Add(zFormFuncButtonAttribute, methodInfo);
            //    }
            //}
            dataGrid.Columns.Clear();
            foreach (var item in sortColumnTempDic)
            {
                PropertyInfo propertyInfo = item.Value;
                IZFormColumn? attribute = item.Key;
                if (attribute == null) continue;
                //if ((zFormDataGridAttribute == null && !attribute.IsReadOnlyColumn) ||
                //    (zFormDataGridAttribute != null && !zFormDataGridAttribute.IsReadOnly &&
                //    !attribute.IsReadOnlyColumn))
                if ((!IsReadOnly && !attribute.IsReadOnlyColumn))
                {
                    if (attribute is ZFormTextColumnAttribute)
                    {
                        ZFormTextColumnAttribute zTextAttribute = (ZFormTextColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        { Header = zTextAttribute.Title, Width = new DataGridLength(zTextAttribute.Width, zTextAttribute.WidthUnit) };
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));

                        FrameworkElementFactory textBox = new FrameworkElementFactory(typeof(TextBox));
                        textBox.SetValue(TextBox.PaddingProperty, new Thickness(5, 0, 5, 0));
                        textBox.SetValue(VerticalContentAlignmentProperty, VerticalAlignment.Center);
                        textBox.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));
                        textBox.SetValue(TextBox.StyleProperty, this.FindResource("DataGridTextBox"));
                        textBox.SetBinding(TextBox.ForegroundProperty, new Binding());
                        textBox.SetBinding(TextBox.TextProperty, GetBinding(zTextAttribute, propertyInfo.Name));
                        cellFactory.AppendChild(textBox);

                        dataTemplate.VisualTree = cellFactory;
                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                            (string.IsNullOrEmpty(zTextAttribute.MemberPath) ? "" : ".") +
                            zTextAttribute.MemberPath;
                        dataGrid.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormTextButtonColumnAttribute)
                    {
                        ZFormTextButtonColumnAttribute zButtonAttribute = (ZFormTextButtonColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        { Header = zButtonAttribute.Title, Width = new DataGridLength(zButtonAttribute.Width, zButtonAttribute.WidthUnit) };
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));

                        FrameworkElementFactory button = new FrameworkElementFactory(typeof(Button));
                        button.SetValue(DockPanel.DockProperty, Dock.Right);
                        button.SetValue(ContentProperty, zButtonAttribute.ButtonContent);
                        button.SetValue(Button.MarginProperty, new Thickness(1, 2, 2, 1));
                        button.SetValue(Button.StyleProperty, this.FindResource(zButtonAttribute.ButtonStyle.ToString()));
                        button.SetBinding(Button.CommandProperty, new Binding() { Path = new PropertyPath(zButtonAttribute.RelayCommandName) });
                        button.SetBinding(Button.CommandParameterProperty, new Binding() { Path = new PropertyPath(".") });
                        cellFactory.AppendChild(button);

                        FrameworkElementFactory textBox = new FrameworkElementFactory(typeof(TextBox));
                        textBox.SetValue(TextBox.PaddingProperty, new Thickness(5, 0, 5, 0));
                        textBox.SetValue(VerticalContentAlignmentProperty, VerticalAlignment.Center);
                        textBox.SetValue(TextBox.IsReadOnlyProperty, true);
                        textBox.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));
                        textBox.SetValue(TextBox.StyleProperty, this.FindResource("DataGridTextBox"));
                        textBox.SetBinding(TextBox.ForegroundProperty, new Binding());
                        textBox.SetBinding(TextBox.TextProperty, GetBinding(zButtonAttribute, propertyInfo.Name));
                        cellFactory.AppendChild(textBox);

                        dataTemplate.VisualTree = cellFactory;
                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGridTemplateColumn.MinWidth = 120;
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                            (string.IsNullOrEmpty(zButtonAttribute.MemberPath) ? "" : ".") +
                            zButtonAttribute.MemberPath;
                        dataGrid.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormCheckColumnAttribute)
                    {
                        ZFormCheckColumnAttribute zCheckAttribute = (ZFormCheckColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        {
                            Header = zCheckAttribute.Title,
                            Width = new DataGridLength(zCheckAttribute.Width, zCheckAttribute.WidthUnit)
                        };
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));
                        FrameworkElementFactory checkBox = new FrameworkElementFactory(typeof(CheckBox));
                        checkBox.SetValue(CheckBox.IsEnabledProperty, !zCheckAttribute.IsReadOnlyColumn);
                        checkBox.SetValue(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Center);
                        checkBox.SetValue(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                        checkBox.SetBinding(CheckBox.IsCheckedProperty, GetBinding(zCheckAttribute, propertyInfo.Name));
                        cellFactory.AppendChild(checkBox);
                        dataTemplate.VisualTree = cellFactory;

                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                            (string.IsNullOrEmpty(zCheckAttribute.MemberPath) ? "" : ".") +
                            zCheckAttribute.MemberPath;
                        dataGrid.Columns.Add(dataGridTemplateColumn);


                        //ZFormCheckColumnAttribute zCheckAttribute = (ZFormCheckColumnAttribute)attribute;
                        //Style elementStyle = new Style(typeof(CheckBox));
                        //elementStyle.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));
                        //elementStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center));
                        //DataGridCheckBoxColumn dataGridCheckBoxColumn = new DataGridCheckBoxColumn()
                        //{
                        //    Header = zCheckAttribute.Title,
                        //    IsReadOnly = zCheckAttribute.IsReadOnlyColumn,
                        //    Binding = GetBinding(zCheckAttribute, propertyInfo.Name),
                        //    Width = new DataGridLength(zCheckAttribute.Width, zCheckAttribute.WidthUnit),
                        //    ElementStyle = elementStyle
                        //};
                        //dataGrid.Columns.Add(dataGridCheckBoxColumn);
                    }
                    else if (attribute is ZFormComboColumnAttribute)
                    {
                        ZFormComboColumnAttribute zComboAttribute = (ZFormComboColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        { Header = zComboAttribute.Title, Width = new DataGridLength(zComboAttribute.Width, zComboAttribute.WidthUnit) };
                        //dataGridTemplateColumn.IsReadOnly = zComboAttribute.IsReadOnly;
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name;
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));
                        FrameworkElementFactory comboBox = new FrameworkElementFactory(typeof(ComboBox));
                        comboBox.SetValue(ComboBox.StyleProperty, this.FindResource("InfoComboBox"));
                        comboBox.SetValue(ComboBox.BorderThicknessProperty, new Thickness(0));
                        comboBox.SetBinding(ComboBox.ForegroundProperty, new Binding());
                        comboBox.SetBinding(ComboBox.BackgroundProperty, new Binding());
                        comboBox.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
                        {
                            Path = new PropertyPath(zComboAttribute.ItemsSourceProperty),
                            Mode = BindingMode.TwoWay,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        });
                        comboBox.SetBinding(ComboBox.SelectedItemProperty, new Binding()
                        {
                            Path = new PropertyPath(propertyInfo.Name),
                            Mode = BindingMode.TwoWay,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        });
                        comboBox.SetValue(ComboBox.SelectedIndexProperty, 0);
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                            (string.IsNullOrEmpty(zComboAttribute.MemberPath) ? "" : ".") +
                            zComboAttribute.MemberPath;
                        dataGridTemplateColumn.CanUserSort = true;
                        cellFactory.AppendChild(comboBox);
                        dataTemplate.VisualTree = cellFactory;
                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGrid.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormMultiCheckColumnAttribute)
                    {
                        ZFormMultiCheckColumnAttribute zMultiCheckAttribute = (ZFormMultiCheckColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        { Header = zMultiCheckAttribute.Title, Width = new DataGridLength(zMultiCheckAttribute.Width, zMultiCheckAttribute.WidthUnit) };
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name;
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));
                        FrameworkElementFactory multiCheckBox = new FrameworkElementFactory(typeof(MultiCheckBox));
                        multiCheckBox.SetValue(MultiCheckBox.CheckPropertyNameProperty, zMultiCheckAttribute.MemberPath);
                        multiCheckBox.SetValue(MultiCheckBox.ContentPropertyNameProperty, zMultiCheckAttribute.ContentProperty);
                        multiCheckBox.SetBinding(MultiCheckBox.ItemsSourceProperty, new Binding()
                        {
                            Path = new PropertyPath(propertyInfo.Name),
                            Mode = BindingMode.TwoWay,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        });
                        cellFactory.AppendChild(multiCheckBox);
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                            (string.IsNullOrEmpty(zMultiCheckAttribute.MemberPath) ? "" : ".") +
                            zMultiCheckAttribute.MemberPath;
                        dataGridTemplateColumn.CanUserSort = true;
                        dataTemplate.VisualTree = cellFactory;
                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGrid.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormDateColumnAttribute)
                    {
                        ZFormDateColumnAttribute zDateAttribute = (ZFormDateColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        { Header = zDateAttribute.Title, Width = new DataGridLength(zDateAttribute.Width, zDateAttribute.WidthUnit) };
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));

                        FrameworkElementFactory dateTimePicker = new FrameworkElementFactory(typeof(DateTimePicker));
                        dateTimePicker.SetValue(DateTimePicker.PaddingProperty, new Thickness(5, 0, 5, 0));
                        dateTimePicker.SetValue(VerticalContentAlignmentProperty, VerticalAlignment.Center);
                        dateTimePicker.SetValue(DateTimePicker.BorderThicknessProperty, new Thickness(0));
                        dateTimePicker.SetValue(DateTimePicker.DisplayFormatProperty, zDateAttribute.DateFormat);
                        dateTimePicker.SetBinding(DateTimePicker.ForegroundProperty, new Binding());
                        dateTimePicker.SetBinding(DateTimePicker.ForegroundProperty, new Binding());
                        dateTimePicker.SetBinding(DateTimePicker.DisplayDateProperty, GetBinding(zDateAttribute, propertyInfo.Name));
                        cellFactory.AppendChild(dateTimePicker);

                        dataTemplate.VisualTree = cellFactory;
                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                            (string.IsNullOrEmpty(zDateAttribute.MemberPath) ? "" : ".") +
                            zDateAttribute.MemberPath;
                        dataGrid.Columns.Add(dataGridTemplateColumn);
                    }
                }
                else
                {
                    if (attribute is ZFormTextColumnAttribute)
                    {
                        ZFormTextColumnAttribute zTextAttribute = (ZFormTextColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        { Header = zTextAttribute.Title, Width = new DataGridLength(zTextAttribute.Width, zTextAttribute.WidthUnit) };
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));

                        FrameworkElementFactory textBox = new FrameworkElementFactory(typeof(TextBox));
                        textBox.SetValue(TextBox.IsReadOnlyProperty, true);
                        textBox.SetValue(TextBox.PaddingProperty, new Thickness(5, 0, 5, 0));
                        textBox.SetValue(VerticalContentAlignmentProperty, VerticalAlignment.Center);
                        textBox.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));
                        textBox.SetValue(TextBox.StyleProperty, this.FindResource("DataGridTextBox"));
                        textBox.SetBinding(TextBox.ForegroundProperty, new Binding());
                        textBox.SetBinding(TextBox.TextProperty, GetBinding(zTextAttribute, propertyInfo.Name));
                        cellFactory.AppendChild(textBox);

                        dataTemplate.VisualTree = cellFactory;
                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                            (string.IsNullOrEmpty(zTextAttribute.MemberPath) ? "" : ".") +
                            zTextAttribute.MemberPath;
                        dataGrid.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormTextButtonColumnAttribute)
                    {
                        ZFormTextButtonColumnAttribute zButtonAttribute = (ZFormTextButtonColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        { Header = zButtonAttribute.Title, Width = new DataGridLength(zButtonAttribute.Width, zButtonAttribute.WidthUnit) };
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));

                        FrameworkElementFactory textBox = new FrameworkElementFactory(typeof(TextBox));
                        textBox.SetValue(TextBox.IsReadOnlyProperty, true);
                        textBox.SetValue(TextBox.PaddingProperty, new Thickness(5, 0, 5, 0));
                        textBox.SetValue(VerticalContentAlignmentProperty, VerticalAlignment.Center);
                        textBox.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));
                        textBox.SetValue(TextBox.StyleProperty, this.FindResource("DataGridTextBox"));
                        textBox.SetBinding(TextBox.ForegroundProperty, new Binding());
                        textBox.SetBinding(TextBox.TextProperty, GetBinding(zButtonAttribute, propertyInfo.Name));
                        cellFactory.AppendChild(textBox);

                        dataTemplate.VisualTree = cellFactory;
                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                            (string.IsNullOrEmpty(zButtonAttribute.MemberPath) ? "" : ".") +
                            zButtonAttribute.MemberPath;
                        dataGrid.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormCheckColumnAttribute)
                    {
                        ZFormCheckColumnAttribute zCheckAttribute = (ZFormCheckColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        {
                            Header = zCheckAttribute.Title,
                            Width = new DataGridLength(zCheckAttribute.Width, zCheckAttribute.WidthUnit)
                        };
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));
                        FrameworkElementFactory checkBox = new FrameworkElementFactory(typeof(CheckBox));
                        checkBox.SetValue(CheckBox.IsEnabledProperty, false);
                        checkBox.SetValue(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Center);
                        checkBox.SetValue(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                        checkBox.SetBinding(CheckBox.IsCheckedProperty, GetBinding(zCheckAttribute, propertyInfo.Name));
                        cellFactory.AppendChild(checkBox);

                        dataTemplate.VisualTree = cellFactory;
                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                            (string.IsNullOrEmpty(zCheckAttribute.MemberPath) ? "" : ".") +
                            zCheckAttribute.MemberPath;
                        dataGrid.Columns.Add(dataGridTemplateColumn);

                        Style elementStyle = new Style(typeof(CheckBox));
                        elementStyle.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));
                        elementStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center));
                    }
                    else if (attribute is ZFormComboColumnAttribute)
                    {
                        ZFormComboColumnAttribute zComboAttribute = (ZFormComboColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        {
                            Header = zComboAttribute.Title,
                            Width = new DataGridLength(zComboAttribute.Width, zComboAttribute.WidthUnit)
                        };
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));

                        FrameworkElementFactory textBox = new FrameworkElementFactory(typeof(TextBox));
                        textBox.SetValue(TextBox.IsReadOnlyProperty, true);
                        textBox.SetValue(TextBox.PaddingProperty, new Thickness(5, 0, 5, 0));
                        textBox.SetValue(VerticalContentAlignmentProperty, VerticalAlignment.Center);
                        textBox.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));
                        textBox.SetValue(TextBox.StyleProperty, this.FindResource("DataGridTextBox"));
                        textBox.SetBinding(TextBox.ForegroundProperty, new Binding());
                        textBox.SetBinding(TextBox.TextProperty, GetBinding(zComboAttribute, propertyInfo.Name));
                        cellFactory.AppendChild(textBox);

                        dataTemplate.VisualTree = cellFactory;
                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                            (string.IsNullOrEmpty(zComboAttribute.MemberPath) ? "" : ".") +
                            zComboAttribute.MemberPath;
                        dataGrid.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormMultiCheckColumnAttribute)
                    {
                        ZFormMultiCheckColumnAttribute zMultiCheckAttribute = (ZFormMultiCheckColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        { Header = zMultiCheckAttribute.Title, Width = new DataGridLength(zMultiCheckAttribute.Width, zMultiCheckAttribute.WidthUnit) };
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name;
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));

                        FrameworkElementFactory itemsPanel = new FrameworkElementFactory(typeof(StackPanel));
                        itemsPanel.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
                        FrameworkElementFactory itemsControl = new FrameworkElementFactory(typeof(ItemsControl));
                        ItemsPanelTemplate itemsPanelTemplate = new ItemsPanelTemplate(itemsPanel);
                        itemsControl.SetValue(ItemsControl.ItemsPanelProperty, itemsPanelTemplate);
                        itemsControl.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
                        {
                            Path = new PropertyPath(propertyInfo.Name),
                            Mode = BindingMode.TwoWay,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        });
                        DataTemplate dataTemplateItemsControl = new DataTemplate();
                        FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(Border), "borderContentItem");
                        frameworkElementFactory.SetValue(Border.HeightProperty, 24.0);
                        frameworkElementFactory.SetValue(Border.MarginProperty, new Thickness(3));
                        frameworkElementFactory.SetValue(Border.BackgroundProperty, new SolidColorBrush(Colors.AliceBlue));
                        FrameworkElementFactory dockPanel = new FrameworkElementFactory(typeof(DockPanel));
                        FrameworkElementFactory button2 = new FrameworkElementFactory(typeof(Button), "buttonContentItem");
                        button2.SetValue(Button.MarginProperty, new Thickness(2, 0, 2, 0));
                        button2.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.Transparent));
                        button2.SetValue(Button.BorderThicknessProperty, new Thickness(0));
                        button2.SetValue(Button.CursorProperty, Cursors.Hand);
                        button2.SetBinding(Button.ContentProperty, new Binding()
                        {
                            Path = new PropertyPath(zMultiCheckAttribute.ContentProperty),
                            Mode = BindingMode.TwoWay,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        });
                        button2.SetBinding(Button.IsEnabledProperty, new Binding()
                        {
                            Path = new PropertyPath(zMultiCheckAttribute.MemberPath),
                            Mode = BindingMode.TwoWay,
                            UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                        });
                        dockPanel.AppendChild(button2);
                        frameworkElementFactory.AppendChild(dockPanel);
                        Trigger trigger = new Trigger();
                        trigger = new Trigger();
                        trigger.SourceName = "buttonContentItem";
                        trigger.Property = Button.IsEnabledProperty;
                        trigger.Value = false;
                        trigger.Setters.Add(new Setter(Button.VisibilityProperty, Visibility.Collapsed, "borderContentItem"));
                        dataTemplateItemsControl.Triggers.Add(trigger);
                        dataTemplateItemsControl.VisualTree = frameworkElementFactory;
                        itemsControl.SetValue(ItemsControl.ItemTemplateProperty, dataTemplateItemsControl);
                        cellFactory.AppendChild(itemsControl);
                        dataTemplate.VisualTree = cellFactory;
                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGrid.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormDateColumnAttribute)
                    {
                        ZFormDateColumnAttribute zDateAttribute = (ZFormDateColumnAttribute)attribute;
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        { Header = zDateAttribute.Title, Width = new DataGridLength(zDateAttribute.Width, zDateAttribute.WidthUnit) };
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));

                        FrameworkElementFactory textBox = new FrameworkElementFactory(typeof(TextBox));
                        textBox.SetValue(TextBox.IsReadOnlyProperty, true);
                        textBox.SetValue(TextBox.PaddingProperty, new Thickness(5, 0, 5, 0));
                        textBox.SetValue(VerticalContentAlignmentProperty, VerticalAlignment.Center);
                        textBox.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));
                        textBox.SetValue(TextBox.StyleProperty, this.FindResource("DataGridTextBox"));
                        textBox.SetBinding(TextBox.ForegroundProperty, new Binding());
                        textBox.SetBinding(TextBox.TextProperty, GetBinding(zDateAttribute, propertyInfo.Name));
                        cellFactory.AppendChild(textBox);

                        dataTemplate.VisualTree = cellFactory;
                        dataGridTemplateColumn.CellTemplate = dataTemplate;
                        dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                            (string.IsNullOrEmpty(zDateAttribute.MemberPath) ? "" : ".") +
                            zDateAttribute.MemberPath;
                        dataGrid.Columns.Add(dataGridTemplateColumn);
                    }
                }
            }
            if (sortButtonColumnDic.Count > 0)
            {
                DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                { Header = string.Empty, Width = DataGridLength.Auto };
                DataTemplate dataTemplate = new DataTemplate();
                FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(StackPanel));
                cellFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
                cellFactory.SetValue(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                foreach (var item in sortButtonColumnDic)
                {
                    PropertyInfo propertyInfo = item.Value;
                    IZFormFuncButton attribute = item.Key;
                    if (attribute == null) continue;
                    if (attribute is ZFormFuncButtonAttribute)
                    {
                        ZFormFuncButtonAttribute zButtonAttribute = (ZFormFuncButtonAttribute)attribute;
                        FrameworkElementFactory button = new FrameworkElementFactory(typeof(Button));
                        button.SetValue(Button.MarginProperty, new Thickness(1, 2, 2, 1));
                        button.SetValue(Button.StyleProperty, this.FindResource(zButtonAttribute.ButtonStyle.ToString()));
                        button.SetValue(ContentProperty, zButtonAttribute.ButtonContent);
                        button.SetBinding(Button.CommandProperty, new Binding() { Path = new PropertyPath(propertyInfo.Name) });
                        MultiBinding multiBinding = new MultiBinding();
                        multiBinding.Converter = new ColumnButtonConverter();
                        multiBinding.Bindings.Add(new Binding() { Path = new PropertyPath(".") });
                        multiBinding.Bindings.Add(new Binding() { Path = new PropertyPath("ItemsSource"), ElementName = "dataGrid" });
                        button.SetBinding(Button.CommandParameterProperty, multiBinding);
                        cellFactory.AppendChild(button);
                    }
                }
                dataTemplate.VisualTree = cellFactory;
                dataGridTemplateColumn.CellTemplate = dataTemplate;
                dataGridTemplateColumn.CanUserResize = false;
                dataGrid.Columns.Add(dataGridTemplateColumn);
            }
            dockPanelTop.Children.Clear();
            dockPanelBottom.Children.Clear();
            List<Button> bottomButtonList = new List<Button>();
            foreach (var item in sortButtonTopDic)
            {
                PropertyInfo propertyInfo = item.Value;
                IZFormFuncButton attribute = item.Key;
                if (attribute is ZFormFuncButtonAttribute)
                {
                    ZFormToolButtonAttribute zButtonAttribute = (ZFormToolButtonAttribute)attribute;
                    Button button = new Button();
                    button.DataContext = Activator.CreateInstance(sourceItemType);
                    button.SetValue(MarginProperty, new Thickness(1, 2, 1, 2));
                    button.SetValue(Button.StyleProperty, this.FindResource(zButtonAttribute.ButtonStyle.ToString()));
                    button.Content = zButtonAttribute.ButtonContent;
                    button.SetValue(DockPanel.DockProperty, Dock.Right);
                    button.SetBinding(Button.CommandProperty, new Binding() { Path = new PropertyPath(propertyInfo.Name) });
                    MultiBinding multiBinding = new MultiBinding();
                    multiBinding.Converter = new ColumnButtonConverter();
                    multiBinding.Bindings.Add(new Binding() { Path = new PropertyPath("ItemsSource"), ElementName = "dataGrid" });
                    button.SetBinding(Button.CommandParameterProperty, multiBinding);
                    //button.Click += async (sd, e) =>
                    //{
                    //    if (!methodInfo.IsStatic)
                    //        throw new ZDataGridException("特性 `ZOperateTopButtonAttribute` 标注方法必须为静态方法！");
                    //    Task task = methodInfo.Invoke(null, new object[] { dataGrid.ItemsSource }) as Task;
                    //    if (TypeUtils.IsAsyncMethod(methodInfo))
                    //        await task;
                    //};
                    if (zButtonAttribute.Location == ButtonLocation.Top)
                    {
                        dockPanelTop.Children.Add(button);
                    }
                    else if (zButtonAttribute.Location == ButtonLocation.Bottom)
                    {
                        button.SetValue(Button.FontSizeProperty, 11.0);
                        button.SetValue(DockPanel.DockProperty, zButtonAttribute.Dock);
                        bottomButtonList.Insert(0, button);
                    }
                }
            }
            foreach (var item in bottomButtonList)
                dockPanelBottom.Children.Add(item);

            UpdateSearchTextMark();
        }

        private void HandleDataContext(object sender, object itemsSource)
        {
            if (sender is FrameworkElement)
            {
                var element = (FrameworkElement)sender;
                element.DataContext = itemsSource;
            }
        }

        private Binding GetBinding(IZFormColumn zFormColumn, string propertyName)
        {
            if (string.IsNullOrEmpty(zFormColumn.MemberPath))
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
                    Path = new PropertyPath(propertyName + "." + zFormColumn.MemberPath),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
            }
        }

        internal class ColumnButtonConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                return new object[] { values[0], values.Length > 1 ? values[1] : null };
            }
            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedItem = dataGrid.SelectedItem;
        }
    }
}
