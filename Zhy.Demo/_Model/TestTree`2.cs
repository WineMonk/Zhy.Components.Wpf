using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Zhy.Components.Wpf.Components.Tree;

namespace Zhy.Demo._Model
{
    internal class TestTree2
    {
        public TestTree2() { }

        public Tree2 GetTree()
        {
            Tree2 treeRoot = new Tree2
            {
                Key = "0",
                Name = "0",
            };
            Tree2 tree1 = new Tree2
            {
                Key = "0-1",
                Name = "0-1",
                Parent = treeRoot,
            };
            tree1.Children = new List<Tree2>
            {
                new Tree2
                {
                    Key = "0-1-1",
                    Name = "0-1-1",
                    Parent = tree1,
                },
                new Tree2
                {
                    Key = "0-1-2",
                    Name = "0-1-2",
                    Parent = tree1,
                },
                new Tree2
                {
                    Key = "0-1-3",
                    Name = "0-1-3",
                    Parent = tree1,
                },
            };
            Tree2 tree2 = new Tree2
            {
                Key = "0-2",
                Name = "0-2",
                Parent = treeRoot,
            };
            tree2.Children = new List<Tree2>
            {
                new Tree2
                {
                    Key = "0-2-1",
                    Name = "0-2-1",
                    Parent = tree2,
                },
                new Tree2
                {
                    Key = "0-2-2",
                    Name = "0-2-2",
                    Parent = tree2,
                },
                new Tree2
                {
                    Key = "0-2-3",
                    Name = "0-2-3",
                    Parent = tree2,
                },
            };
            Tree2 tree3 = new Tree2
            {
                Key = "0-3",
                Name = "0-3",
                Parent = treeRoot,
            };
            tree3.Children = new List<Tree2>
            {
                new Tree2
                {
                    Key = "0-3-1",
                    Name = "0-3-1",
                    Parent = tree3,
                },
                new Tree2
                {
                    Key = "0-3-2",
                    Name = "0-3-2",
                    Parent = tree3,
                },
                new Tree2
                {
                    Key = "0-3-3",
                    Name = "0-3-3",
                    Parent = tree3,
                },
            };
            treeRoot.Children = new List<Tree2> { tree1, tree2, tree3 };
            return treeRoot;
        }

        public void TestSearch()
        {
            Tree2 treeRoot = GetTree();
            Tree2? tree = treeRoot.Search(t => t.Name.Contains("-2"), true);
        }

        public void TestSafeTraversal()
        {
            Tree2 treeRoot = GetTree();
            treeRoot.SafeTraversal(t =>
            {
                if (t.Name.Contains("-1"))
                {
                    t.Parent.Children.Remove(t);
                }
            });
        }

        public string TestToJson()
        {
            Tree2 treeRoot = GetTree();
            string jsonText = JsonSerializer.Serialize(treeRoot);
            return jsonText;
        }
        public Tree2? TestFromJson(string jsonText)
        {
            Tree2? treeRoot = JsonSerializer.Deserialize<Tree2>(jsonText);
            return treeRoot;
        }
    }
}
