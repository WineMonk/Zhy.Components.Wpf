/****************************************
 * FileName:	ZFormItem
 * Creater: 	shaozhy
 * Create Date:	2023/10/17 16:19:49
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Zhy.Components.Wpf._Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ZFormItem: ObservableObject
    {
        private string _key;
        private string _name;
        //private string _dataType;
        private string _value;
        private bool _isReadOnly;
        private Func<ZFormItem, bool> _verifyFunc;
        private string _tip;
        private int _oid;

        public int Oid
        {
            get { return _oid; }
            internal set { SetProperty(ref _oid, value); }
        }
        public string Key
        {
            get { return _key; }
            set { SetProperty(ref _key, value); }
        }
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { SetProperty(ref _isReadOnly, value); }
        }

        internal Func<ZFormItem, bool> VerifyFunc
        {
            get { return _verifyFunc; }
        }
        internal string Tip
        {
            get { return _tip; }
        }

        public void SetVerify(Func<ZFormItem, bool> verifyFunc, string tip)
        {
            _verifyFunc = verifyFunc;
            _tip = tip;
        }
        public bool ShouldSerializeOid()
        {
            return false;
        }
    }
}
