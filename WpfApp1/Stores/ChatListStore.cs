using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WpfApp1.Simulator;

namespace WpfApp1.Stores
{
    public class ChatListModel
    {
        public string Initials { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string ProfilePictureRGB { get; set; } = string.Empty;


    }

    public class ChatListStore : ObservableObject
    {
        public List<ChatListModel> ChatList= new();
   

        public ChatListStore()
        {
            Load();

        }

        private void Load()
        {
            string filePath = @"C:\Users\USER\Desktop\WPF_AX\WPF_START\config.json";
            try
            {

                if (!File.Exists(filePath))
                    throw new FileNotFoundException($"설정 파일이 없습니다.{filePath}");

                var jsonstring = File.ReadAllText(filePath);


                ChatList = JsonSerializer.Deserialize<List<ChatListModel>>(jsonstring) ?? new();


            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

        }
    }
}
