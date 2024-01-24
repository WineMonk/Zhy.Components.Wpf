using System;
using System.Collections.Generic;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using Zhy.Components.Wpf.Attributes.Bases;
using Zhy.Components.Wpf.Attributes.Interfaces;
using Zhy.Components.Wpf.Attributes.ZFormColumns;
using Zhy.Components.Wpf.Commons.Converters;
using Zhy.Components.Wpf.Enums;
using Zhy.Components.Wpf.Views.Controls.Zhys;

namespace Zhy.Components.Wpf.Commons.Utils
{
    internal class IZFormItemUtils
    {
        public static DataGridTemplateColumn GetButtonColumn(IEnumerable<KeyValuePair<IZFormFuncButton, PropertyInfo>> buttonPropertyDic,
            Dictionary<ZFormButtonStyle, Style> buttonStyleDir)
        {
            FrameworkElementFactory cellFactory = new FrameworkElementFactory(typeof(StackPanel));
            cellFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
            cellFactory.SetValue(StackPanel.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            foreach (var item in buttonPropertyDic)
            {
                PropertyInfo propertyInfo = item.Value;
                IZFormFuncButton zFormFuncButton = item.Key;
                if (zFormFuncButton is ZFormFuncButtonAttribute zButtonAttribute)
                {
                    FrameworkElementFactory buttonFactory = new(typeof(Button));
                    buttonFactory.SetValue(Button.MarginProperty, new Thickness(1, 2, 2, 1));
                    buttonFactory.SetValue(Button.ContentProperty, zButtonAttribute.ButtonContent);
                    buttonFactory.SetBinding(Button.CommandProperty, new Binding() { Path = new PropertyPath(propertyInfo.Name) });
                    if (buttonStyleDir.TryGetValue(zButtonAttribute.ButtonStyle, out Style? buttonStyle) && buttonStyle != null)
                    {
                        buttonFactory.SetValue(Button.StyleProperty, buttonStyle);
                    }
                    else if (FindResource(zButtonAttribute.ButtonStyle.ToString()) is Style buttonStyle1)
                    {
                        buttonFactory.SetValue(Button.StyleProperty, buttonStyle1);
                    }
                    MultiBinding multiBinding = new()
                    {
                        Converter = new ColumnButtonConverter()
                    };
                    multiBinding.Bindings.Add(new Binding() { Path = new PropertyPath(".") });
                    multiBinding.Bindings.Add(new Binding()
                    {
                        Path = new PropertyPath("ItemsSource"),
                        RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(DataGrid), 1)
                    });
                    buttonFactory.SetBinding(Button.CommandParameterProperty, multiBinding);
                    cellFactory.AppendChild(buttonFactory);
                }
            }
            DataGridTemplateColumn dataGridTemplateColumn = new()
            {
                Header = string.Empty,
                Width = DataGridLength.Auto,
                CellTemplate = new DataTemplate()
                {
                    VisualTree = cellFactory
                },
                CanUserResize = false
            };
            return dataGridTemplateColumn;
        }

        public static DataGridTemplateColumn GetTextColumn(ZFormTextColumnAttribute zTextAttribute, PropertyInfo propertyInfo)
        {
            FrameworkElementFactory textBoxFactory = new(typeof(TextBox));
            textBoxFactory.SetValue(TextBox.PaddingProperty, new Thickness(5, 0, 5, 0));
            textBoxFactory.SetValue(TextBox.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            textBoxFactory.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));
            textBoxFactory.SetValue(TextBox.ForegroundProperty, new SolidColorBrush(Colors.Black));
            textBoxFactory.SetBinding(TextBox.TextProperty, GetBinding(zTextAttribute, propertyInfo));
            if (FindResource("DataGridTextBox") is Style textBoxStyle)
            {
                textBoxFactory.SetValue(TextBox.StyleProperty, textBoxStyle);
            }
            FrameworkElementFactory cellFactory = new(typeof(DockPanel));
            cellFactory.AppendChild(textBoxFactory);
            DataGridTemplateColumn dataGridTemplateColumn = GetBaseColumn(zTextAttribute, propertyInfo, cellFactory);
            return dataGridTemplateColumn;
        }
        public static DataGridTemplateColumn GetTextReadOnlyColumn(IZFormColumn zFormColumn, PropertyInfo propertyInfo)
        {
            FrameworkElementFactory textBoxFactory = new(typeof(TextBox));
            textBoxFactory.SetValue(TextBox.IsReadOnlyProperty, true);
            textBoxFactory.SetValue(TextBox.PaddingProperty, new Thickness(5, 0, 5, 0));
            textBoxFactory.SetValue(TextBox.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            textBoxFactory.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));
            textBoxFactory.SetValue(TextBox.ForegroundProperty, new SolidColorBrush(Colors.Black));
            textBoxFactory.SetBinding(TextBox.TextProperty, GetBinding(zFormColumn, propertyInfo));
            if (FindResource("DataGridTextBox") is Style textBoxStyle)
            {
                textBoxFactory.SetValue(TextBox.StyleProperty, textBoxStyle);
            }
            FrameworkElementFactory cellFactory = new(typeof(DockPanel));
            cellFactory.AppendChild(textBoxFactory);
            DataGridTemplateColumn dataGridTemplateColumn = GetBaseColumn(zFormColumn, propertyInfo, cellFactory);
            return dataGridTemplateColumn;
        }
        public static DataGridTemplateColumn GetTextButtonColumn(ZFormTextButtonColumnAttribute zButtonAttribute, PropertyInfo propertyInfo, Style? style)
        {
            FrameworkElementFactory buttonFactory = new(typeof(Button));
            buttonFactory.SetValue(DockPanel.DockProperty, Dock.Right);
            buttonFactory.SetValue(Button.ContentProperty, zButtonAttribute.ButtonContent);
            buttonFactory.SetValue(Button.MarginProperty, new Thickness(1, 2, 2, 1));
            buttonFactory.SetBinding(Button.CommandProperty, new Binding() { Path = new PropertyPath(zButtonAttribute.RelayCommandName) });
            buttonFactory.SetBinding(Button.CommandParameterProperty, new Binding() { Path = new PropertyPath(".") });
            if (style != null)
            {
                buttonFactory.SetValue(TextBox.StyleProperty, style);
            }
            else if (FindResource(zButtonAttribute.ButtonStyle.ToString()) is Style buttonStyle)
            {
                buttonFactory.SetValue(Button.StyleProperty, buttonStyle);
            }
            FrameworkElementFactory textBoxFactory = new(typeof(TextBox));
            textBoxFactory.SetValue(TextBox.PaddingProperty, new Thickness(5, 0, 5, 0));
            textBoxFactory.SetValue(TextBox.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            textBoxFactory.SetValue(TextBox.IsReadOnlyProperty, true);
            textBoxFactory.SetValue(TextBox.BorderThicknessProperty, new Thickness(0));
            textBoxFactory.SetValue(TextBox.ForegroundProperty, new SolidColorBrush(Colors.Black));
            textBoxFactory.SetBinding(TextBox.TextProperty, GetBinding(zButtonAttribute, propertyInfo));
            if (FindResource("DataGridTextBox") is Style textBoxStyle)
            {
                textBoxFactory.SetValue(TextBox.StyleProperty, textBoxStyle);
            }
            FrameworkElementFactory cellFactory = new(typeof(DockPanel));
            cellFactory.AppendChild(buttonFactory);
            cellFactory.AppendChild(textBoxFactory);
            DataGridTemplateColumn dataGridTemplateColumn = GetBaseColumn(zButtonAttribute, propertyInfo, cellFactory);
            return dataGridTemplateColumn;
        }

        public static DataGridTemplateColumn GetCheckReadOnlyColumn(ZFormCheckColumnAttribute zCheckAttribute, PropertyInfo propertyInfo)
        {
            FrameworkElementFactory checkBoxFactory = new FrameworkElementFactory(typeof(CheckBox));
            checkBoxFactory.SetValue(CheckBox.IsEnabledProperty, false);
            checkBoxFactory.SetValue(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Center);
            checkBoxFactory.SetValue(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            checkBoxFactory.SetBinding(CheckBox.IsCheckedProperty, GetBinding(zCheckAttribute, propertyInfo));
            FrameworkElementFactory cellFactory = new(typeof(DockPanel));
            cellFactory.AppendChild(checkBoxFactory);
            DataGridTemplateColumn dataGridTemplateColumn = GetBaseColumn(zCheckAttribute, propertyInfo, cellFactory);
            return dataGridTemplateColumn;
        }

        public static DataGridTemplateColumn GetCheckColumn(ZFormCheckColumnAttribute zCheckAttribute, PropertyInfo propertyInfo)
        {
            FrameworkElementFactory checkBoxFactory = new FrameworkElementFactory(typeof(CheckBox));
            checkBoxFactory.SetValue(CheckBox.IsEnabledProperty, !zCheckAttribute.IsReadOnlyColumn);
            checkBoxFactory.SetValue(CheckBox.VerticalAlignmentProperty, VerticalAlignment.Center);
            checkBoxFactory.SetValue(CheckBox.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            checkBoxFactory.SetBinding(CheckBox.IsCheckedProperty, GetBinding(zCheckAttribute, propertyInfo));
            FrameworkElementFactory cellFactory = new(typeof(DockPanel));
            cellFactory.AppendChild(checkBoxFactory);
            DataGridTemplateColumn dataGridTemplateColumn = GetBaseColumn(zCheckAttribute, propertyInfo, cellFactory);
            return dataGridTemplateColumn;
        }

        public static DataGridTemplateColumn GetComboColumn(ZFormComboColumnAttribute zComboAttribute, PropertyInfo propertyInfo)
        {
            FrameworkElementFactory comboBoxFactory = new FrameworkElementFactory(typeof(ComboBox));
            comboBoxFactory.SetValue(ComboBox.BorderThicknessProperty, new Thickness(0));
            comboBoxFactory.SetValue(ComboBox.ForegroundProperty, new SolidColorBrush(Colors.Black));
            comboBoxFactory.SetValue(ComboBox.BackgroundProperty, new SolidColorBrush(Colors.Transparent));
            comboBoxFactory.SetValue(ComboBox.SelectedIndexProperty, 0);
            comboBoxFactory.SetBinding(ComboBox.ItemsSourceProperty, new Binding()
            {
                Path = new PropertyPath(zComboAttribute.ItemsSourceProperty),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            comboBoxFactory.SetBinding(ComboBox.SelectedItemProperty, new Binding()
            {
                Path = new PropertyPath(propertyInfo.Name),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            if (FindResource("InfoComboBox") is Style comboBoxStyle)
            {
                comboBoxFactory.SetValue(ComboBox.StyleProperty, comboBoxStyle);
            }
            FrameworkElementFactory cellFactory = new(typeof(DockPanel));
            cellFactory.AppendChild(comboBoxFactory);
            DataGridTemplateColumn dataGridTemplateColumn = GetBaseColumn(zComboAttribute, propertyInfo, cellFactory);
            return dataGridTemplateColumn;
        }

        public static DataGridTemplateColumn GetMultiCheckColumn(ZFormMultiCheckColumnAttribute zMultiCheckAttribute, PropertyInfo propertyInfo)
        {
            FrameworkElementFactory multiCheckBox = new FrameworkElementFactory(typeof(MultiCheckBox));
            multiCheckBox.SetValue(MultiCheckBox.CheckPropertyNameProperty, zMultiCheckAttribute.MemberPath);
            multiCheckBox.SetValue(MultiCheckBox.ContentPropertyNameProperty, zMultiCheckAttribute.ContentProperty);
            multiCheckBox.SetBinding(MultiCheckBox.ItemsSourceProperty, new Binding()
            {
                Path = new PropertyPath(propertyInfo.Name),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            FrameworkElementFactory cellFactory = new(typeof(DockPanel));
            cellFactory.AppendChild(multiCheckBox);
            DataGridTemplateColumn dataGridTemplateColumn = GetBaseColumn(zMultiCheckAttribute, propertyInfo, cellFactory);
            return dataGridTemplateColumn;
        }

        public static DataGridTemplateColumn GetMultiCheckReadOnlyColumn(ZFormMultiCheckColumnAttribute zMultiCheckAttribute, PropertyInfo propertyInfo)
        {
            FrameworkElementFactory itemsControlFactory = new(typeof(ItemsControl));
            FrameworkElementFactory itemsPanelFactory = new(typeof(StackPanel));
            itemsPanelFactory.SetValue(StackPanel.OrientationProperty, Orientation.Horizontal);
            ItemsPanelTemplate itemsPanelTemplate = new(itemsPanelFactory);
            itemsControlFactory.SetValue(ItemsControl.ItemsPanelProperty, itemsPanelTemplate);
            itemsControlFactory.SetBinding(ItemsControl.ItemsSourceProperty, new Binding()
            {
                Path = new PropertyPath(propertyInfo.Name),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            FrameworkElementFactory buttonFactory = new(typeof(Button), "buttonContentItem");
            buttonFactory.SetValue(Button.MarginProperty, new Thickness(2, 0, 2, 0));
            buttonFactory.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.Transparent));
            buttonFactory.SetValue(Button.BorderThicknessProperty, new Thickness(0));
            buttonFactory.SetValue(Button.CursorProperty, Cursors.Hand);
            buttonFactory.SetBinding(Button.ContentProperty, new Binding()
            {
                Path = new PropertyPath(zMultiCheckAttribute.ContentProperty),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            buttonFactory.SetBinding(Button.IsEnabledProperty, new Binding()
            {
                Path = new PropertyPath(zMultiCheckAttribute.MemberPath),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            FrameworkElementFactory dockPanelFactory = new(typeof(DockPanel));
            dockPanelFactory.AppendChild(buttonFactory);
            FrameworkElementFactory frameworkElementFactory = new(typeof(Border), "borderContentItem");
            frameworkElementFactory.SetValue(Border.HeightProperty, 24.0);
            frameworkElementFactory.SetValue(Border.MarginProperty, new Thickness(3));
            frameworkElementFactory.SetValue(Border.BackgroundProperty, new SolidColorBrush(Colors.AliceBlue));
            frameworkElementFactory.AppendChild(dockPanelFactory);
            Trigger trigger = new()
            {
                SourceName = "buttonContentItem",
                Property = Button.IsEnabledProperty,
                Value = false
            };
            trigger.Setters.Add(new Setter(Button.VisibilityProperty, Visibility.Collapsed, "borderContentItem"));
            DataTemplate dataTemplateItemsControl = new();
            dataTemplateItemsControl.Triggers.Add(trigger);
            dataTemplateItemsControl.VisualTree = frameworkElementFactory;
            itemsControlFactory.SetValue(ItemsControl.ItemTemplateProperty, dataTemplateItemsControl);
            FrameworkElementFactory cellFactory = new(typeof(DockPanel));
            cellFactory.AppendChild(itemsControlFactory);
            DataGridTemplateColumn dataGridTemplateColumn = new()
            {
                Header = zMultiCheckAttribute.Title,
                Width = new DataGridLength(zMultiCheckAttribute.Width, zMultiCheckAttribute.WidthUnit),
                CellTemplate = new()
                {
                    VisualTree = cellFactory
                }
            };
            return dataGridTemplateColumn;
        }

        public static DataGridTemplateColumn GetDateColumn(ZFormDateColumnAttribute zDateAttribute, PropertyInfo propertyInfo)
        {
            FrameworkElementFactory dateTimePicker = new FrameworkElementFactory(typeof(DateTimePicker));
            dateTimePicker.SetValue(DateTimePicker.PaddingProperty, new Thickness(5, 0, 5, 0));
            dateTimePicker.SetValue(DateTimePicker.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            dateTimePicker.SetValue(DateTimePicker.BorderThicknessProperty, new Thickness(0));
            dateTimePicker.SetValue(DateTimePicker.DisplayFormatProperty, zDateAttribute.DateFormat);
            dateTimePicker.SetValue(DateTimePicker.ForegroundProperty, new SolidColorBrush(Colors.Black));
            dateTimePicker.SetBinding(DateTimePicker.DisplayDateProperty, GetBinding(zDateAttribute, propertyInfo));
            FrameworkElementFactory cellFactory = new(typeof(DockPanel));
            cellFactory.AppendChild(dateTimePicker);
            DataGridTemplateColumn dataGridTemplateColumn = GetBaseColumn(zDateAttribute, propertyInfo, cellFactory);
            return dataGridTemplateColumn;
        }

        public static DataGridTemplateColumn GetBaseColumn(IZFormColumn zFormColumn, PropertyInfo propertyInfo, FrameworkElementFactory cellFactory)
        {
            DataGridTemplateColumn dataGridTemplateColumn = new()
            {
                Header = zFormColumn.Title,
                Width = new DataGridLength(zFormColumn.Width, zFormColumn.WidthUnit),
                CellTemplate = new()
                {
                    VisualTree = cellFactory
                },
                SortMemberPath = GetMemberPath(zFormColumn, propertyInfo)
            };
            return dataGridTemplateColumn;
        }

        public static string GetMemberPath(IZFormColumn zFormItem, PropertyInfo propertyInfo)
        {
            if (string.IsNullOrEmpty(zFormItem.MemberPath))
            {
                return propertyInfo.Name;
            }
            return $"{propertyInfo.Name}.{zFormItem.MemberPath}";
        }

        public static Binding GetBinding(IZFormColumn zFormColumn, PropertyInfo propertyInfo)
        {
            string path = propertyInfo.Name;
            if (!string.IsNullOrEmpty(zFormColumn.MemberPath))
            {
                path = $"{path}.{zFormColumn.MemberPath}";
            }
            return new Binding()
            {
                Path = new PropertyPath(path),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
        }

        public static object? FindResource(string key)
        {
            ResourceDictionary resourceDict = new ResourceDictionary();
            resourceDict.Source = new Uri("/Zhy.Components.Wpf;component/Views/Theme/AppDictionary.xaml", UriKind.Relative);
            bool has = resourceDict.Contains(key);
            if (has)
            {
                object res = resourceDict[key];
                return res;
            }
            return null;
        }
    }
}