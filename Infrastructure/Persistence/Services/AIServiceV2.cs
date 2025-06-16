using Microsoft.AspNetCore.SignalR;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using Persistence.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Persistence.Models;

namespace Persistence.Services
{
    public class AIServiceV2(IHubContext<AIHubV2> hubContext, IChatCompletionService chatCompletionService, Kernel kernel)
    {
        public async Task GetMessageStreamAsync(string prompt, string connectionId, CancellationToken? cancellationToken = default!)
        {
            OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            var history = HistoryServiceV2.GetChatHistory(connectionId);

            string formattedPrompt = $"""
            Aşağıdaki soruyu detaylı ve öğretici bir şekilde adım adım yanıtla.
            - Önce soruyu analiz et,
            - Konu hakkında kısa ama net bir genel bilgi ver,
            - Sorunun olası doğru yanıtını açıkla ve neden o yanıtın doğru olacağını mantıksal olarak belirt,
            - Eğer seçenekler yoksa, farklı ihtimalleri değerlendirerek neden-sonuç ilişkisi kurarak anlat.

            Soru:
            {prompt}
            """;

            history.AddUserMessage(formattedPrompt);
            string responseContent = "";
            try
            {
                await foreach (var response in chatCompletionService.GetStreamingChatMessageContentsAsync(history, executionSettings: openAIPromptExecutionSettings, kernel: kernel))
                {
                    cancellationToken?.ThrowIfCancellationRequested();
                    await hubContext.Clients.Client(connectionId).SendAsync("ReceiveMessage", response.ToString());
                    responseContent += response.ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hata oluştu: {ex.Message}");
            }

            history.AddAssistantMessage(responseContent);
        }

        public async Task<string> GetMessageAsync(string prompt, CancellationToken? cancellationToken = default!)
        {
            try
            {
                var history = HistoryServiceV2.GetChatHistory("some-connection-id");
                history.AddUserMessage(prompt);

                OpenAIPromptExecutionSettings settings = new()
                {
                    FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
                };

                var response = await chatCompletionService.GetChatMessageContentAsync(
                    history,
                    executionSettings: settings,
                    kernel: kernel,
                    cancellationToken: cancellationToken ?? CancellationToken.None);

                string result = response?.Content ?? "";
                history.AddAssistantMessage(result);

                return result;
            }
            catch (Exception ex)
            {
                return $"Hata: {ex.Message}";
            }
        }

        public async Task<Dictionary<string, string>> GetAnalysisSuggestionsAsync(AnalysisRequestVMV2 request)
        {
            var results = new Dictionary<string, string>();

            foreach (var topic in request.Data)
            {
                string prompt = $"""
                Öğrencinin '{topic.Topic}' adlı konuya ait {request.AnalysisType} performansı:
                - ✅ Doğru: {topic.Correct}
                - ❌ Yanlış: {topic.Wrong}
                - ⏱️ Süre: {topic.Duration} dakika

                Bu verilere göre kısa bir analiz yap ve öğrenciye bu konu özelinde öneri sun.
                """;

                var response = await chatCompletionService.GetChatMessageContentAsync(
                    prompt,
                    new OpenAIPromptExecutionSettings { FunctionChoiceBehavior = FunctionChoiceBehavior.Auto() },
                    kernel
                );

                results[topic.Topic] = response?.Content?.Trim() ?? "Cevap alınamadı.";
            }

            return results;
        }
    }
}
