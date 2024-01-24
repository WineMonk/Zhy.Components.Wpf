using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace Zhy.Demo._Model
{
    /// <summary>
    /// 
    /// </summary>
    public class TestItem : ObservableObject
    {
        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }
        private string _path;
        public string Path
        {
            get { return _path; }
            set { SetProperty(ref _path, value); }
        }
        private bool _isChecked;
        public bool Checked
        {
            get { return _isChecked; }
            set { SetProperty(ref _isChecked, value); }
        }

        public RelayCommand<TestItem> SelectPathCommand => new RelayCommand<TestItem>(SelectPath);

        public void SelectPath(TestItem testItem)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            bool? dr = fileDialog.ShowDialog();
            if (!(bool)dr)
                return;
            testItem.Path = fileDialog.FileName;
        }

    }
}
