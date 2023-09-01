/****************************************
 * FileName:	ZDataGridException
 * Creater: 	shaozhy
 * Create Date:	2023/8/25 11:23:09
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhy.Components._Exception
{
    /// <summary>
    /// ZDataGrid控件错误
    /// </summary>
    [Serializable]
    public class ZDataGridException : Exception
    {
        /// <summary>
        /// ZDataGrid控件错误
        /// </summary>
        public ZDataGridException():base() { }
        /// <summary>
        /// ZDataGrid控件错误
        /// </summary>
        /// <param name="message">错误信息</param>
        public ZDataGridException(string  message):base(message) { }
        /// <summary>
        /// ZDataGrid控件错误
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="inner">内部异常</param>
        public ZDataGridException(string message, Exception inner):base(message, inner) { }
    }
}
