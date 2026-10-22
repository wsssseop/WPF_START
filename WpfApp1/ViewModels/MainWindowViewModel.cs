using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {

        [ObservableProperty]
        private string _myList;


        [ObservableProperty]
        private string _test;
        [ObservableProperty]
        private string _testb;
        [ObservableProperty]
        private string _testc;
        [ObservableProperty]
        private string _testdd;


        public MainWindowViewModel()
        {
            MyList = "hello!!";

        }


        [RelayCommand]
        private void he()
        {
            MessageBox.Show("hello");
        }

    }
}
