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

namespace Zhy.Components.Wpf._Model
{
    /// <summary>
    /// 
    /// </summary>
    public class ZFormItem : ObservableObject
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int Oid
        {
            get { return _oid; }
            internal set { SetProperty(ref _oid, value); }
        }
        /// <summary>
        /// 表单项键
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { SetProperty(ref _key, value); }
        }
        /// <summary>
        /// 表单项名称
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        /// <summary>
        /// 表单项值
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }
        /// <summary>
        /// 是否只读
        /// </summary>
        public bool IsReadOnly
        {
            get { return _isReadOnly; }
            set { SetProperty(ref _isReadOnly, value); }
        }
        /// <summary>
        /// 设置表单项验证函数
        /// </summary>
        /// <param name="verifyFunc">验证函数</param>
        /// <param name="tip">提示信息</param>
        public void SetVerify(Func<ZFormItem, bool> verifyFunc, string tip)
        {
            _verifyFunc = verifyFunc;
            _tip = tip;
        }
        /// <summary>
        /// 序列化时是否忽略序号
        /// </summary>
        /// <returns>忽略 -> true，否则 -> false。</returns>
        public virtual bool ShouldSerializeOid()
        {
            return false;
        }

        private string _key;
        private string _name;
        private string _value;
        private bool _isReadOnly;
        private Func<ZFormItem, bool> _verifyFunc;
        private string _tip;
        private int _oid;

        internal Func<ZFormItem, bool> VerifyFunc
        {
            get { return _verifyFunc; }
        }
        internal string Tip
        {
            get { return _tip; }
        }
    }
}
