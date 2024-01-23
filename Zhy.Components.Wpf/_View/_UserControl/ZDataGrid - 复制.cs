using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Zhy.Components.Wpf._Attribute;
using Zhy.Components.Wpf._Attribute._Base;
using Zhy.Components.Wpf._Attribute._ZFormColumn;
using Zhy.Components.Wpf._Common._Comparer;
using Zhy.Components.Wpf._Common._Utils;
using Zhy.Components.Wpf._Enum;

namespace Zhy.Components.Wpf._View._UserControl
{
    /// <summary>
    /// 数据表
    /// </summary>
    public class ZDataGrid : DataGrid
    {
        /// <summary>
        /// 数据表
        /// </summary>
        public ZDataGrid() : base()
        {
            ResourceDictionary resourceDict = new ResourceDictionary();
            resourceDict.Source = new Uri("/Zhy.Components.Wpf;component/_View/_Theme/AppDictionary.xaml", UriKind.Relative);
            this.Resources.MergedDictionaries.Add(resourceDict);
            this.Style = (Style)this.Resources["ZDataGridStyle"];
            CanUserAddRows = false;
            AutoGenerateColumns = false;
        }

        private bool _isLoad = false;

        /// <summary>
        /// 项源监听
        /// </summary>
        protected override void OnItemsSourceChanged(IEnumerable oldValue, IEnumerable newValue)
        {
            InitializeColumns();
            base.OnItemsSourceChanged(oldValue, newValue);
            _isLoad = true;
        }

        private void InitializeColumns()
        {
            if (ItemsSource == null || !ItemsSource.GetType().IsGenericType)
                return;
            _collectionView = CollectionViewSource.GetDefaultView(ItemsSource);
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
                if (attributeColumn != null && attributeColumn is IZFormColumn zFormItemAttribute)
                {
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
            this.Columns.Clear();
            foreach (var item in sortColumnTempDic)
            {
                PropertyInfo propertyInfo = item.Value;
                IZFormColumn? attribute = item.Key;
                if (attribute == null) continue;
                if ((!IsReadOnly && !attribute.IsReadOnlyColumn))
                {
                    if (attribute is ZFormTextColumnAttribute zTextAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = IZFormItemUtils.GetColumn(zTextAttribute, propertyInfo);
                        //DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        //{ Header = zTextAttribute.Title, Width = new DataGridLength(zTextAttribute.Width, zTextAttribute.WidthUnit) };
                        //DataTemplate dataTemplate = new DataTemplate();
                        //FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));

                        //FrameworkElementFactory textBox = new FrameworkElementFactory(typeof(TextBox));
                        //textBox.SetValue(TextBox.PaddingProperty, new Thickness(5, 0, 5, 0));
                        //textBox.SetValue(VerticalContentAlignmentProperty, VerticalAlignment.Center);
                        //textBox.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));
                        //textBox.SetValue(TextBox.StyleProperty, this.FindResource("DataGridTextBox"));
                        //textBox.SetBinding(TextBox.ForegroundProperty, new Binding());
                        //textBox.SetBinding(TextBox.TextProperty, GetBinding(zTextAttribute, propertyInfo.Name));
                        //cellFactory.AppendChild(textBox);

                        //dataTemplate.VisualTree = cellFactory;
                        //dataGridTemplateColumn.CellTemplate = dataTemplate;
                        //dataGridTemplateColumn.SortMemberPath = propertyInfo.Name +
                        //    (string.IsNullOrEmpty(zTextAttribute.MemberPath) ? "" : ".") +
                        //    zTextAttribute.MemberPath;
                        this.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormTextButtonColumnAttribute zButtonAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        { Header = zButtonAttribute.Title, Width = new DataGridLength(zButtonAttribute.Width, zButtonAttribute.WidthUnit) };
                        DataTemplate dataTemplate = new DataTemplate();
                        FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(DockPanel));

                        FrameworkElementFactory button = new FrameworkElementFactory(typeof(Button));
                        button.SetValue(DockPanel.DockProperty, Dock.Right);
                        button.SetValue(Button.ContentProperty, zButtonAttribute.ButtonContent);
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
                        this.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormCheckColumnAttribute zCheckAttribute)
                    {
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
                        this.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormComboColumnAttribute zComboAttribute)
                    {
                        DataGridTemplateColumn dataGridTemplateColumn = new DataGridTemplateColumn()
                        { Header = zComboAttribute.Title, Width = new DataGridLength(zComboAttribute.Width, zComboAttribute.WidthUnit) };
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
                        this.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormMultiCheckColumnAttribute zMultiCheckAttribute)
                    {
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
                        this.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormDateColumnAttribute zDateAttribute)
                    {
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
                        this.Columns.Add(dataGridTemplateColumn);
                    }
                }
                else
                {
                    if (attribute is ZFormTextColumnAttribute zTextAttribute)
                    {
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
                        this.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormTextButtonColumnAttribute zButtonAttribute)
                    {
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
                        this.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormCheckColumnAttribute zCheckAttribute)
                    {
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
                        this.Columns.Add(dataGridTemplateColumn);

                        Style elementStyle = new Style(typeof(CheckBox));
                        elementStyle.Setters.Add(new Setter(VerticalAlignmentProperty, VerticalAlignment.Center));
                        elementStyle.Setters.Add(new Setter(HorizontalAlignmentProperty, HorizontalAlignment.Center));
                    }
                    else if (attribute is ZFormComboColumnAttribute zComboAttribute)
                    {
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
                        this.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormMultiCheckColumnAttribute zMultiCheckAttribute)
                    {
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
                        this.Columns.Add(dataGridTemplateColumn);
                    }
                    else if (attribute is ZFormDateColumnAttribute zDateAttribute)
                    {
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
                        this.Columns.Add(dataGridTemplateColumn);
                    }
                }
            }
            this.Dispatcher.BeginInvoke(DispatcherPriority.Loaded, new Action(() =>
            {
                InitSearchComponent();
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
                        if (attribute is ZFormFuncButtonAttribute zButtonAttribute)
                        {
                            FrameworkElementFactory button = new FrameworkElementFactory(typeof(Button));
                            button.SetValue(Button.MarginProperty, new Thickness(1, 2, 2, 1));
                            button.SetValue(Button.StyleProperty, this.FindResource(zButtonAttribute.ButtonStyle.ToString()));
                            button.SetValue(Button.ContentProperty, zButtonAttribute.ButtonContent);
                            button.SetBinding(Button.CommandProperty, new Binding() { Path = new PropertyPath(propertyInfo.Name) });
                            MultiBinding multiBinding = new MultiBinding();
                            multiBinding.Converter = new ColumnButtonConverter();
                            multiBinding.Bindings.Add(new Binding() { Path = new PropertyPath(".") });
                            multiBinding.Bindings.Add(new Binding()
                            {
                                Path = new PropertyPath("ItemsSource"),
                                RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGrid), 1)
                            });
                            button.SetBinding(Button.CommandParameterProperty, multiBinding);
                            cellFactory.AppendChild(button);
                        }
                    }
                    dataTemplate.VisualTree = cellFactory;
                    dataGridTemplateColumn.CellTemplate = dataTemplate;
                    dataGridTemplateColumn.CanUserResize = false;
                    this.Columns.Add(dataGridTemplateColumn);
                }

                DockPanel? dockPanelTop = this.Template.FindName("topZDataGridDockPanel", this) as DockPanel;
                dockPanelTop?.Children.Clear();
                List<Button> bottomButtonList = new List<Button>();
                foreach (var item in sortButtonTopDic)
                {
                    PropertyInfo propertyInfo = item.Value;
                    IZFormFuncButton attribute = item.Key;
                    if (attribute is ZFormToolButtonAttribute zButtonAttribute)
                    {
                        Button button = new Button();
                        button.DataContext = Activator.CreateInstance(sourceItemType);
                        button.SetValue(MarginProperty, new Thickness(1, 2, 1, 2));
                        button.SetValue(Button.StyleProperty, this.FindResource(zButtonAttribute.ButtonStyle.ToString()));
                        button.Content = zButtonAttribute.ButtonContent;
                        button.SetValue(DockPanel.DockProperty, Dock.Right);
                        button.SetBinding(Button.CommandProperty, new Binding() { Path = new PropertyPath(propertyInfo.Name) });
                        button.SetBinding(Button.CommandParameterProperty, new Binding()
                        {
                            Path = new PropertyPath("ItemsSource"),
                            RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGrid), 1)
                        });
                        if (zButtonAttribute.Location == ButtonLocation.Top)
                        {
                            dockPanelTop?.Children.Add(button);
                        }
                        else if (zButtonAttribute.Location == ButtonLocation.Bottom)
                        {
                            button.SetValue(Button.FontSizeProperty, 11.0);
                            button.SetValue(DockPanel.DockProperty, zButtonAttribute.Dock);
                            bottomButtonList.Insert(0, button);
                        }
                    }
                }
                DockPanel? dockPanelBottom = this.Template.FindName("bottomZDataGridDockPanel", this) as DockPanel;
                dockPanelBottom?.Children.Clear();
                foreach (var item in bottomButtonList)
                {
                    dockPanelBottom?.Children.Add(item);
                }
            }));
        }

        /// <summary>
        /// 只读属性监听
        /// </summary>
        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            if (e.Property == ZDataGrid.IsReadOnlyProperty)
            {
                if (!_isLoad)
                {
                    base.OnPropertyChanged(e);
                    return;
                }
                InitializeColumns();
            }
            base.OnPropertyChanged(e);
        }

        #region Search
        private ICollectionView? _collectionView;
        private void InitSearchComponent()
        {
            DockPanel? dockPanelSearch = this.Template.FindName("searchZDataGridDockPanel", this) as DockPanel;
            if(dockPanelSearch == null)
            {
                return;
            }
            dockPanelSearch?.Children.Clear();
            Button buttonSearch = new Button();
            buttonSearch.Name = "buttonSearch";
            buttonSearch.Content = "查 询";
            buttonSearch.Style = this.FindResource("InfoButton") as Style;
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

            dockPanelSearch?.Children.Add(buttonSearch);
            dockPanelSearch?.Children.Add(buttonCancelSearch);
            dockPanelSearch?.Children.Add(textBoxSearch);
            UpdateSearchTextMark();
        }
        private void UpdateSearchTextMark()
        {
            if (ItemsSource == null || !ItemsSource.GetType().IsGenericType)
                return;
            DockPanel? dockPanelSearch = this.Template.FindName("searchZDataGridDockPanel", this) as DockPanel;
            if (dockPanelSearch == null) return;
            Type sourceItemType = ItemsSource.GetType().GetGenericArguments()[0];
            PropertyInfo[] propertyInfos = sourceItemType.GetProperties();
            SortedDictionary<IZFormColumn, PropertyInfo> sortColumnTempDic =
                new SortedDictionary<IZFormColumn, PropertyInfo>(new ZFormSortItemComparer());
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
                    if (mark.Length > 0)
                        mark += " / ";
                    mark += item.Key.Title.Replace(" ", "");
                }
            }
            if (mark.Length == 0)
                return;
            mark = "查询 " + mark + " 信息";
            foreach (var item in dockPanelSearch.Children)
            {
                if (item is TextBox textBoxItem  && textBoxItem.Name == "textBoxSearch")
                {
                    textBoxItem.SetValue(_Common._Utils.TextBoxHelper.TextMarkProperty, mark);
                    return;
                }
            }
        }
        private void ButtonSearch_Click(object sender, RoutedEventArgs e)
        {
            if (_collectionView == null) return;
            DockPanel? dockPanelSearch = this.Template.FindName("searchZDataGridDockPanel", this) as DockPanel;
            if (dockPanelSearch == null) return;
            TextBox? textBoxSearch = null;
            Button? buttonCancelSearch = null;
            foreach (var item in dockPanelSearch.Children)
            {
                if(item is TextBox textBox && textBox.Name == "textBoxSearch")
                {
                    textBoxSearch = item as TextBox;
                }
                else if(item is Button button && button.Name == "buttonCancelSearch")
                {
                    buttonCancelSearch = item as Button;
                }
            }
            if (textBoxSearch == null) return;
            if (buttonCancelSearch == null) return;
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
            List<PropertyInfo> columnPropertyInfos = new List<PropertyInfo>();
            if (!ObservableObjects.GetType().IsGenericType)
                return columnPropertyInfos;
            Type sourceItemType = ObservableObjects.GetType().GetGenericArguments()[0];
            PropertyInfo[] propertyInfos = sourceItemType.GetProperties();
            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                IEnumerable<Attribute> attributes = propertyInfo.GetCustomAttributes();
                foreach (Attribute attribute in attributes)
                    if (attribute is IZFormColumn column && column.IsSearchProperty)
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
                if (attribute is ZFormComboColumnAttribute zComboDataColumnAttribute && !string.IsNullOrEmpty(zComboDataColumnAttribute.MemberPath))
                {
                    PropertyInfo? pi = val.GetType().GetProperty(zComboDataColumnAttribute.MemberPath);
                    if (pi == null) continue;
                    object? vval = pi.GetValue(val, null);
                    if (vval == null) continue;
                    if (vval?.ToString()?.ToLower().Contains(searchText.ToLower()) == true)
                        return true;
                }
                else if (attribute is ZFormTextButtonColumnAttribute zFormTextButtonColumnAttribute && !string.IsNullOrEmpty(zFormTextButtonColumnAttribute.MemberPath))
                {
                    PropertyInfo? pi = val.GetType().GetProperty((zFormTextButtonColumnAttribute).MemberPath);
                    if (pi == null) continue;
                    object? vval = pi.GetValue(val, null);
                    if (vval == null) continue;
                    if (vval?.ToString()?.ToLower().Contains(searchText.ToLower()) == true)
                        return true;
                }
                else if (attribute is ZFormMultiCheckColumnAttribute zFormMultiCheck && !string.IsNullOrEmpty(zFormMultiCheck.MemberPath))
                {
                    IEnumerable? objects = val as IEnumerable;
                    if (objects == null)
                        continue;
                    foreach (var obj in objects)
                    {
                        PropertyInfo? propertyInfoMember = obj.GetType().GetProperty(zFormMultiCheck.MemberPath);
                        PropertyInfo? propertyInfoContent = obj.GetType().GetProperty(zFormMultiCheck.ContentProperty);
                        if (propertyInfoMember == null || propertyInfoContent == null)
                            continue;
                        object? member = propertyInfoMember.GetValue(obj, null);
                        object? content = propertyInfoContent.GetValue(obj, null);
                        if (member == null || content == null)
                            continue;
                        bool check = false;
                        bool.TryParse(member.ToString(), out check);
                        if (!check)
                            continue;
                        if (content?.ToString()?.ToLower().Contains(searchText.ToLower()) == true)
                            return true;
                    }
                }
                else
                {
                    if (val?.ToString()?.ToLower().Contains(searchText.ToLower()) == true)
                        return true;
                }
            }
            return false;
        }
        #endregion Search

        internal class ColumnButtonConverter : IMultiValueConverter
        {
            public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            {
                object? item = null;
                IList? list = null;
                if (values != null)
                {
                    if (values.Length > 0)
                    {
                        item = values[0];
                    }
                    if (values.Length > 1)
                    {
                        list = values[1] as IList;
                    }
                }
                return Tuple.Create(item, list);
            }
            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
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
    }
}
