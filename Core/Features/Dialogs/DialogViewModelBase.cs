using CommunityToolkit.Mvvm.ComponentModel;
using Core.Dialogs.MessageBoxDialog;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dialogs
{
    public interface IDialogService
    {

        // Modal 창
        Task<bool?> ShowDialogAsync(DialogViewModelBase viewModel);
        // Modal
        void Show(DialogViewModelBase viewModel);
        void Close(DialogViewModelBase viewModel);
    }

    public abstract partial class DialogViewModelBase : ObservableObject
    {
        [ObservableProperty]
        private string _title = string.Empty;

        public event Action<bool?>? RequestClose;
        protected void Close(bool? result = null) => RequestClose?.Invoke(result);


    }

    public static class DialogServiceExtensions
    {
        public static async Task<bool> ConfirmAsync(this IDialogService dialog, string message, string title = "확인")
        {
            var vm = new MessageBoxDialogViewModel { Title = title, Message = message, CancelText = "취소" };
            return await dialog.ShowDialogAsync(vm) == true;
        }
    }
}
