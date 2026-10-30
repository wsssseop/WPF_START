using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Helpers;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApp1.ViewModels.ContentViewModel
{
    public interface IHavePassword
    {
        SecureString SecurePassword { get; }

    }
    public partial class LoginViewModel : ObservableObject
    {
        private INavigationService _navigationService;
        [ObservableProperty]
        private string _email = string.Empty;

        public LoginViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

        }

        [RelayCommand]
        private async Task LoginAsync(IHavePassword passwordHolder)
        {

            string password = passwordHolder.SecurePassword.Unsecure();

            await Task.Delay(500);
            _navigationService.NavigateTo<ChatViewModel>();

        }


    }
}
