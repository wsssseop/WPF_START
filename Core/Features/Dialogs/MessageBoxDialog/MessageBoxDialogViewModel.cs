using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dialogs.MessageBoxDialog
{
    public partial class MessageBoxDialogViewModel : DialogViewModelBase
    {
        [ObservableProperty]
        private string _message = string.Empty;
        [ObservableProperty]

        private string _okText = "확인";
        [ObservableProperty]

        private string? _cancelText;
        [RelayCommand]
        private void OK() => Close(true);
        [RelayCommand]

        private void Cancel() => Close(false);


    }
}
