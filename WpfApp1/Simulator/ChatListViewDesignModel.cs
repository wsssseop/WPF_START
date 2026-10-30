using Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Stores;

namespace WpfApp1.Simulator
{
    public class ChatListViewDesignModel
    {
        private static readonly Lazy<ChatListViewDesignModel> _lazy = new Lazy<ChatListViewDesignModel>(new ChatListViewDesignModel());
        public static ChatListViewDesignModel Instance => _lazy.Value;

        // ChatViewModel과 같은 이름의 속성 → 같은 바인딩 경로가 동작
        public ObservableCollection<ChatListModel> ChatList { get; } =
        [
            new() { Initials = "LM", Name = "Luke",    Message = "This chat app is awesome!",        ProfilePictureRGB = "3099c5" },
            new() { Initials = "JA", Name = "Jesse",   Message = "Hey dude, here are the new icons", ProfilePictureRGB = "fe4503" },
            new() { Initials = "PL", Name = "Parnell", Message = "The new server is up",             ProfilePictureRGB = "00d405" },
        ];
        public static ChatListModel SampleItem => Instance.ChatList[0];

        public ObservableCollection<MessageModel> Messages { get; } = [
            new(){ Message = "No1", IsMine = false , Sender="tester"},
            new(){ Message = "No2", IsMine = true , Sender="tester"},
            new(){ Message = "No3", IsMine = false , Sender="tester"},
            new(){ Message = "No4", IsMine = true , Sender="tester"},
        ];



        private ChatListViewDesignModel() { }

    }




}
