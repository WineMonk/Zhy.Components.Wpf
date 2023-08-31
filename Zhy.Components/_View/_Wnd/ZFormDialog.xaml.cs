using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Text;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zhy.Components._Attribute;
using Zhy.Components._Attribute._Base;
using Zhy.Components._Attribute._ZFormItem;
using Zhy.Components._Common._Comparer;

namespace Zhy.Components._View._Window
{
    /// <summary>
    /// ZFormDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ZFormDialog : Window
    {
        Func<ObservableObject, bool> _funcValuesCheck = null;
        ObservableObject _observableObject = null;
        public ZFormDialog(ObservableObject observableObject, Func<ObservableObject, bool> funcValuesCheck = null)
        {
            _observableObject = observableObject;
            _funcValuesCheck = funcValuesCheck;
            InitializeComponent();
            InitOwner();
            InitView();
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
                ZFormItemAttribute? zFormItem =
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
                    ItemsControl itemsControl = new ItemsControl();
                    itemsControl.SetValue(ItemsControl.VerticalAlignmentProperty, VerticalAlignment.Center);
                    itemsControl.SetValue(ItemsControl.HorizontalAlignmentProperty, HorizontalAlignment.Center);
                    itemsControl.SetValue(ItemsControl.ItemsPanelProperty,
                        new ItemsPanelTemplate(new FrameworkElementFactory(typeof(WrapPanel))));
                    itemsControl.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
                    {
                        Path = new PropertyPath(propertyInfo.Name),
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });
                    DataTemplate dataTemplate = new DataTemplate();
                    FrameworkElementFactory itemFactory = new FrameworkElementFactory(typeof(DockPanel));
                    FrameworkElementFactory checkBoxFactory = new FrameworkElementFactory(typeof(CheckBox));
                    checkBoxFactory.SetBinding(CheckBox.ContentProperty, new Binding()
                    {
                        Path = new PropertyPath(zFormMultiCheck.ContentProperty),
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });
                    checkBoxFactory.SetBinding(CheckBox.IsCheckedProperty, new Binding()
                    {
                        Path = new PropertyPath(zFormMultiCheck.MemberPath),
                        Mode = BindingMode.TwoWay,
                        UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                    });
                    checkBoxFactory.SetValue(CheckBox.MarginProperty, new Thickness(0, 0, 20, 0));
                    itemFactory.AppendChild(checkBoxFactory);
                    dataTemplate.VisualTree = itemFactory;
                    itemsControl.ItemTemplate = dataTemplate;
                    Border border = new Border();
                    border.SetValue(Border.MinHeightProperty, 30.0);
                    border.SetValue(Border.BorderBrushProperty, new SolidColorBrush(Color.FromRgb(150, 150, 150)));
                    border.SetValue(Border.BorderThicknessProperty, new Thickness(0.8));
                    border.Child = itemsControl;
                    stackPanel.Children.Add(border);
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
