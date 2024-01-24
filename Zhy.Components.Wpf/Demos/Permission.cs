using CommunityToolkit.Mvvm.ComponentModel;

namespace Zhy.Components.Wpf.Demos
{
    /// <summary>
    /// 权限类
    /// </summary>
    public class Permission : ObservableObject
    {
        private bool _isChecked = false;
        /// <summary>
        /// 选择
        /// </summary>
        public bool IsChecked
        {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value); }
        }
        /// <summary>
        /// 权限名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 说明
        /// </summary>
        public string Desc { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        /// <param name="isChecked">选择</param>
        /// <param name="name">权限名</param>
        /// <param name="desc">说明</param>
        public Permission(bool isChecked, string name, string desc)
        {
            IsChecked = isChecked;
            Name = name;
            Desc = desc;
        }
    }
}
