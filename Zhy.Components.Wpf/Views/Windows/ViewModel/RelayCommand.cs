using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Input;
using System.Windows;

namespace Zhy.Components.Wpf.Views.Windows.ViewModel
{
    /// <summary>
    /// An implementation of ICommand.
    /// </summary>
    public class RelayCommand : ViewModelBase, ICommand
    {
        private readonly Action _execute;

        private readonly Action<object> _executeWithParam;

        private readonly Func<bool> _canExecute;

        private readonly Func<object, bool> _canExecuteWithParamer;

        private readonly Func<Task> _executeTask;

        private readonly PropertyInfo _canExecuteProperty;

        private readonly MethodInfo _executeMethod;

        private readonly object _targetObject;

        /// <summary>
        /// 当CanExecute属性更改时发生。
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 实例化一个新的RelayCommand实例。
        /// </summary>
        /// <param name="execute">执行方法</param>
        /// <exception cref="ArgumentNullException">执行方法为空异常</exception>
        public RelayCommand(Func<Task> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _executeTask = execute;
        }
        /// <summary>
        /// 实例化一个新的RelayCommand实例。
        /// </summary>
        /// <param name="execute">执行方法</param>
        /// <exception cref="ArgumentNullException">执行方法为空异常</exception>
        public RelayCommand(Action execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
        }

        /// <summary>
        /// 实例化一个新的RelayCommand实例。
        /// </summary>
        /// <param name="execute">执行方法</param>
        /// <exception cref="ArgumentNullException">执行方法为空异常</exception>
        public RelayCommand(Action<object> execute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            _executeWithParam = execute;
        }

        /// <summary>
        /// 实例化一个新的RelayCommand实例。
        /// </summary>
        /// <param name="execute">执行方法</param>
        /// <param name="canExecute">是否可执行</param>
        /// <exception cref="ArgumentNullException">执行方法为空异常</exception>
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 实例化一个新的RelayCommand实例。
        /// </summary>
        /// <param name="execute">执行方法</param>
        /// <param name="canExecute">是否可执行</param>
        /// <exception cref="ArgumentNullException">执行方法为空异常</exception>
        public RelayCommand(Action<object> execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _executeWithParam = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 实例化一个新的RelayCommand实例。
        /// </summary>
        /// <param name="execute">执行方法</param>
        /// <param name="canExecute">是否可执行</param>
        /// <exception cref="ArgumentNullException">执行方法为空异常</exception>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _executeWithParam = execute;
            _canExecuteWithParamer = canExecute;
        }

        /// <summary>
        /// 实例化一个新的RelayCommand实例。
        /// </summary>
        /// <param name="execute">执行方法</param>
        /// <param name="canExecute">是否可执行</param>
        /// <exception cref="ArgumentNullException">执行方法为空异常</exception>
        public RelayCommand(Func<Task> execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            _executeTask = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 实例化一个新的RelayCommand实例。
        /// </summary>
        /// <param name="execute">执行方法</param>
        /// <param name="canExecute">是否可执行</param>
        /// <exception cref="ArgumentNullException">执行方法为空异常</exception>
        public RelayCommand(object targetObject, MethodInfo execute, PropertyInfo canExecute)
        {
            _targetObject = targetObject;
            _executeMethod = execute;
            _canExecuteProperty = canExecute;
        }

        /// <summary>
        /// 是否可执行
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>是否可执行</returns>
        public virtual bool CanExecute(object parameter)
        {
            if (_canExecute != null)
            {
                return _canExecute();
            }

            if (_canExecuteWithParamer != null)
            {
                return _canExecuteWithParamer(parameter);
            }

            return true;
        }


        /// <summary>
        /// 可执行状态变化
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameter">参数</param>
        public virtual void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute();
            }
            else if (_executeTask != null)
            {
                _executeTask();
            }
            else if (_executeWithParam != null)
            {
                _executeWithParam(parameter);
            }
            else if (_executeMethod != null)
            {
                _executeMethod.Invoke(_targetObject, new object[1] { parameter });
            }
        }

    }
}
