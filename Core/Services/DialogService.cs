using Core.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Core.Services
{
    public class DialogService : IDialogService
    {
        private readonly Dictionary<Type, DialogWindow> _openWindows = new();



        private static DialogWindow CreateWindow(DialogViewModelBase viewModel)
        {
            var owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w=>w.IsActive) ?? Application.Current.MainWindow;
            return new DialogWindow { DataContext = viewModel, Owner = owner };
        }

        public Task<bool?> ShowDialogAsync(DialogViewModelBase viewModel)
        {
            var window = CreateWindow(viewModel);

            void OnRequestClose(bool? result)
            {
                if (result is null)
                    window.Close();
                else
                    window.DialogResult = result;
            }

            viewModel.RequestClose += OnRequestClose;

            try
            {
                return Task.FromResult(window.ShowDialog());
            }
            finally
            {
                viewModel.RequestClose -= OnRequestClose;
            }
        }



        public void Close(DialogViewModelBase viewModel)
        {
            if(_openWindows.TryGetValue(viewModel.GetType(), out var window))
                window.Close();

        }

        public void Show(DialogViewModelBase viewModel)
        {
            var key = viewModel.GetType();
            if(_openWindows.TryGetValue(key, out var opened))
            {
                opened.Activate();
                return;
            }
            var window = CreateWindow(viewModel);

            void OnRequestClose(bool? _)=>window.Close();

            viewModel.RequestClose += OnRequestClose;

            window.Closed += (_, _) =>
            {
                viewModel.RequestClose -= OnRequestClose;
                _openWindows.Remove(key);
            };
            _openWindows[key] = window;
            window.Show();

        }


    }
}
