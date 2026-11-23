using Core.Dialogs;
using Core.Logging;
using Core.Logging.Service;
using Core.Logging.Targets;
using Core.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private static readonly Logger _uiLog = LogService.GetLogger("UI", Process.GetCurrentProcess().ProcessName);

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

            Thread.CurrentThread.Name = "UI";

            string appName = Process.GetCurrentProcess().ProcessName;
            string logRoot = Directory.Exists(@"D:\") ? @$"D:\{appName}\Log" : Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), appName, "Log");

            

            LogService.Configure(LogLevel.Debug, new DebugOutputTarget(), new AsyncTarget(new FileTarget(logRoot)));

            EventManager.RegisterClassHandler(typeof(ButtonBase), ButtonBase.ClickEvent, new RoutedEventHandler(OnAnyButtonClick));
            //EventManager.RegisterClassHandler(typeof(ContentControl), ContentControl.MouseDownEvent, new RoutedEventHandler(OnAnyControlClick));
            

            var serviceCollection = new ServiceCollection();
            Config(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();

            var mainWindow = Services.GetRequiredService<MainWindow>();

            //mainWindow.DataContext = Services.GetRequiredService<MainWindowViewModel>();

            mainWindow.Show();

        }

        private void OnAnyButtonClick(object sender, RoutedEventArgs e)
        {
            if (sender is not ButtonBase button)
                return;

            string window = Window.GetWindow(button)?.GetType().Name ?? "?";
            _uiLog.Event($"버튼 클릭: {GetButtonLabel(button)} ({window})");
        }

        private void OnAnyControlClick(object sender, RoutedEventArgs e)
        {
            if (sender is not ContentControl obj)
                return;
            var t = obj.GetType();
            
            string window = Window.GetWindow(obj)?.GetType().Name ?? "?";
            _uiLog.Event($"클릭: {t.Name} ({window})");

        }

        private static string GetButtonLabel(ButtonBase button)
        {
            string automationName = AutomationProperties.GetName(button);
            if (!string.IsNullOrEmpty(automationName))
                return automationName;

            if (!string.IsNullOrEmpty(button.Name))
                return button.Name;

            return button.Content as string ?? button.GetType().Name;
        }
    }

}
