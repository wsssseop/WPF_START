using Core.Dialogs;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Windows;
using WpfApp1.Stores;
using WpfApp1.ViewModels;
using WpfApp1.ViewModels.ContentViewModel;
using WpfApp1.ViewModels.ContentViewModel.Features;
using WpfApp1.Views;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider? Services { get; private set; }


        void Config(IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();

            //store
            services.AddSingleton<ChatListStore>();

            //viewmodel
            services.AddSingleton<MainWindowViewModel>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<ChatViewModel>();
            services.AddTransient<ChatRoomViewModel>();



            services.AddSingleton<IDialogService, DialogService>();

            services.AddSingleton<INavigationService, NavigationService>();

            services.AddSingleton<ChatMessageService>();

        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            var serviceCollection = new ServiceCollection();
            Config(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();

            var mainWindow = Services.GetRequiredService<MainWindow>();

            //mainWindow.DataContext = Services.GetRequiredService<MainWindowViewModel>();

            mainWindow.Show();


        }
    }

}
