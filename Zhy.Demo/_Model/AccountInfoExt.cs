/****************************************
 * FileName:	AccountInfoExt
 * Creater: 	shaozhy
 * Create Date:	2023/11/29 10:14:05
 * Version: 	v0.0.1
 * ======================================
 * Modify: —— Version: —— Date: —— Modifier: —— Content:
 ****************************************/
using Zhy.Components.Wpf._Attribute._ZFormColumn;

namespace Zhy.Demo._Model
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountInfoExt : AccountInfo
    {
        [ZFormTextColumn("修改姓名", IsHideFormColumn = true)]
        public override string Username
        {
            get => base.Username;
            set => base.Username = value;
        }
    }
}
