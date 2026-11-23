using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Dialogs;
using Core.Logging;
using Core.Logging.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Stores;
using WpfApp1.ViewModels.ContentViewModel.Features;

namespace WpfApp1.ViewModels.ContentViewModel
{
    public partial class ChatViewModel :ObservableObject
    {
        private readonly ChatListStore _chatListStore;
        private readonly IDialogService _dialog;
        [ObservableProperty]
        private ObservableCollection<ChatListModel> _chatList;

        private static readonly Logger _testLog = LogService.GetLogger("Chat", Process.GetCurrentProcess().ProcessName);

        [ObservableProperty]
        private ChatListModel? _selectedChat;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(PictureCommand))]
        private bool _onBulb = false;

        //[ObservableProperty]
        //private object _chatRoom;
        public ChatRoomViewModel ChatRoom { get; }


        public ChatViewModel(ChatListStore chatListStore, ChatRoomViewModel chatRoomViewModel, IDialogService dialog)
        {
            ChatRoom = chatRoomViewModel;
            _dialog = dialog;
            _chatListStore = chatListStore;
            ChatList = new ObservableCollection<ChatListModel>(_chatListStore.ChatList);
        }

        partial void OnSelectedChatChanged(ChatListModel? value)
        {
            // 선택이 바뀌면 오른쪽 채팅방 내용 불러오기 등
            _ = ChatRoom.LoadAsync(value.Name);
        }

        /*
        1. bool 이 바뀐다.
        2. 바뀌면서 NotifyCanExecuteChangeFor로 CanExcute를 검사하게 만든다.
        3. RelayCommand에도 Canexcute bool 을 1개 넣어야한다.
         */

        [RelayCommand]
        private void Option()
        {
            _testLog.Seq($"option Btn 실행");

        }
        [RelayCommand]
        private async Task DeleteAsync()
        {
            if (!await _dialog.ConfirmAsync("안녕하세요?"))
            {

                Debug.WriteLine("false");
                return;

            }
            else
            {
                Debug.WriteLine("true");
            }


        }


        [RelayCommand(CanExecute =nameof(OnBulb))]
        private void Picture()
        {
            
        }

        [RelayCommand]
        private void Chat()
        {
            OnBulb = !OnBulb;
        }


    }
}
