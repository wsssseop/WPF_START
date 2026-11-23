using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Stores;

namespace WpfApp1.Startup
{
    public record StartupProgress(string Message, double Persent);

    public class AppInitializer
    {
        private readonly ChatListStore _chatListStore;

        public AppInitializer(ChatListStore chatListStore)
        {
            _chatListStore = chatListStore;
        }

        //public async Task InitializeAsync(IProgress<StartupProgress> progress, CancellationToken token = default)
        //{
        //    progress.Report(new("채팅 목록 불러오는 중...", 10));
        //    await _chatListStore.LoadAsync(token);

        //    progress.Report(new("설정 불러오는 중...", 50));
        //    await Task.Delay(500, token);   // 확인용: 나중에 다른 Store/장비 연결로 교체

        //    progress.Report(new("화면 준비 중...", 90));
        //}
    }
}
