using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.ViewModels.ContentViewModel;
using WpfApp1.Views.ContentView;

namespace WpfApp1.ViewModels
{
    public partial class MainWindowViewModel : ObservableObject
    {

        [ObservableProperty]
        private int _titleHeight = 42;
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(OuterMarginSize))]
        [NotifyPropertyChangedFor(nameof(OuterMarginThickness))]
        private WindowState _windowState;

        [ObservableProperty]
        private double _windowMinimumWidth = 400;
        [ObservableProperty]
        private double _windowMinimumHeight = 400;

        public INavigationService Navigate { get; }

        public double OuterMarginSize => WindowState == WindowState.Maximized ? 0 : 10;

        public Thickness OuterMarginThickness => new Thickness(OuterMarginSize);

        public MainWindowViewModel(INavigationService navigationService)
        {
            Navigate = navigationService;
            Navigate.NavigateTo<LoginViewModel>();
        }


        [RelayCommand]
        private void Menu()
        {
            //var t= new Point(100, 100);
            //SystemCommands.ShowSystemMenu( , t);
        }

        [RelayCommand]
        private void Minimize() => WindowState = WindowState.Minimized;
 
        [RelayCommand]
        private void Maximize() => WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;

        [RelayCommand]
        private void Close()
        {
        }

    }
}
