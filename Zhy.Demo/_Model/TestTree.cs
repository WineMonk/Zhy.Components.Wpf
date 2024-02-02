using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Zhy.Components.Wpf.Components.Tree;

namespace Zhy.Demo._Model
{
    public class TestTree
    {
        public TestTree() { }

        public Tree GetTree() 
        {
            Tree treeRoot = new Tree
            {
                Name = "0"
            };
            Tree tree01 = new Tree
            {
                Name = "0-1",
                Parent = treeRoot
            };
            tree01.Children = new ObservableCollection<Tree>
            {
                new Tree
                {
                    Name = "0-1-1",
                    Parent = tree01
                },
                new Tree
                {
                    Name = "0-1-2",
                    Parent = tree01
                },
                new Tree
                {
                    Name = "0-1-3",
                    Parent = tree01
                },
            };
            Tree tree02 = new Tree
            {
                Name = "0-2",
                Parent = treeRoot
            };
            tree02.Children = new ObservableCollection<Tree>
            {
                new Tree
                {
                    Name = "0-2-1",
                    Parent = tree02
                },
                new Tree
                {
                    Name = "0-2-2",
                    Parent = tree02
                },
                new Tree
                {
                    Name = "0-2-3",
                    Parent = tree02
                },
            };
            Tree tree03 = new Tree
            {
                Name = "0-3",
                Parent = treeRoot
            };
            tree03.Children = new ObservableCollection<Tree>
            {
                new Tree
                {
                    Name = "0-3-1",
                    Parent = tree03
                },
                new Tree
                {
                    Name = "0-3-2",
                    Parent = tree03
                },
                new Tree
                {
                    Name = "0-3-3",
                    Parent = tree03
                },
            };
            treeRoot.Children = new ObservableCollection<Tree> { tree01, tree02, tree03 };
            return treeRoot;
        }

        public void TestSearch()
        {
            Tree treeRoot = GetTree();
            Tree? tree = treeRoot.Search(n => n?.Name.Contains("-1") == true);
        }

        public void TestSafeTraversal()
        {
            Tree treeRoot = GetTree();
            treeRoot.SafeTraversal(t =>
            {
                if (t?.Name.Contains("-2") == true)
                {
                    t?.Parent?.Children?.Remove(t);
                }
            });
        }

        public string TestToJson()
        {
            Tree treeRoot = GetTree();
            string jsonText = JsonSerializer.Serialize(treeRoot);
            return jsonText;
        }
        public Tree? TestFromJson(string jsonText)
        {
            Tree? treeRoot = TreeUtil.JsonToObservableTree<Tree>(jsonText);
            return treeRoot;
        }
    }
}
