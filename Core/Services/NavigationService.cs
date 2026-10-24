using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface INavigationService
    {
        object? CurrentView { get; }

        void NavigateTo<TViewModel>() where TViewModel : class;
    }

    public partial class NavigationService : ObservableObject, INavigationService
    {
        private readonly IServiceProvider _serviceProvider;
        [ObservableProperty]
        private object? _currentView;
        public NavigationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void NavigateTo<TViewModel>()
            where TViewModel : class
        {
            CurrentView = _serviceProvider.GetRequiredService<TViewModel>();
        }


    }
}
