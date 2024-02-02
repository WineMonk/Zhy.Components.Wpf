using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhy.Components.Wpf.Components.Tree
{
    internal class Tree : ITree<Tree>
    {
        public string Name { get; set; }
        public Tree Parent { get; set; }
        public List<Tree> Children { get; set; }

        public Tree Clone()
        {
            throw new NotImplementedException();
        }
    }
}
