using Core.Models;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace Core.Services
{
    //public interface IChatMessageService
    //{
    //    Task<IReadOnlyList<ChatMessageService>>
    //}

    public class ChatMessageService
    {
        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        private readonly string _folder = Path.Combine(AppContext.BaseDirectory, "Messages");

        private readonly SemaphoreSlim _fileLock = new(1, 1);

        private string GetFilePath(string chatId) => Path.Combine(_folder, $"{chatId}.json");

        public async Task<ChatMessageModel> LoadAsync(string chatId, CancellationToken token)
        {
            string path = GetFilePath(chatId);

            await _fileLock.WaitAsync(token);
            try
            {
                if (!File.Exists(path))
                    return new ChatMessageModel { ChatID = chatId };

                var jsonString = await File.ReadAllTextAsync(path, token);
                ChatMessageModel? Message = JsonSerializer.Deserialize<ChatMessageModel>(jsonString, _jsonOptions);

                return Message ?? new ChatMessageModel { ChatID = chatId };
            }
            finally
            {
                _fileLock.Release();
            }
        }

        public async Task SaveAsync(ChatMessageModel chat, CancellationToken token)
        {
            string path = GetFilePath(chat.ChatID);
            string tempPath = path + ".tmp";
            string jsonString = JsonSerializer.Serialize(chat, _jsonOptions);

            await _fileLock.WaitAsync(token);

            try
            {
                if (!Directory.Exists(_folder))
                    Directory.CreateDirectory(_folder);
                await File.WriteAllTextAsync(tempPath, jsonString, token);
                File.Move(tempPath, path, overwrite: true);
            }

            finally
            { _fileLock.Release(); }


        }

    }
}
