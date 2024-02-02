using System;
using System.Collections.Generic;
using System.Linq;

namespace Zhy.Components.Wpf.Components.Tree
{
    /// <summary>
    /// 树扩展方法
    /// </summary>
    public static class TreeExtension
    {
        /// <summary>
        /// 搜索树
        /// </summary>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">搜索验证表达式</param>
        /// <param name="isClone">搜索结果是否以新生成的拷贝对象返回</param>
        /// <returns>搜索结果树</returns>
        public static TTreeNode? Search<TTreeNode>(this ITree<TTreeNode> tree, Func<TTreeNode?, bool> expression, bool isClone = false) where TTreeNode : class, ITree<TTreeNode>
        {
            if (tree == null)
            {
                return tree as TTreeNode;
            }
            if (isClone)
            {
                TTreeNode treeClone = tree.Clone();
                SearchRec(treeClone, expression);
                return treeClone;
            }
            SearchRec(tree, expression);
            return tree as TTreeNode;
        }
        private static bool SearchRec<TTreeNode>(ITree<TTreeNode> tree, Func<TTreeNode?, bool> expression) where TTreeNode : class, ITree<TTreeNode>
        {
            if (tree == null)
            {
                return false;
            }
            if (tree.Children?.Count > 0)
            {
                List<TTreeNode> rms = new List<TTreeNode>();
                foreach (var child in tree.Children)
                {
                    bool v = SearchRec(child, expression);
                    if (!v)
                    {
                        rms.Add(child);
                    }
                }
                foreach (var rm in rms)
                {
                    tree.Children.Remove(rm);
                }
            }
            if (tree.Children?.Count > 0 || expression(tree as TTreeNode))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 搜索树
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">搜索验证表达式</param>
        /// <param name="isClone">搜索结果是否以新生成的拷贝对象返回</param>
        /// <returns>搜索结果树</returns>
        public static TTreeNode? Search<TKey, TTreeNode>(this ITree<TKey, TTreeNode> tree, Func<TTreeNode?, bool> expression, bool isClone = false) where TTreeNode : class, ITree<TKey, TTreeNode>
        {
            if (tree == null)
            {
                return tree as TTreeNode;
            }
            if (isClone)
            {
                TTreeNode treeClone = tree.Clone();
                SearchRec(treeClone, expression);
                return treeClone;
            }
            SearchRec(tree, expression);
            return tree as TTreeNode;
        }
        private static bool SearchRec<TKey, TTreeNode>(ITree<TKey, TTreeNode> tree, Func<TTreeNode?, bool> expression) where TTreeNode : class, ITree<TKey, TTreeNode>
        {
            if (tree == null)
            {
                return false;
            }
            if (tree.Children?.Count > 0)
            {
                List<TTreeNode> rms = new List<TTreeNode>();
                foreach (var child in tree.Children)
                {
                    bool v = SearchRec(child, expression);
                    if (!v)
                    {
                        rms.Add(child);
                    }
                }
                foreach (var rm in rms)
                {
                    tree.Children.Remove(rm);
                }
            }
            if (tree.Children?.Count > 0 || expression(tree as TTreeNode))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 遍历树
        /// </summary>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">遍历表达式</param>
        /// <remarks>
        ///     遍历时不允许对子节点进行改动，若需改动请使用“SafeTraversal”方法！
        /// </remarks>
        public static void Traversal<TTreeNode>(this ITree<TTreeNode> tree, Action<TTreeNode?>? expression) where TTreeNode : class, ITree<TTreeNode>
        {
            expression?.Invoke(tree as TTreeNode);
            if (tree.Children == null)
            {
                return;
            }
            foreach (var child in tree.Children)
            {
                Traversal(child, expression);
            }
        }
        /// <summary>
        /// 遍历树
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">遍历表达式</param>
        /// <remarks>
        ///     遍历时不允许对子节点进行改动，若需改动请使用“SafeTraversal”方法！
        /// </remarks>
        public static void Traversal<TKey, TTreeNode>(this ITree<TKey, TTreeNode> tree, Action<TTreeNode?>? expression) where TTreeNode : class, ITree<TKey, TTreeNode>
        {
            expression?.Invoke(tree as TTreeNode);
            if (tree.Children == null)
            {
                return;
            }
            foreach (var child in tree.Children)
            {
                Traversal(child, expression);
            }
        }
        /// <summary>
        /// 遍历树
        /// </summary>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">遍历表达式</param>
        public static void SafeTraversal<TTreeNode>(this ITree<TTreeNode> tree, Action<TTreeNode?>? expression) where TTreeNode : class, ITree<TTreeNode>
        {
            expression?.Invoke(tree as TTreeNode);
            List<TTreeNode> traverseds = new List<TTreeNode>();
            TTreeNode? node = default;
            while ((node = GetNode(traverseds, tree.Children)) != null)
            {
                SafeTraversal(node, expression);
                traverseds.Add(node);
            }
        }
        /// <summary>
        /// 遍历树
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">遍历表达式</param>
        public static void SafeTraversal<TKey, TTreeNode>(this ITree<TKey, TTreeNode> tree, Action<TTreeNode?>? expression) where TTreeNode : class, ITree<TKey, TTreeNode>
        {
            expression?.Invoke(tree as TTreeNode);
            List<TTreeNode> traverseds = new List<TTreeNode>();
            TTreeNode? node = default;
            while ((node = GetNode(traverseds, tree.Children)) != null)
            {
                SafeTraversal(node, expression);
                traverseds.Add(node);
            }
        }
        /// <summary>
        /// 搜索树
        /// </summary>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">搜索验证表达式</param>
        /// <param name="isClone">搜索结果是否以新生成的拷贝对象返回</param>
        /// <returns>搜索结果树</returns>
        public static TTreeNode? Search<TTreeNode>(this IObservableTree<TTreeNode> tree, Func<TTreeNode?, bool> expression, bool isClone = false) where TTreeNode : class, IObservableTree<TTreeNode>
        {
            if (tree == null)
            {
                return tree as TTreeNode;
            }
            if (isClone)
            {
                TTreeNode treeClone = tree.Clone();
                SearchRec(treeClone, expression);
                return treeClone;
            }
            SearchRec(tree, expression);
            return tree as TTreeNode;
        }
        private static bool SearchRec<TTreeNode>(IObservableTree<TTreeNode> tree, Func<TTreeNode?, bool> expression) where TTreeNode : class, IObservableTree<TTreeNode>
        {
            if (tree == null)
            {
                return false;
            }
            if (tree.Children?.Count > 0)
            {
                List<TTreeNode> rms = new List<TTreeNode>();
                foreach (var child in tree.Children)
                {
                    bool v = SearchRec(child, expression);
                    if (!v)
                    {
                        rms.Add(child);
                    }
                }
                foreach (var rm in rms)
                {
                    tree.Children.Remove(rm);
                }
            }
            if (tree.Children?.Count > 0 || expression(tree as TTreeNode))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 搜索树
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">搜索验证表达式</param>
        /// <param name="isClone">搜索结果是否以新生成的拷贝对象返回</param>
        /// <returns>搜索结果树</returns>
        public static TTreeNode? Search<TKey, TTreeNode>(this IObservableTree<TKey, TTreeNode> tree, Func<TTreeNode?, bool> expression, bool isClone = false) where TTreeNode : class, IObservableTree<TKey, TTreeNode>
        {
            if (tree == null)
            {
                return tree as TTreeNode;
            }
            if (isClone)
            {
                TTreeNode treeClone = tree.Clone();
                SearchRec(treeClone, expression);
                return treeClone;
            }
            SearchRec(tree, expression);
            return tree as TTreeNode;
        }
        private static bool SearchRec<TKey, TTreeNode>(IObservableTree<TKey, TTreeNode> tree, Func<TTreeNode?, bool> expression) where TTreeNode : class, IObservableTree<TKey, TTreeNode>
        {
            if (tree == null)
            {
                return false;
            }
            if (tree.Children?.Count > 0)
            {
                List<TTreeNode> rms = new List<TTreeNode>();
                foreach (var child in tree.Children)
                {
                    bool v = SearchRec(child, expression);
                    if (!v)
                    {
                        rms.Add(child);
                    }
                }
                foreach (var rm in rms)
                {
                    tree.Children.Remove(rm);
                }
            }
            if (tree.Children?.Count > 0 || expression(tree as TTreeNode))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 遍历树
        /// </summary>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">遍历表达式</param>
        /// <remarks>
        ///     遍历时不允许对子节点进行改动，若需改动请使用“SafeTraversal”方法！
        /// </remarks>
        public static void Traversal<TTreeNode>(this IObservableTree<TTreeNode> tree, Action<TTreeNode?>? expression) where TTreeNode : class, IObservableTree<TTreeNode>
        {
            expression?.Invoke(tree as TTreeNode);
            if (tree.Children == null)
            {
                return;
            }
            foreach (var child in tree.Children)
            {
                Traversal(child, expression);
            }
        }
        /// <summary>
        /// 遍历树
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">遍历表达式</param>
        /// <remarks>
        ///     遍历时不允许对子节点进行改动，若需改动请使用“SafeTraversal”方法！
        /// </remarks>
        public static void Traversal<TKey, TTreeNode>(this IObservableTree<TKey, TTreeNode> tree, Action<TTreeNode?>? expression) where TTreeNode : class, IObservableTree<TKey, TTreeNode>
        {
            expression?.Invoke(tree as TTreeNode);
            if (tree.Children == null)
            {
                return;
            }
            foreach (var child in tree.Children)
            {
                Traversal(child, expression);
            }
        }
        /// <summary>
        /// 遍历树
        /// </summary>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">遍历表达式</param>
        public static void SafeTraversal<TTreeNode>(this IObservableTree<TTreeNode> tree, Action<TTreeNode?>? expression) where TTreeNode : class, IObservableTree<TTreeNode>
        {
            expression?.Invoke(tree as TTreeNode);
            List<TTreeNode> traverseds = new List<TTreeNode>();
            TTreeNode? node = default;
            while ((node = GetNode(traverseds, tree.Children?.ToList())) != null)
            {
                SafeTraversal(node, expression);
                traverseds.Add(node);
            }
        }
        /// <summary>
        /// 遍历树
        /// </summary>
        /// <typeparam name="TKey">键类型</typeparam>
        /// <typeparam name="TTreeNode">树类型</typeparam>
        /// <param name="tree">树实例</param>
        /// <param name="expression">遍历表达式</param>
        public static void SafeTraversal<TKey, TTreeNode>(this IObservableTree<TKey, TTreeNode> tree, Action<TTreeNode?>? expression) where TTreeNode : class, IObservableTree<TKey, TTreeNode>
        {
            expression?.Invoke(tree as TTreeNode);
            List<TTreeNode> traverseds = new List<TTreeNode>();
            TTreeNode? node = default;
            while ((node = GetNode(traverseds, tree?.Children?.ToList())) != null)
            {
                SafeTraversal(node, expression);
                traverseds.Add(node);
            }
        }
        private static TTreeNode? GetNode<TTreeNode>(List<TTreeNode> traverseds, List<TTreeNode>? nodes)
        {
            int idx = 0;
            while (nodes?.Count > idx)
            {
                TTreeNode? treeNode = nodes[idx];
                bool contains = traverseds.Contains(treeNode);
                if (!contains)
                {
                    return treeNode;
                }
                idx++;
            }
            return default;
        }
    }
}