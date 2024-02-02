using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhy.Components.Wpf.Components.Tree.Extensions;

namespace Zhy.Components.Wpf.Components.Tree
{
    public class TestTree
    {
        public TestTree() { }

        public void Test()
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
            tree01.Children = new List<Tree>
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
            tree02.Children = new List<Tree>
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
            tree03.Children = new List<Tree>
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
            treeRoot.Children = new List<Tree> { tree01, tree02, tree03 };

            Tree? tree = treeRoot.Search(n => n.Name.Contains("-1"));
        }
    }
}
