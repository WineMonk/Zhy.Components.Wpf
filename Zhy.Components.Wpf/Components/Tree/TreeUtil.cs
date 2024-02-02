using System.Text.Json;

namespace Zhy.Components.Wpf.Components.Tree
{
    /// <summary>
    /// 树工具类
    /// </summary>
    public static class TreeUtil
    {
        /// <summary>
        /// 自Json字符串加载实例
        /// </summary>
        /// <typeparam name="TTree">树类型</typeparam>
        /// <param name="jsonText">Json文本</param>
        /// <returns>树实例</returns>
        public static TTree? JsonToTree<TTree>(string jsonText) where TTree : class, ITree<TTree>
        {
            TTree? tree = JsonSerializer.Deserialize<TTree>(jsonText);
            if (tree == null)
            {
                return default;
            }
            tree.Traversal(t =>
            {
                if (t == null || t.Children == null)
                {
                    return;
                }
                foreach (var child in t.Children)
                {
                    child.Parent = t;
                }
            });
            return tree;
        }
        /// <summary>
        /// 自Json字符串加载实例
        /// </summary>
        /// <typeparam name="TTree">树类型</typeparam>
        /// <param name="jsonText">Json文本</param>
        /// <returns>树实例</returns>
        public static TTree? JsonToObservableTree<TTree>(string jsonText) where TTree : class, IObservableTree<TTree>
        {
            TTree? tree = JsonSerializer.Deserialize<TTree>(jsonText);
            if (tree == null)
            {
                return default;
            }
            tree.Traversal(t =>
            {
                if (t == null || t.Children == null)
                {
                    return;
                }
                foreach (var child in t.Children)
                {
                    child.Parent = t;
                }
            });
            return tree;
        }
        /// <summary>
        /// 自Json字符串加载实例
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TTree">树类型</typeparam>
        /// <param name="jsonText">Json文本</param>
        /// <returns>树实例</returns>
        public static TTree? JsonToTree<TKey, TTree>(string jsonText) where TTree : class, ITree<TKey, TTree>
        {
            TTree? tree = JsonSerializer.Deserialize<TTree>(jsonText);
            if (tree == null)
            {
                return default;
            }
            tree.Traversal(t =>
            {
                if (t == null || t.Children == null)
                {
                    return;
                }
                foreach (var child in t.Children)
                {
                    child.Parent = t;
                }
            });
            return tree;
        }
        /// <summary>
        /// 自Json字符串加载实例
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TTree">树类型</typeparam>
        /// <param name="jsonText">Json文本</param>
        /// <returns>树实例</returns>
        public static TTree? JsonToObservableTree<TKey, TTree>(string jsonText) where TTree : class, IObservableTree<TKey, TTree>
        {
            TTree? tree = JsonSerializer.Deserialize<TTree>(jsonText);
            if (tree == null)
            {
                return default;
            }
            tree.Traversal(t =>
            {
                if (t == null || t.Children == null)
                {
                    return;
                }
                foreach (var child in t.Children)
                {
                    child.Parent = t;
                }
            });
            return tree;
        }
    }
}
