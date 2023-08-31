/****************************************
 * FileName:	TestItem
 * Creater: 	shaozhy
 * Create Date:	2023/8/23 15:47:11
 * Version: 	v0.0.1
 * Description:	
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Collections.ObjectModel;
using System.Collections;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using Zhy.Components._Enum;
using CommunityToolkit.Mvvm.Input;
using Zhy.Components._Attribute._Column;
using Zhy.Components._Attribute;
using Zhy.Components._View._Window;

namespace Zhy.Demo._Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TestItem : ObservableObject
    {
        private bool _isChecked;
        private string _onlyReadText;
        private string _text;
        private TestSearchMemberItem _member;
        private List<TestSearchMemberItem> _members = new List<TestSearchMemberItem>
        {
            new TestSearchMemberItem("小亚",1),
            new TestSearchMemberItem("小苹",2),
            new TestSearchMemberItem("小米",3),
            new TestSearchMemberItem("小腾",4),
            new TestSearchMemberItem("小易",5),
            new TestSearchMemberItem("小奇",6)
        };
        private string _combo;
        private List<string> _comboList = new List<string>
        {
            "选项1", "选项2", "选项3", "选项4", "选项5"
        };
        private TestSearchMemberItem _selectMember;
        private string _selectText;
        private List<TestMultiCheckItem> _multiCheckItems = new List<TestMultiCheckItem>()
        {
            new TestMultiCheckItem(true, "选项1"),
            new TestMultiCheckItem(true, "选项2"),
            new TestMultiCheckItem(true, "选项3"),
            new TestMultiCheckItem(false , "选项4"),
            new TestMultiCheckItem(true, "选项5"),
            new TestMultiCheckItem(false , "选项6")
        };


        [ZFormCheckColumn("选 择", Width = 30, Index = 0,IsHideFormItem =true,
            WidthUnit = DataGridLengthUnitType.Auto)]
        public bool IsChecked
        {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value); }
        }

        [ZFormTextColumn( "只读文本",Index=3, IsReadOnlyItem = true,
            Width = 1, WidthUnit = DataGridLengthUnitType.Auto)]
        public string OnlyReadText
        {
            get { return _onlyReadText; }
            set{ SetProperty(ref _onlyReadText, value); }
        }

        [ZFormTextColumn( "可编辑文本", Index= 3, 
            Width = 1, WidthUnit = DataGridLengthUnitType.Auto)]
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }
        [ZFormComboColumn( "成员", ItemsSourceProperty = nameof(Members),
            MemberPath = "Name", IsSearchProperty = true,
            WidthUnit = DataGridLengthUnitType.Auto)]
        public TestSearchMemberItem Member
        {
            get { return _member; }
            set { SetProperty(ref _member, value); }
        }
        public List<TestSearchMemberItem> Members
        {
            get { return _members; }
            set { SetProperty(ref _members, value); }
        }

        [ZFormComboColumn( "选 项", Index = 2, ItemsSourceProperty = nameof(ComboList),
            IsReadOnlyColumn =true, Width = 1, WidthUnit = DataGridLengthUnitType.Auto)]
        public string Combo
        {
            get { return _combo; }
            set { SetProperty(ref _combo, value); }
        }
        public List<string> ComboList
        {
            get { return _comboList; }
            set { SetProperty(ref _comboList, value); }
        }

        [ZFormTextButtonColumn( "路 径", ButtonContent = "选 择", ButtonStyle = ZFormButtonStyle.SuccessButton,
            IsSearchProperty = true, IsReadOnlyItem = true, RelayCommandName = nameof(CommandOper))]
        public string SelectText
        {
            get { return _selectText; }
            set { SetProperty(ref _selectText, value); }
        }
        [ZFormTextButtonColumn( "朋 友", ButtonContent = "选 择", MemberPath = "Name", IsSearchProperty = true,
            ButtonStyle = ZFormButtonStyle.InfoButton, IsReadOnlyColumn = true, RelayCommandName = nameof(CommandOper6))]
        public TestSearchMemberItem SelectMember
        {
            get { return _selectMember; }
            set { SetProperty(ref _selectMember, value); }
        }

        [ZFormMultiCheckItem( "多选项", "Checked", "Content")]
        public List<TestMultiCheckItem> MultiCheckItems
        {
            get { return _multiCheckItems; }
            set { SetProperty(ref _multiCheckItems, value); }
        }

        public RelayCommand<TestItem> CommandOper => new RelayCommand<TestItem>(Oper);

        [ZFormFuncButton(ButtonContent = "删 除",Index=1, ButtonStyle = ZFormButtonStyle.ErrorButton)]
        public RelayCommand<object[]> CommandOper2 => new RelayCommand<object[]>(Oper2);
        [ZFormFuncButton(ButtonContent = "查 看",Index =0, ButtonStyle = ZFormButtonStyle.WarnButton)]
        public RelayCommand<object[]> CommandOper3 => new RelayCommand<object[]>(Oper3);
        public RelayCommand<TestItem> CommandOper6 => new RelayCommand<TestItem>(Oper6);


        public void Oper(TestItem testItem)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? dr = fileDialog.ShowDialog();
            if (!(bool)dr)
                return;
            testItem.SelectText = fileDialog.FileName;
        }
        public void Oper2(object[] paras)
        {
            IList testItems = paras[1] as IList;
            TestItem testItem = paras[0] as TestItem;
            testItems.Remove(testItem);
        }
        public void Oper3(object[] paras)
        {
            TestItem testItem = paras[0] as TestItem;
            string msg = null;
            PropertyInfo[] propertyInfos = testItem.GetType().GetProperties();
            foreach (var propertyInfo in propertyInfos)
            {
                ZFormItemAttribute? zFormItemAttribute = propertyInfo.GetCustomAttribute<ZFormItemAttribute>();
                if (zFormItemAttribute == null)
                    continue;
                if (zFormItemAttribute is ZFormMultiCheckItemAttribute)
                {
                    ZFormMultiCheckItemAttribute zFormMultiCheckItem = (ZFormMultiCheckItemAttribute)zFormItemAttribute;
                    PropertyInfo? propertyInfo1 = testItem.GetType().GetProperty(propertyInfo.Name);
                    object? val = propertyInfo1.GetValue(testItem);
                    IList list = val as IList;
                    msg += zFormItemAttribute.Title + ": ";
                    foreach (var item in list)
                    {
                        PropertyInfo? propertyInfo2 = item.GetType().GetProperty(zFormMultiCheckItem.MemberPath);
                        PropertyInfo? propertyInfo3 = item.GetType().GetProperty(zFormMultiCheckItem.ContentProperty);
                        object? vval = propertyInfo2.GetValue(item);
                        bool check = (bool)vval;
                        if(check)
                            msg += propertyInfo3.GetValue(item) + " ";
                    }
                }
                else if (zFormItemAttribute is ZFormTextButtonColumnAttribute || zFormItemAttribute is ZFormComboColumnAttribute)
                {
                    if (string.IsNullOrEmpty(zFormItemAttribute.MemberPath))
                        msg += zFormItemAttribute.Title + ": " + propertyInfo.GetValue(testItem) + "\r\n";
                    else
                    {
                        object? val = propertyInfo.GetValue(testItem);
                        if (val == null) continue;
                        PropertyInfo? propertyInfo2 = val.GetType().GetProperty(zFormItemAttribute.MemberPath);
                        msg += zFormItemAttribute.Title + ": " + propertyInfo2.GetValue(val) + "\r\n";
                    }
                }
                else
                {
                    msg += zFormItemAttribute.Title + ": " + propertyInfo.GetValue(testItem) + "\r\n";
                }
            }
            MessageBox.Show(msg, "属性信息", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        [ZFormFuncButton(ButtonContent = "添 加",Index=1, ButtonStyle = ZFormButtonStyle.InfoButton)]
        public static void Oper4(IEnumerable items)
        {
            IList<TestItem> testItems = items as IList<TestItem>;
            TestItem testItem = new TestItem();
            testItem.Text = "测试";
            testItem.IsChecked = true;
            ZFormDialog zFormDialog = new ZFormDialog(testItem, bool (item) =>
            {
                TestItem testItem = (TestItem)item;
                if (string.IsNullOrEmpty(testItem.SelectText))
                {
                    MessageBox.Show("选择项不可为空！");
                    return false;
                }
                return true;
            });
            bool dr = (bool)zFormDialog.ShowDialog();
            if (dr)
                testItems.Add(testItem);
            //IList<TestItem> testItems = items as IList<TestItem>;
            //TestItem testItem = new TestItem() { IsChecked = true, Text = "测试文本1", ComboList = new List<string>() { "选项1", "选项2" }, Combo = "选项1", SelectText = "asd" };
            //testItems.Add(testItem);
        }

        [ZFormFuncButton(ButtonContent = "批量删除",Index=0, ButtonStyle = ZFormButtonStyle.ErrorButton)]
        public static void Oper5(IEnumerable items)
        {
            IList<TestItem> testItems = items as IList<TestItem>;
            List<TestItem> rmItems = new List<TestItem>();
            foreach (var item in testItems)
                if(item.IsChecked)
                    rmItems.Add(item);
            foreach (var item in rmItems)
                testItems.Remove(item);
        }
        public void Oper6(TestItem objects)
        {
            string[] strings = { "小亚", "小苹", "小米", "小腾", "小易", "小奇" };
            int v = new Random().Next(0, strings.Length);
            SelectMember = new TestSearchMemberItem(strings[v], v);
        }
    }

    public class TestSearchMemberItem
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public TestSearchMemberItem(string name, int age)
        {
            Name = name;
            Age = age;
        }
    }

    public class TestMultiCheckItem
    {
        public bool Checked { get; set; }
        public string Content { get; set; }
        public TestMultiCheckItem(bool check, string content)
        {
            Checked = check;
            Content = content;
        }
    }
}
