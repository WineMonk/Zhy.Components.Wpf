using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Zhy.Components.Wpf.Views.Controls.Zhys
{
    /// <summary>
    /// DateTimePicker.xaml 的交互逻辑
    /// </summary>
    public partial class DateTimePicker : UserControl
    {
        /// <summary>
        /// 选择的日期
        /// </summary>
        public DateTime SelectDate
        {
            get { return (DateTime)GetValue(SelectDateProperty); }
            set { SetValue(SelectDateProperty, value); PropertyDateChanged(); }
        }
        /// <summary>
        /// 选择的日期依赖配置
        /// </summary>
        public static readonly DependencyProperty SelectDateProperty =
            DependencyProperty.Register("SelectDate", typeof(DateTime),
                typeof(DateTimePicker), new PropertyMetadata(DateTime.Now, SelectDatePropertyChanged));
        private static void SelectDatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTimePicker dateTimePicker = (DateTimePicker)d;
            dateTimePicker.PropertyDateChanged();
        }

        /// <summary>
        /// 日期文本显示格式
        /// </summary>
        public string DisplayFormat
        {
            get { return (string)GetValue(DisplayFormatProperty); }
            set { SetValue(DisplayFormatProperty, value); }
        }
        /// <summary>
        /// 日期文本显示格式依赖配置
        /// </summary>
        public static readonly DependencyProperty DisplayFormatProperty =
            DependencyProperty.Register("DisplayFormat", typeof(string),
                typeof(DateTimePicker), new PropertyMetadata("yyyy-MM-dd HH:mm:ss"));
        /// <summary>
        /// 日期文本
        /// </summary>
        public string DisplayDate
        {
            get { return (string)GetValue(DisplayDateProperty); }
            set { SetValue(DisplayDateProperty, value); PropertyDisplayDateChanged(); }
        }
        /// <summary>
        /// 日期文本依赖配置
        /// </summary>
        public static readonly DependencyProperty DisplayDateProperty =
            DependencyProperty.Register("DisplayDate", typeof(string),
                typeof(DateTimePicker), new PropertyMetadata(
                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), DisplayDatePropertyChanged));
        private static void DisplayDatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DateTimePicker dateTimePicker = (DateTimePicker)d;
            dateTimePicker.PropertyDisplayDateChanged();
        }

        //private string _dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        //public string DateTimeFormat
        //{
        //    get { return _dateTimeFormat; }
        //    set { _dateTimeFormat = value; }
        //}
        //private DateTime _selectDate = DateTime.Now;
        //public DateTime SelectDate
        //{
        //    get { return _selectDate; }
        //    set { _selectDate = value; PropertyDateChanged(); }
        //}

        /// <summary>
        /// DateTimePicker构造函数
        /// </summary>
        public DateTimePicker()
        {
            InitializeComponent();
            calendar.SelectedDate = SelectDate;
            textBoxHour.Text = SelectDate.Hour.ToString().PadLeft(2, '0');
            textBoxMinute.Text = SelectDate.Minute.ToString().PadLeft(2, '0');
            textBoxSecond.Text = SelectDate.Second.ToString().PadLeft(2, '0');
            textBoxHour.TextChanged += textBoxTime_TextChanged;
            textBoxMinute.TextChanged += textBoxTime_TextChanged;
            textBoxSecond.TextChanged += textBoxTime_TextChanged;
            calendar.SelectedDatesChanged += Calendar_SelectedDatesChanged;
            textBoxDateTime.Text = SelectDate.ToString(DisplayFormat);
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null || e.AddedItems.Count < 1)
            {
                return;
            }
            ViewDateChanged();
        }

        private void toggleButton_LostFocus(object sender, RoutedEventArgs e)
        {
            if (popup.IsMouseOver)
                return;
            toggleButton.IsChecked = false;
        }

        private void textBoxInt_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9) ||
                (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                e.Key == Key.Back || e.Key == Key.Delete ||
                e.Key == Key.Right || e.Key == Key.Left))
            {
                e.Handled = true;
            }
        }

        private void textBoxHour_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int selectionStart = textBox.SelectionStart;
            if (textBox == null)
            {
                return;
            }
            if (textBox.Text.Length >= 2)
            {
                if (selectionStart == 0)
                {
                    string newText = e.Text + textBox.Text[1];
                    if (int.Parse(newText) <= 24)
                    {
                        textBox.Text = newText;
                    }
                }
                else
                {
                    textBox.Text = textBox.Text[0] + e.Text;
                }
                e.Handled = true;
            }
        }

        private void textBoxMinute_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int selectionStart = textBox.SelectionStart;
            if (textBox == null)
            {
                return;
            }
            if (textBox.Text.Length >= 2)
            {
                if (selectionStart == 0)
                {
                    string newText = e.Text + textBox.Text[1];
                    if (int.Parse(newText) <= 60)
                    {
                        textBox.Text = newText;
                    }
                }
                else
                {
                    textBox.Text = textBox.Text[0] + e.Text;
                }
                e.Handled = true;
            }
        }

        private void textBoxSecond_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            int selectionStart = textBox.SelectionStart;
            if (textBox == null)
            {
                return;
            }
            if (textBox.Text.Length >= 2)
            {
                if (selectionStart == 0)
                {
                    string newText = e.Text + textBox.Text[1];
                    if (int.Parse(newText) <= 60)
                    {
                        textBox.Text = newText;
                    }
                }
                else
                {
                    textBox.Text = textBox.Text[0] + e.Text;
                }
                e.Handled = true;
            }
        }


        private void textBoxTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewDateChanged();
        }

        private void ViewDateChanged()
        {
            try
            {
                int year = 0;
                int month = 0;
                int day = 0;
                int hour = 0;
                int minute = 0;
                int second = 0;

                DateTime dateTime = Convert.ToDateTime(calendar.SelectedDate);
                year = dateTime.Year;
                month = dateTime.Month;
                day = dateTime.Day;
                int.TryParse(textBoxHour.Text, out hour);
                int.TryParse(textBoxMinute.Text, out minute);
                int.TryParse(textBoxSecond.Text, out second);

                SelectDate = new DateTime(year, month, day, hour, minute, second);
                textBoxDateTime.Text = SelectDate.ToString(DisplayFormat);
            }
            catch { }
        }

        internal void PropertyDateChanged()
        {
            calendar.SelectedDate = SelectDate;
            textBoxHour.Text = SelectDate.Hour.ToString().PadLeft(2, '0');
            textBoxMinute.Text = SelectDate.Minute.ToString().PadLeft(2, '0');
            textBoxSecond.Text = SelectDate.Second.ToString().PadLeft(2, '0');
        }

        internal void PropertyDisplayDateChanged()
        {
            bool sunccess = DateTime.TryParse(DisplayDate, out DateTime result);
            calendar.SelectedDate = result;
            textBoxHour.Text = result.Hour.ToString().PadLeft(2, '0');
            textBoxMinute.Text = result.Minute.ToString().PadLeft(2, '0');
            textBoxSecond.Text = result.Second.ToString().PadLeft(2, '0');
            SelectDate = result;
        }

        private void buttonNow_Click(object sender, RoutedEventArgs e)
        {
            //SelectDate = DateTime.Now;
            DisplayDate = DateTime.Now.ToString(DisplayFormat);
            PropertyDisplayDateChanged();
        }
    }
}
