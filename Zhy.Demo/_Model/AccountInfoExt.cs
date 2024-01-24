using Zhy.Components.Wpf.Attributes.ZFormColumns;

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
