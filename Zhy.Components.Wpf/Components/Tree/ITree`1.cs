using System.Collections.Generic;

namespace Zhy.Components.Wpf.Components.Tree
{
    /// <summary>
    /// 变化可视树或树节点
    /// </summary>
    /// <typeparam name="TTreeNode">树类型</typeparam>
    public interface ITree<TTreeNode> : ITree where TTreeNode : ITree<TTreeNode>
    {
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
        List<TTreeNode>? Children { get; set; }
        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns>克隆结果实例</returns>
        TTreeNode Clone();
    }
}
