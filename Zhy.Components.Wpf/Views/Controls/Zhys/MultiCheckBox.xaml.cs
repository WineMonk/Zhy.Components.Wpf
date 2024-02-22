using System;
using System.Collections;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Zhy.Components.Wpf.Views.Controls.Zhys
{
    /// <summary>
    /// MultiCheckBox.xaml 的交互逻辑
    /// </summary>
    public partial class MultiCheckBox : UserControl
    {
        /// <summary>
        /// 多项选择下拉框
        /// </summary>
        public MultiCheckBox()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 数据项源
        /// </summary>
        public IList ItemsSource
        {
            get { return (IList)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }
        /// <summary>
        /// 数据项源依赖性
        /// </summary>
        public static readonly DependencyProperty ItemsSourceProperty =
            DependencyProperty.Register("ItemsSource", typeof(IList), typeof(MultiCheckBox), new PropertyMetadata(null, OnItemsSourceChanged));
        /// <summary>
        /// 选择属性名称
        /// </summary>
        public string CheckPropertyName
        {
            get { return (string)GetValue(CheckPropertyNameProperty); }
            set { SetValue(CheckPropertyNameProperty, value); }
        }
        /// <summary>
        /// 选择属性依赖项
        /// </summary>
        public static readonly DependencyProperty CheckPropertyNameProperty =
            DependencyProperty.Register("CheckPropertyName", typeof(string), typeof(MultiCheckBox), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnContentOrCheckPropertyNameChanged)));
        /// <summary>
        /// 内容属性名称
        /// </summary>
        public string ContentPropertyName
        {
            get { return (string)GetValue(ContentPropertyNameProperty); }
            set { SetValue(ContentPropertyNameProperty, value); }
        }
        /// <summary>
        /// 内容属性依赖项
        /// </summary>
        public static readonly DependencyProperty ContentPropertyNameProperty =
            DependencyProperty.Register("ContentPropertyName", typeof(string), typeof(MultiCheckBox), new PropertyMetadata(string.Empty, new PropertyChangedCallback(OnContentOrCheckPropertyNameChanged)));
        private static void OnContentOrCheckPropertyNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiCheckBox multiCheckBox = (MultiCheckBox)d;
            object v = multiCheckBox.FindName("itemsControlChecked");
            if (v == null)
                return;
            ItemsControl itemsControl = (ItemsControl)v;
            itemsControl.ItemTemplate = multiCheckBox.GetCheckedItemsDataTemplate();


            object v1 = multiCheckBox.FindName("listBoxTotalItems");
            if (v1 == null)
                return;
            ListBox listBox = (ListBox)v1;
            listBox.ItemContainerStyle = multiCheckBox.GetTotalItemContainerStyle();
            listBox.ItemTemplate = multiCheckBox.GetTotalItemsDataTemplate();
        }

        internal IList CheckedItemsSource
        {
            get { return (IList)GetValue(CheckedItemsSourceProperty); }
            set { SetValue(CheckedItemsSourceProperty, value); }
        }

        internal static readonly DependencyProperty CheckedItemsSourceProperty =
            DependencyProperty.Register("CheckedItemsSource", typeof(IList), typeof(MultiCheckBox), new PropertyMetadata(null));

        private void InitializeView()
        {
            if (ItemsSource == null || ItemsSource.Count < 1)
                return;
            Type type = this.ItemsSource.GetType();
            CheckedItemsSource = Activator.CreateInstance(type) as IList;
            foreach (object item in ItemsSource)
            {
                PropertyInfo propertyInfo = item.GetType().GetProperty(CheckPropertyName);
                if (propertyInfo == null)
                    continue;
                object val = propertyInfo.GetValue(item);
                if (val == null)
                    continue;
                bool check = false;
                bool.TryParse(val.ToString(), out check);
                if (check)
                    CheckedItemsSource.Add(item);
            }
            itemsControlChecked.ItemsSource = CheckedItemsSource;
            listBoxTotalItems.ItemsSource = ItemsSource;
        }

        private static void OnItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            MultiCheckBox multiCheckBox = (MultiCheckBox)d;
            multiCheckBox.InitializeView();
        }

        private void toggleButton_LostFocus(object sender, RoutedEventArgs e)
        {
            if (popup.IsMouseOver)
                return;
            toggleButton.IsChecked = false;
        }

        private Style GetTotalItemContainerStyle()
        {
            Style style = new Style(typeof(ListBoxItem));
            style.Setters.Add(new Setter(ListBoxItem.MarginProperty, new Thickness(1)));
            style.Setters.Add(new Setter(ListBoxItem.IsSelectedProperty, new Binding()
            {
                Path = new PropertyPath(multiCheckBox.CheckPropertyName),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            }));
            ControlTemplate controlTemplate = new ControlTemplate(typeof(ListBoxItem));
            FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(Grid));
            FrameworkElementFactory border1 = new FrameworkElementFactory(typeof(Border), "bg");
            border1.SetValue(Border.BorderThicknessProperty, new Thickness(0));
            border1.SetValue(Border.CornerRadiusProperty, new CornerRadius(3));
            frameworkElementFactory.AppendChild(border1);
            FrameworkElementFactory contentPresenter = new FrameworkElementFactory(typeof(ContentPresenter), "content");
            contentPresenter.SetValue(ContentPresenter.MarginProperty, new Thickness(5));
            frameworkElementFactory.AppendChild(contentPresenter);
            FrameworkElementFactory border2 = new FrameworkElementFactory(typeof(Border));
            border2.SetValue(Border.BackgroundProperty, new SolidColorBrush(Colors.White));
            border2.SetValue(Border.CornerRadiusProperty, new CornerRadius(3));
            border2.SetValue(Border.OpacityProperty, 0.0);
            frameworkElementFactory.AppendChild(border2);
            controlTemplate.VisualTree = frameworkElementFactory;
            Trigger trigger = new Trigger();
            trigger.Property = ListBoxItem.IsSelectedProperty;
            trigger.Value = true;
            trigger.Setters.Add(new Setter(Border.BackgroundProperty, new SolidColorBrush(Color.FromRgb(173, 214, 255)), "bg"));
            controlTemplate.Triggers.Add(trigger);
            //trigger = new Trigger();
            //trigger.Property = ListBoxItem.IsMouseOverProperty;
            //trigger.Value = true;
            //trigger.Setters.Add(new Setter(Border.BackgroundProperty, new SolidColorBrush(Color.FromRgb(236, 255, 255)), "bg"));
            //controlTemplate.Triggers.Add(trigger);
            trigger = new Trigger();
            trigger.Property = ListBoxItem.IsEnabledProperty;
            trigger.Value = false;
            trigger.Setters.Add(new Setter(Border.OpacityProperty, 0.3, "bg"));
            trigger.Setters.Add(new Setter(ListBoxItem.ForegroundProperty, new SolidColorBrush(Colors.Gray)));
            controlTemplate.Triggers.Add(trigger);
            MultiTrigger multiTrigger = new MultiTrigger();
            multiTrigger.Conditions.Add(new Condition(ListBoxItem.IsMouseOverProperty, true));
            multiTrigger.Conditions.Add(new Condition(ListBoxItem.IsSelectedProperty, false));
            multiTrigger.Setters.Add(new Setter(Border.BackgroundProperty, new SolidColorBrush(Color.FromRgb(0, 155, 219)), "bg"));
            multiTrigger.Setters.Add(new Setter(Border.OpacityProperty, 0.7, "bg"));
            multiTrigger.Setters.Add(new Setter(ListBoxItem.ForegroundProperty, new SolidColorBrush(Colors.White)));
            controlTemplate.Triggers.Add(multiTrigger);
            style.Setters.Add(new Setter(ListBoxItem.TemplateProperty, controlTemplate));
            return style;
        }

        private DataTemplate GetCheckedItemsDataTemplate()
        {
            DataTemplate dataTemplate = new DataTemplate();
            FrameworkElementFactory frameworkElementFactory = new FrameworkElementFactory(typeof(Border));
            frameworkElementFactory.SetValue(Border.HeightProperty, 24.0);
            frameworkElementFactory.SetValue(Border.MarginProperty, new Thickness(3));
            frameworkElementFactory.SetValue(Border.BackgroundProperty, new SolidColorBrush(Colors.AliceBlue));

            FrameworkElementFactory dockPanel = new FrameworkElementFactory(typeof(DockPanel));
            FrameworkElementFactory button1 = new FrameworkElementFactory(typeof(Button));
            button1.SetValue(Button.PaddingProperty, new Thickness(2, 0, 4, 0));
            button1.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.Transparent));
            button1.SetValue(Button.BorderThicknessProperty, new Thickness(0));

            FrameworkElementFactory image = new FrameworkElementFactory(typeof(Image));
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("pack://application:,,,/Zhy.Components.Wpf;component\\Resources\\Images\\remove.png");
            bitmapImage.EndInit();
            image.SetValue(Image.SourceProperty, bitmapImage);
            image.SetValue(Image.StretchProperty, Stretch.Fill);
            image.SetValue(Image.VerticalAlignmentProperty, VerticalAlignment.Center);
            image.SetValue(Image.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            image.SetValue(Image.HeightProperty, 9.0);
            image.SetValue(Image.WidthProperty, 9.0);
            button1.AppendChild(image);
            //button1.SetValue(Button.ContentProperty, "X");
            button1.SetValue(Button.CursorProperty, Cursors.Hand);
            button1.SetValue(DockPanel.DockProperty, Dock.Right);
            button1.AddHandler(Button.ClickEvent, new RoutedEventHandler(OnCheckedItemRemoveClick));
            dockPanel.AppendChild(button1);
            FrameworkElementFactory button2 = new FrameworkElementFactory(typeof(Button));
            button2.SetValue(Button.MarginProperty, new Thickness(2, 0, 2, 0));
            button2.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.Transparent));
            button2.SetValue(Button.BorderThicknessProperty, new Thickness(0));
            button2.SetValue(Button.CursorProperty, Cursors.Hand);
            button2.SetBinding(Button.ContentProperty, new Binding()
            {
                Path = new PropertyPath(multiCheckBox.ContentPropertyName),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            dockPanel.AppendChild(button2);
            frameworkElementFactory.AppendChild(dockPanel);
            dataTemplate.VisualTree = frameworkElementFactory;
            return dataTemplate;
        }

        private DataTemplate GetTotalItemsDataTemplate()
        {
            DataTemplate dataTemplate = new DataTemplate();
            FrameworkElementFactory factory = new FrameworkElementFactory(typeof(DockPanel));
            FrameworkElementFactory checkBox = new FrameworkElementFactory(typeof(CheckBox), "checkBox");
            checkBox.SetBinding(CheckBox.IsCheckedProperty, new Binding()
            {
                Path = new PropertyPath(multiCheckBox.CheckPropertyName),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            checkBox.AddHandler(CheckBox.CheckedEvent, new RoutedEventHandler(OnTotalItemChecked));
            checkBox.AddHandler(CheckBox.UncheckedEvent, new RoutedEventHandler(OnTotalItemUnchecked));
            checkBox.SetValue(CheckBox.VerticalContentAlignmentProperty, VerticalAlignment.Center);
            checkBox.SetBinding(CheckBox.ContentProperty, new Binding()
            {
                Path = new PropertyPath(multiCheckBox.ContentPropertyName),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            });
            factory.AppendChild(checkBox);
            dataTemplate.VisualTree = factory;
            return dataTemplate;
        }

        private void OnTotalItemChecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (!CheckedItemsSource.Contains(checkBox.DataContext))
            {
                int idx = CheckedItemsSource.Count;
                int totalIdx = ItemsSource.IndexOf(checkBox.DataContext);
                for (int i = 0; i < CheckedItemsSource.Count; i++)
                {
                    var item = CheckedItemsSource[i];
                    int totalIdxCur = ItemsSource.IndexOf(item);
                    if (totalIdx < totalIdxCur)
                    {
                        idx = i;
                        break;
                    }
                }
                PropertyInfo propertyInfo = checkBox.DataContext.GetType().GetProperty(CheckPropertyName);
                if (propertyInfo == null)
                    return;
                propertyInfo.SetValue(checkBox.DataContext, true);
                CheckedItemsSource.Insert(idx, checkBox.DataContext);
                itemsControlChecked.ItemsSource = null;
                itemsControlChecked.ItemsSource = CheckedItemsSource;
            }
        }
        private void OnTotalItemUnchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            if (CheckedItemsSource.Contains(checkBox.DataContext))
            {
                PropertyInfo propertyInfo = checkBox.DataContext.GetType().GetProperty(CheckPropertyName);
                if (propertyInfo == null)
                    return;
                propertyInfo.SetValue(checkBox.DataContext, false);
                CheckedItemsSource.Remove(checkBox.DataContext);
                itemsControlChecked.ItemsSource = null;
                itemsControlChecked.ItemsSource = CheckedItemsSource;
            }
        }

        private void OnCheckedItemRemoveClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (CheckedItemsSource.Contains(button.DataContext))
            {
                PropertyInfo propertyInfo = button.DataContext.GetType().GetProperty(CheckPropertyName);
                if (propertyInfo == null)
                    return;
                propertyInfo.SetValue(button.DataContext, false);
                CheckedItemsSource.Remove(button.DataContext);
                itemsControlChecked.ItemsSource = null;
                itemsControlChecked.ItemsSource = CheckedItemsSource;
            }
        }
    }
}
