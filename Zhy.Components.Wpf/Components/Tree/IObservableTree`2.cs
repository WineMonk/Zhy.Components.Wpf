using System.Collections.ObjectModel;

namespace Zhy.Components.Wpf.Components.Tree
{
    /// <summary>
    /// 拥有键的变化可视树或树节点
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TTreeNode">树类型</typeparam>
    public interface IObservableTree<TKey, TTreeNode> : ITree where TTreeNode : IObservableTree<TKey, TTreeNode>
    {
        /// <summary>
        /// 键
        /// </summary>
        TKey Key { get; set; }
        /// <summary>
        /// 父节点键
        /// </summary>
        TKey? PKey { get; }
        /// <summary>
        /// 父节点
        /// </summary>
        /// <remarks>
        ///     序列化时需要忽略此属性！
        /// </remarks>
        TTreeNode? Parent { get; set; }
        /// <summary>
        /// 子节点
        /// </summary>
        ObservableCollection<TTreeNode>? Children { get; set; }
        /// <summary>
        /// 子节点索引器
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>对应子节点</returns>
        TTreeNode this[TKey key] { get; set; }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>克隆结果实例</returns>
        TTreeNode Clone();
    }
}
