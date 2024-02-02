using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Zhy.Components.Wpf.Components.Tree;

namespace Zhy.Demo._Model
{
    internal class Tree2 : ITree<string, Tree2>
    {
        public Tree2 this[string key] 
        {
            get => Children.First(t=>t.Key.Equals(key));
            set
            {
                Tree2 tree2 = Children.First(t => t.Key.Equals(key));
                int idx = Children.IndexOf(tree2);
                if (idx != -1)
                {
                    Children[idx] = value;
                }
            } 
        }
        public string Name {  get; set; }
        public string Key { get; set; }
        public string PKey { get => Parent?.Key; }
        [JsonIgnore]
        public Tree2 Parent { get; set; }
        public List<Tree2> Children { get; set; }

        public Tree2 Clone()
        {
            Tree2 clone = new Tree2();
            clone.Name = Name;
            clone.Key = Key;
            if (Children?.Count > 0)
            {
                clone.Children = new List<Tree2>();
                foreach (var child in Children)
                {
                    Tree2 childClone = child.Clone();
                    childClone.Parent = clone;
                    clone.Children.Add(childClone);
                }
            }
            return clone;
        }
    }
}
