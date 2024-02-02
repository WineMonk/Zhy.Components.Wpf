using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zhy.Demo._Model;

namespace Zhy.Demo._ViewModel
{
    public class ZhyTreeViewWindowViewModel : ObservableObject
    {
        private ObservableCollection<Tree> _treeItems;
        public ObservableCollection<Tree> TreeItems
        {
            get { return _treeItems; }
            set { SetProperty(ref _treeItems, value); }
        }

        private Tree _selectedTreeItem;
        public Tree SelectedTreeItem
        {
            get { return _selectedTreeItem; }
            set { SetProperty(ref _selectedTreeItem, value); }
        }

        public ZhyTreeViewWindowViewModel() 
        {
            TestTree testTree = new TestTree();
            _treeItems = new ObservableCollection<Tree>
            {
                testTree.GetTree()
            };
        }

        public void AddNode()
        {
            SelectedTreeItem.Children ??= new ObservableCollection<Tree>();
            SelectedTreeItem.Children.Add(new Tree
            {
                Name = "add",
                Parent = SelectedTreeItem
            });
        }
        
        public void DeleteNode()
        {
            SelectedTreeItem.Parent?.Children.Remove(SelectedTreeItem);
        }
        
        public void UpdateNode()
        {
            SelectedTreeItem.Name = "update";
        }
    }
}
