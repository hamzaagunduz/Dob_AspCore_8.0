using Microsoft.SemanticKernel.ChatCompletion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public static class HistoryServiceV2
    {
        private static readonly Dictionary<string, ChatHistory> _chatHistories = new();

        public static ChatHistory GetChatHistory(string connectionId)
        {
            if (_chatHistories.TryGetValue(connectionId, out var chatHistory))
                return chatHistory;

            chatHistory = new();
            _chatHistories.Add(connectionId, chatHistory);
            return chatHistory;
        }
    }
}
