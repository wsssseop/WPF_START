using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    // 채팅 id, 
    // 
    public class MessageModel
    {
        public string Sender {  get; set; } = string.Empty;
        public DateTime CreateAt { get; set; } = DateTime.Now;
        public bool IsMine {  get; set; }
        public string Message { get; set; } = string.Empty;
    }

    public class ChatMessageModel
    {
        public string ChatID { get; set; } = string.Empty;
        public List<MessageModel> Messages { get; set; } = new();

    }
}
