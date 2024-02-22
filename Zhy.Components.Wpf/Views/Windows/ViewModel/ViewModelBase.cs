using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Zhy.Components.Wpf.Views.Windows.ViewModel
{
    /// <summary>
    /// 视图模型
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected bool SetProperty<T>(ref T backingField, T value, Expression<Func<T>> property)
        {
            bool num = !EqualityComparer<T>.Default.Equals(backingField, value);
            if (num)
            {
                backingField = value;
                NotifyPropertyChanged(GetName(property));
            }
            return num;
        }

        /// <summary>
        /// 设置属性值，并在新值与当前值不同时调用NotifyPropertyChanged。
        /// </summary>
        /// <typeparam name="T">属性值类型</typeparam>
        /// <param name="backingField">返回字段</param>
        /// <param name="value">属性值</param>
        /// <param name="name">属性名</param>
        /// <returns>如果属性改变，返回true。</returns>
        protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string name = "")
        {
            bool num = !EqualityComparer<T>.Default.Equals(backingField, value);
            if (num)
            {
                backingField = value;
                NotifyPropertyChanged(name);
            }

            return num;
        }

        /// <summary>
        /// 引发指定属性的PropertyChanged事件。
        /// </summary>
        /// <typeparam name="T">属性值类型</typeparam>
        /// <param name="property">属性</param>
        protected virtual void NotifyPropertyChanged<T>(Expression<Func<T>> property)
        {
            NotifyPropertyChanged(GetName(property));
        }

        /// <summary>
        /// 引发指定属性的PropertyChanged事件。
        /// </summary>
        /// <param name="name">属性名</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            NotifyPropertyChanged(new PropertyChangedEventArgs(name));
        }

        /// <summary>
        /// 引发指定属性的PropertyChanged事件。
        /// </summary>
        /// <param name="args">属性变更参数</param>
        protected virtual void NotifyPropertyChanged(PropertyChangedEventArgs args)
        {
            this.PropertyChanged?.Invoke(this, args);
        }

        private static string GetName<T>(Expression<Func<T>> property)
        {
            MemberExpression memberExpression = ((!(property.Body is UnaryExpression)) ? ((MemberExpression)property.Body) : ((MemberExpression)((UnaryExpression)property.Body).Operand));
            return memberExpression.Member.Name;
        }
    }

}
