using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Core.Models;
using Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApp1.ViewModels.ContentViewModel.Features
{


    public partial class ChatRoomViewModel : ObservableRecipient
    {
        private readonly ChatMessageService _chatMessageService;
        private CancellationTokenSource? _cts;
        [ObservableProperty]
        private ObservableCollection<MessageModel> _messages = new ObservableCollection<MessageModel>();

        [ObservableProperty]
        private string _textboxMessage;

        [ObservableProperty]
        private string _title = string.Empty;
        [ObservableProperty]
        private bool _isLoading;
        [ObservableProperty]
        private string? _errorMessage;

        private string? _currentChatId;   // 지금 열린 방

        public ChatRoomViewModel(ChatMessageService chatMessageService)
        {
            _chatMessageService = chatMessageService;
        }

        [RelayCommand]
        private void SendMessage()
        {
            MessageModel messageModel = new MessageModel {IsMine=false, Message= TextboxMessage, Sender="me" };
            Messages.Add(messageModel);
            TextboxMessage = string.Empty;
        }

        public async Task LoadAsync(string chatId)
        {

            //var cts = new CancellationTokenSource();
            //var previous = Interlocked.Exchange(ref _cts, cts);
            //previous?.Cancel();          // 이전 요청에는 "취소 표시"만
            _cts?.Cancel();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            // null 이면 save해야 할 대상도 지금 불려온 놈.

            var prevMessages = Messages.ToList();

            Messages.Clear();
            IsLoading = true;
            ErrorMessage = null;

            try
            {
                var chat = await _chatMessageService.LoadAsync(chatId, token);
                token.ThrowIfCancellationRequested();
                foreach (var message in chat.Messages)
                    Messages.Add(message);
                if (_currentChatId is not null)
                    await SaveAsync(new ChatMessageModel { ChatID = _currentChatId, Messages = prevMessages });

                _currentChatId = chatId;

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            finally
            {
                if (!token.IsCancellationRequested)
                    IsLoading = false;
            }
        }
        
        public async Task SaveAsync(ChatMessageModel chatMessage )
        {
            using var cts = new CancellationTokenSource();
            var token = cts.Token;
            // save를 
            //var test = new ChatMessageModel { ChatID = chatId, Messages = Messages.ToList() };

            try
            {
                await _chatMessageService.SaveAsync(chatMessage, token);
            }
            catch (Exception) { }
            finally
            {

            }

        }


    }
}
