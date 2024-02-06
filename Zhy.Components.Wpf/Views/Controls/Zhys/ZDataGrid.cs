using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Zhy.Components.Wpf.Attributes.Bases;
using Zhy.Components.Wpf.Attributes.Interfaces;
using Zhy.Components.Wpf.Attributes.ZFormColumns;
using Zhy.Components.Wpf.Commons.Comparers;
using Zhy.Components.Wpf.Commons.Utils;
using Zhy.Components.Wpf.Enums;

namespace Zhy.Components.Wpf.Views.Controls.Zhys
{
    /// <summary>
    /// 数据表
    /// </summary>
    public class ZDataGrid : DataGrid
    {
        /// <summary>
        /// 默认按钮样式
        /// </summary>
        public static readonly DependencyProperty DefaultButtonStyleProperty =
            DependencyProperty.Register("DefaultButtonStyle", typeof(Style), typeof(ZDataGrid), new PropertyMetadata(null));
        /// <summary>
        /// 信息按钮样式
        /// </summary>
        public static readonly DependencyProperty InfoButtonStyleProperty =
            DependencyProperty.Register("InfoButtonStyle", typeof(Style), typeof(ZDataGrid), new PropertyMetadata(null));
        /// <summary>
        /// 成功按钮样式
        /// </summary>
        public static readonly DependencyProperty SuccessButtonStyleProperty =
            DependencyProperty.Register("SuccessButtonStyle", typeof(Style), typeof(ZDataGrid), new PropertyMetadata(null));
        /// <summary>
        /// 警告按钮样式
        /// </summary>
        public static readonly DependencyProperty WarnButtonStyleProperty =
            DependencyProperty.Register("WarnButtonStyle", typeof(Style), typeof(ZDataGrid), new PropertyMetadata(null));
        /// <summary>
        /// 错误按钮样式
        /// </summary>
        public static readonly DependencyProperty ErrorButtonStyleProperty =
            DependencyProperty.Register("ErrorButtonStyle", typeof(Style), typeof(ZDataGrid), new PropertyMetadata(null));
        /// <summary>
        /// 查询按钮样式
        /// </summary>
        public static readonly DependencyProperty SearchButtonStyleProperty =
            DependencyProperty.Register("SearchButtonStyle", typeof(Style), typeof(ZDataGrid), new PropertyMetadata(null));
        /// <summary>
        /// 取消查询按钮样式
        /// </summary>
        public static readonly DependencyProperty CancelSearchButtonStyleProperty =
            DependencyProperty.Register("CancelSearchButtonStyle", typeof(Style), typeof(ZDataGrid), new PropertyMetadata(null));

        /// <summary>
        /// 默认按钮样式
        /// </summary>
        public Style DefaultButtonStyle
        {
            get { return (Style)GetValue(DefaultButtonStyleProperty); }
            set { SetValue(DefaultButtonStyleProperty, value); }
        }
        /// <summary>
        /// 信息按钮样式
        /// </summary>
        public Style InfoButtonStyle
        {
            get { return (Style)GetValue(InfoButtonStyleProperty); }
            set { SetValue(InfoButtonStyleProperty, value); }
        }
        /// <summary>
        /// 成功按钮样式
        /// </summary>
        public Style SuccessButtonStyle
        {
            get { return (Style)GetValue(SuccessButtonStyleProperty); }
            set { SetValue(SuccessButtonStyleProperty, value); }
        }
        /// <summary>
        /// 警告按钮样式
        /// </summary>
        public Style WarnButtonStyle
        {
            get { return (Style)GetValue(WarnButtonStyleProperty); }
            set { SetValue(WarnButtonStyleProperty, value); }
        }
        /// <summary>
        /// 错误按钮样式
        /// </summary>
        public Style ErrorButtonStyle
        {
            get { return (Style)GetValue(ErrorButtonStyleProperty); }
            set { SetValue(ErrorButtonStyleProperty, value); }
        }
        /// <summary>
        /// 查询按钮样式
        /// </summary>
        public Style SearchButtonStyle
        {
            get { return (Style)GetValue(SearchButtonStyleProperty); }
            set { SetValue(SearchButtonStyleProperty, value); }
        }
        /// <summary>
        /// 取消查询按钮样式
        /// </summary>
        public Style CancelSearchButtonStyle
        {
            get { return (Style)GetValue(CancelSearchButtonStyleProperty); }
            set { SetValue(CancelSearchButtonStyleProperty, value); }
        }

        /// <summary>
        /// 数据表
        /// </summary>
        public ZDataGrid() : base()
        {
            InitZDataGrid();
            InjectIcon();

        }

        /// <summary>
        /// 项源监听
        /// </summary>
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            base.OnItemsSourceChanged(oldValue, newValue);
            InitializeFormComponent();
            _collectionView = CollectionViewSource.GetDefaultView(newValue);
        }

        /// <summary>
        /// 只读属性监听
        /// </summary>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == IsReadOnlyProperty)
            {
                base.OnPropertyChanged(e);
                InitializeFormComponent();
            }
            base.OnPropertyChanged(e);
        }

        #region Search
        private ICollectionView? _collectionView;
        private void InitSearchComponent()
        {
            DockPanel? dockPanelSearch = Template.FindName("searchZDataGridDockPanel", this) as DockPanel;
            if (dockPanelSearch == null)
            {
                return;
            }
            dockPanelSearch?.Children.Clear();
            Style? searchButtonStyle = null;
            if (SearchButtonStyle != null)
            {
                searchButtonStyle = SearchButtonStyle;
            }
            else
            {
                searchButtonStyle = IZFormItemUtils.FindResource(ZFormButtonStyle.InfoButton.ToString()) as Style;
            }
            Style? cancelSearchButtonStyle = null;
            if (CancelSearchButtonStyle != null)
            {
                cancelSearchButtonStyle = CancelSearchButtonStyle;
            }
            else
            {
                cancelSearchButtonStyle = IZFormItemUtils.FindResource(ZFormButtonStyle.ErrorButton.ToString()) as Style;
            }
            Button buttonSearch = new()
            {
                Name = "buttonSearch",
                Content = "查 询",
                Style = searchButtonStyle,
                Margin = new Thickness(1, 2, 1, 2)
            };
            buttonSearch.SetValue(DockPanel.DockProperty, Dock.Right);
            buttonSearch.Click += ButtonSearch_Click;
            TextBox textBoxSearch = new()
            {
                Name = "textBoxSearch",
                BorderThickness = new Thickness(0.8),
                Margin = new Thickness(1, 2, 1, 2),
                VerticalContentAlignment = VerticalAlignment.Center,
                MinWidth = 200.0,
                Style = IZFormItemUtils.FindResource("InfoTextBox") as Style
            };
            textBoxSearch.SetValue(TextBoxHelper.TextMarkProperty, "输入查询内容");
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
            Button buttonCancelSearch = new Button
            {
                Name = "buttonCancelSearch",
                Content = "取 消",
                Visibility = Visibility.Collapsed,
                Style = cancelSearchButtonStyle,
                Margin = new Thickness(1, 2, 1, 2)
            };
            buttonCancelSearch.SetValue(DockPanel.DockProperty, Dock.Right);
            buttonCancelSearch.Click += (o, e) =>
            {
                textBoxSearch.Text = string.Empty;
                ButtonSearch_Click(o, e);
            };
            dockPanelSearch?.Children.Add(buttonSearch);
            dockPanelSearch?.Children.Add(buttonCancelSearch);
            dockPanelSearch?.Children.Add(textBoxSearch);
            UpdateSearchTextMark();
        }
        private void UpdateSearchTextMark()
        {
            if (ItemsSource == null || !ItemsSource.GetType().IsGenericType)
            {
                return;
            }
            if (Template.FindName("searchZDataGridDockPanel", this) is not DockPanel dockPanelSearch)
            {
                return;
            }
            TextBox? textBoxItem = null;
            foreach (FrameworkElement? child in dockPanelSearch.Children)
            {
                if (child?.Name == "textBoxSearch")
                {
                    textBoxItem = child as TextBox;
                    break;
                }
            }
            if (textBoxItem == null)
            {
                return;
            }
            Type sourceItemType = ItemsSource.GetType().GetGenericArguments()[0];
            PropertyInfo[] propertyInfos = sourceItemType.GetProperties();
            SortedDictionary<IZFormColumn, PropertyInfo> sortColumnTempDic = new(new ZFormSortItemComparer());
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                Attribute? attributeColumn = propertyInfo.GetCustomAttribute(typeof(ZFormItemAttribute), true);
                if (attributeColumn != null && attributeColumn is IZFormColumn zFormItemAttribute)
                {
                    sortColumnTempDic.Add(zFormItemAttribute, propertyInfo);
                }
            }
            string mark = string.Empty;
            foreach (var item in sortColumnTempDic)
            {
                if (item.Key.IsSearchProperty)
                {
                    mark += item.Key.Title.Replace(" ", "") + "/";
                }
            }
            if (mark.Length == 0) return;
            mark = "查询 " + mark.Remove(mark.Length - 1) + " 信息";
            textBoxItem.SetValue(TextBoxHelper.TextMarkProperty, mark);
        }
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (_collectionView == null)
            {
                return;
            }
            if (Template.FindName("searchZDataGridDockPanel", this) is not DockPanel dockPanelSearch)
            {
                return;
            }
            TextBox? textBoxSearch = null;
            Button? buttonCancelSearch = null;
            foreach (FrameworkElement? child in dockPanelSearch.Children)
            {
                if (child?.Name == "textBoxSearch")
                {
                    textBoxSearch = child as TextBox;
                }
                else if (child?.Name == "buttonCancelSearch")
                {
                    buttonCancelSearch = child as Button;
                }
            }
            if (textBoxSearch == null || buttonCancelSearch == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(textBoxSearch.Text))
            {
                _collectionView.Filter = item =>
                {
                    return true;
                };
                buttonCancelSearch.Visibility = Visibility.Collapsed;
            }
            else
            {
                List<PropertyInfo> propertyInfos = GetSearchColumnPropertyInfo(ItemsSource);
                _collectionView.Filter = item =>
                {
                    bool check = CheckItem(item, propertyInfos, textBoxSearch.Text);
                    return check;
                };
                buttonCancelSearch.Visibility = Visibility.Visible;
            }
        }
        private List<PropertyInfo> GetSearchColumnPropertyInfo(object ObservableObjects)
        {
            List<PropertyInfo> columnPropertyInfos = new();
            if (!ObservableObjects.GetType().IsGenericType)
            {
                return columnPropertyInfos;
            }
            Type sourceItemType = ObservableObjects.GetType().GetGenericArguments()[0];
            PropertyInfo[] propertyInfos = sourceItemType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IEnumerable<Attribute> attributes = propertyInfo.GetCustomAttributes();
                foreach (Attribute attribute in attributes)
                {
                    if (attribute is IZFormColumn column && column.IsSearchProperty)
                    {
                        columnPropertyInfos.Add(propertyInfo);
                    }
                }
            }
            return columnPropertyInfos;
        }
        private bool CheckItem(object item, List<PropertyInfo> propertyInfos, string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return true;
            }
            foreach (var propertyInfo in propertyInfos)
            {
                object? val = propertyInfo.GetValue(item, null);
                if (val == null)
                {
                    continue;
                }
                Attribute? attribute = propertyInfo.GetCustomAttribute(typeof(ZFormItemAttribute));
                if (attribute is ZFormComboColumnAttribute zComboDataColumnAttribute && !string.IsNullOrEmpty(zComboDataColumnAttribute.MemberPath))
                {
                    PropertyInfo? pi = val.GetType().GetProperty(zComboDataColumnAttribute.MemberPath);
                    if (pi == null)
                    {
                        continue;
                    }
                    object? vval = pi.GetValue(val, null);
                    if (vval == null)
                    {
                        continue;
                    }
                    if (vval?.ToString()?.ToLower().Contains(searchText.ToLower()) == true)
                    {
                        return true;
                    }
                }
                else if (attribute is ZFormTextButtonColumnAttribute zFormTextButtonColumnAttribute && !string.IsNullOrEmpty(zFormTextButtonColumnAttribute.MemberPath))
                {
                    PropertyInfo? pi = val.GetType().GetProperty(zFormTextButtonColumnAttribute.MemberPath);
                    if (pi == null)
                    {
                        continue;
                    }
                    object? vval = pi.GetValue(val, null);
                    if (vval == null)
                    {
                        continue;
                    }
                    if (vval?.ToString()?.ToLower().Contains(searchText.ToLower()) == true)
                    {
                        return true;
                    }
                }
                else if (attribute is ZFormMultiCheckColumnAttribute zFormMultiCheck && !string.IsNullOrEmpty(zFormMultiCheck.MemberPath))
                {
                    if (val is not IEnumerable objects)
                    {
                        continue;
                    }
                    foreach (var obj in objects)
                    {
                        PropertyInfo? propertyInfoMember = obj.GetType().GetProperty(zFormMultiCheck.MemberPath);
                        PropertyInfo? propertyInfoContent = obj.GetType().GetProperty(zFormMultiCheck.ContentProperty);
                        if (propertyInfoMember == null || propertyInfoContent == null)
                        {
                            continue;
                        }
                        object? member = propertyInfoMember.GetValue(obj, null);
                        object? content = propertyInfoContent.GetValue(obj, null);
                        if (member == null || content == null)
                        {
                            continue;
                        }
                        bool.TryParse(member.ToString(), out bool check);
                        if (!check)
                        {
                            continue;
                        }
                        if (content?.ToString()?.ToLower().Contains(searchText.ToLower()) == true)
                        {
                            return true;
                        }
                    }
                }
                else
                {
                    if (val?.ToString()?.ToLower().Contains(searchText.ToLower()) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        #endregion Search

        #region Form
        private void InitializeFormComponent()
        {
            if (ItemsSource == null || !ItemsSource.GetType().IsGenericType)
            {
                return;
            }
            Columns.Clear();
            Type sourceItemType = ItemsSource.GetType().GetGenericArguments()[0];
            PropertyInfo[] propertyInfos = sourceItemType.GetProperties();
            SortedDictionary<IZFormColumn, PropertyInfo> columnPropertyInfoDic = new(new ZFormSortItemComparer());
            SortedDictionary<IZFormFuncButton, PropertyInfo> columnButtonDic = new(new ZFormSortItemComparer());
            SortedDictionary<IZFormFuncButton, PropertyInfo> toolButtonDic = new(new ZFormSortItemComparer(true));
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                List<Attribute> attributes = propertyInfo.GetCustomAttributes().ToList();
                Attribute? attributeColumn = propertyInfo.GetCustomAttribute(typeof(ZFormItemAttribute), true);
                Attribute? attributeButton = propertyInfo.GetCustomAttribute(typeof(ZFormFuncButtonAttribute), true);
                if (attributeColumn != null && attributeColumn is IZFormColumn zFormItemAttribute)
                {
                    if (!zFormItemAttribute.IsHideFormColumn)
                    {
                        columnPropertyInfoDic.Add(zFormItemAttribute, propertyInfo);
                    }
                }
                if (attributeButton != null && attributeButton is IZFormFuncButton zFormFuncButtonAttribute)
                {
                    if (attributeButton is ZFormOperColumnButtonAttribute)
                    {
                        columnButtonDic.Add(zFormFuncButtonAttribute, propertyInfo);
                    }
                    else if (attributeButton is ZFormToolButtonAttribute)
                    {
                        toolButtonDic.Add(zFormFuncButtonAttribute, propertyInfo);
                    }
                }
            }
            foreach (var item in columnPropertyInfoDic)
            {
                PropertyInfo propertyInfo = item.Value;
                IZFormColumn? attribute = item.Key;
                if (attribute == null) continue;
                if (!IsReadOnly && !attribute.IsReadOnlyColumn)
                {
                    if (attribute is ZFormTextColumnAttribute zTextAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetTextColumn(zTextAttribute, propertyInfo);
                        Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormTextButtonColumnAttribute zButtonAttribute)
                    {
                        Style? buttonStyle = null;
                        switch (zButtonAttribute.ButtonStyle)
                        {
                            case ZFormButtonStyle.DefaultButton:
                                buttonStyle = DefaultButtonStyle;
                                break;
                            case ZFormButtonStyle.InfoButton:
                                buttonStyle = InfoButtonStyle;
                                break;
                            case ZFormButtonStyle.WarnButton:
                                buttonStyle = WarnButtonStyle;
                                break;
                            case ZFormButtonStyle.ErrorButton:
                                buttonStyle = ErrorButtonStyle;
                                break;
                            case ZFormButtonStyle.SuccessButton:
                                buttonStyle = SuccessButtonStyle;
                                break;
                            default:
                                break;
                        }
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetTextButtonColumn(zButtonAttribute, propertyInfo, buttonStyle);
                        Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormCheckColumnAttribute zCheckAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetCheckColumn(zCheckAttribute, propertyInfo);
                        Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormComboColumnAttribute zComboAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetComboColumn(zComboAttribute, propertyInfo);
                        Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormMultiCheckColumnAttribute zMultiCheckAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetMultiCheckColumn(zMultiCheckAttribute, propertyInfo);
                        Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormDateColumnAttribute zDateAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetDateColumn(zDateAttribute, propertyInfo);
                        Columns.Add(dataGridTemplateColumn);
                    }
                }
                else
                {
                    if (attribute is ZFormTextColumnAttribute zTextAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetTextReadOnlyColumn(zTextAttribute, propertyInfo);
                        Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormTextButtonColumnAttribute zButtonAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetTextReadOnlyColumn(zButtonAttribute, propertyInfo);
                        Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormCheckColumnAttribute zCheckAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetCheckReadOnlyColumn(zCheckAttribute, propertyInfo);
                        Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormComboColumnAttribute zComboAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetTextReadOnlyColumn(zComboAttribute, propertyInfo);
                        Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormMultiCheckColumnAttribute zMultiCheckAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetMultiCheckReadOnlyColumn(zMultiCheckAttribute, propertyInfo);
                        Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormDateColumnAttribute zDateAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetTextReadOnlyColumn(zDateAttribute, propertyInfo);
                        Columns.Add(dataGridTemplateColumn);
                    }
                }
            }
            if (columnButtonDic.Count > 0)
            {
                Dictionary<ZFormButtonStyle, Style> styleDic = new Dictionary<ZFormButtonStyle, Style>
                {
                    { ZFormButtonStyle.DefaultButton, DefaultButtonStyle },
                    { ZFormButtonStyle.InfoButton, InfoButtonStyle },
                    { ZFormButtonStyle.SuccessButton, SuccessButtonStyle },
                    { ZFormButtonStyle.WarnButton, WarnButtonStyle },
                    { ZFormButtonStyle.ErrorButton, ErrorButtonStyle }
                };
                DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetButtonColumn(columnButtonDic, styleDic);
                Columns.Add(dataGridTemplateColumn);
            }
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                InitSearchComponent();
                InitToolComponent(toolButtonDic);
            }));
        }
        private void InitToolComponent(SortedDictionary<IZFormFuncButton, PropertyInfo> toolButtonDic)
        {
            DockPanel? dockPanelTop = Template.FindName("topZDataGridDockPanel", this) as DockPanel;
            dockPanelTop?.Children.Clear();
            DockPanel? dockPanelBottom = Template.FindName("bottomZDataGridDockPanel", this) as DockPanel;
            dockPanelBottom?.Children.Clear();
            List<Button> bottomButtonList = new();
            foreach (var item in toolButtonDic)
            {
                PropertyInfo propertyInfo = item.Value;
                IZFormFuncButton attribute = item.Key;
                if (attribute is ZFormToolButtonAttribute zButtonAttribute)
                {
                    Button toolButton = new()
                    {
                        Content = zButtonAttribute.ButtonContent,
                        Margin = new Thickness(1, 2, 1, 2),
                    };
                    if (propertyInfo.ReflectedType != null)
                    {
                        toolButton.DataContext = Activator.CreateInstance(propertyInfo.ReflectedType);
                    }
                    Style? buttonStyle = null;
                    switch (zButtonAttribute.ButtonStyle)
                    {
                        case ZFormButtonStyle.DefaultButton:
                            buttonStyle = DefaultButtonStyle;
                            break;
                        case ZFormButtonStyle.InfoButton:
                            buttonStyle = InfoButtonStyle;
                            break;
                        case ZFormButtonStyle.WarnButton:
                            buttonStyle = WarnButtonStyle;
                            break;
                        case ZFormButtonStyle.ErrorButton:
                            buttonStyle = ErrorButtonStyle;
                            break;
                        case ZFormButtonStyle.SuccessButton:
                            buttonStyle = SuccessButtonStyle;
                            break;
                        default:
                            break;
                    }
                    if (buttonStyle != null)
                    {
                        toolButton.SetValue(StyleProperty, buttonStyle);
                    }
                    else if (IZFormItemUtils.FindResource(zButtonAttribute.ButtonStyle.ToString()) is Style toolButtonStyle)
                    {
                        toolButton.SetValue(StyleProperty, toolButtonStyle);
                    }
                    toolButton.SetValue(DockPanel.DockProperty, Dock.Right);
                    toolButton.SetBinding(System.Windows.Controls.Primitives.ButtonBase.CommandProperty, new Binding()
                    {
                        Path = new PropertyPath(propertyInfo.Name)
                    });
                    toolButton.SetBinding(System.Windows.Controls.Primitives.ButtonBase.CommandParameterProperty, new Binding()
                    {
                        Path = new PropertyPath("ItemsSource"),
                        RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGrid), 1)
                    });
                    if (zButtonAttribute.Location == ButtonLocation.Top)
                    {
                        dockPanelTop?.Children.Add(toolButton);
                    }
                    else if (zButtonAttribute.Location == ButtonLocation.Bottom)
                    {
                        toolButton.SetValue(FontSizeProperty, 11.0);
                        toolButton.SetValue(DockPanel.DockProperty, zButtonAttribute.Dock);
                        bottomButtonList.Insert(0, toolButton);
                    }
                }
            }
            bottomButtonList.ForEach(button => { dockPanelBottom?.Children.Add(button); });
        }
        #endregion Form

        private void InitZDataGrid()
        {
            if (IZFormItemUtils.FindResource("ZDataGridStyle") is Style zDataGridStyle)
            {
                Style = zDataGridStyle;
            }
            CanUserAddRows = false;
            AutoGenerateColumns = false;
            IsReadOnly = true;
        }
        private void InjectIcon()
        {
            Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                try
                {
                    DependencyObject parent = VisualTreeHelper.GetParent(this);
                    while (parent != null && !(parent is Window))
                    {
                        parent = VisualTreeHelper.GetParent(parent);
                    }
                    if (parent != null && parent is Window window)
                    {
                        if (window.Icon == null)
                        {
                            window.Icon = new BitmapImage(new("pack://application:,,,/Zhy.Components.Wpf;component\\Resources\\Icons\\logo_light.ico"));
                        }
                    }
                }
                catch { }
            }));
        }
    }
}
