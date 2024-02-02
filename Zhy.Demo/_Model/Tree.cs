using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using Zhy.Components.Wpf.Components.Tree;

namespace Zhy.Demo._Model
{
    public class Tree : ObservableObject,IObservableTree<Tree>
    {
        private string _name;
        public string Name 
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        [JsonIgnore]
        public Tree? Parent { get; set; }
        private ObservableCollection<Tree>? _children;
        public ObservableCollection<Tree>? Children
        {
            get => _children;
            set => SetProperty(ref _children, value);
        }

        public Tree Clone()
        {
            Tree clone = new Tree();
            clone.Name = Name;
            if (Children?.Count > 0)
            {
                clone.Children = new ObservableCollection<Tree>();
                foreach (var child in Children)
                {
                    Tree childClone = child.Clone();
                    childClone.Parent = clone;
                    clone.Children.Add(childClone);
                }
            }
            return clone;
        }
    }
}
