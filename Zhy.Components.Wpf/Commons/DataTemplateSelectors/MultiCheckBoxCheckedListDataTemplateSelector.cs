using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Zhy.Components.Wpf.Commons.DataTemplateSelectors
{
    internal class MultiCheckBoxCheckedListDataTemplateSelector : System.Windows.Controls.DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            //FrameworkElement frameworkElement = (FrameworkElement)container;
            //MultiCheckBox multiCheckBox = null;
            //while(frameworkElement.Parent != null && !(frameworkElement.Parent is MultiCheckBox))
            //{
            //    frameworkElement = (FrameworkElement)frameworkElement.Parent;
            //}
            //if(frameworkElement.Parent == null)
            //return base.SelectTemplate(item, container);
            //multiCheckBox = (MultiCheckBox)frameworkElement.Parent;
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
            button1.SetValue(Button.ContentProperty, "X");
            button1.SetValue(Button.CursorProperty, Cursors.Hand);
            button1.SetValue(DockPanel.DockProperty, Dock.Right);
            dockPanel.AppendChild(button1);
            //FrameworkElementFactory button2 = new FrameworkElementFactory(typeof(Button));
            //button2.SetValue(Button.MarginProperty, new Thickness(2, 0, 2, 0));
            //button2.SetValue(Button.BackgroundProperty, new SolidColorBrush(Colors.Transparent));
            //button2.SetValue(Button.BorderThicknessProperty, new Thickness(0));
            //button2.SetValue(Button.CursorProperty, Cursors.Hand);
            //button2.SetBinding(Button.ContentProperty, new Binding()
            //{
            //    Path = new PropertyPath(MultiCheckBox.ContentPropertyNameStatic),
            //    Mode = BindingMode.TwoWay,
            //    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            //});
            //dockPanel.AppendChild(button2);
            frameworkElementFactory.AppendChild(dockPanel);
            dataTemplate.VisualTree = frameworkElementFactory;
            return dataTemplate;
            //ControlTemplate controlTemplate = new ControlTemplate();
            //FrameworkElementFactory factory = new FrameworkElementFactory(typeof(ScrollViewer));
            //factory.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
            //factory.SetValue(ScrollViewer.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Auto);
            //Style style = new Style(typeof(ScrollBar));
            //Trigger trigger = new Trigger();
            //trigger.Property = ScrollBar.IsVisibleProperty;
            //trigger.Value = true;
            //trigger.Setters.Add(new Setter(ScrollBar.HeightProperty, 3.0));
            //style.Triggers.Add(trigger);
            //factory.SetResourceReference(ScrollBar.StyleProperty, style);
        }
    }
}
