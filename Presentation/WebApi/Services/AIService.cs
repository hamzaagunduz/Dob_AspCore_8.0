using Microsoft.AspNetCore.SignalR;
using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;
using Microsoft.SemanticKernel;
using WebApi.Hubs;

namespace WebApi.Services
{
    public class AIService(IHubContext<AIHub> hubContext, IChatCompletionService chatCompletionService, Kernel kernel)
    {
        public async Task GetMessageStreamAsync(string prompt, string connectionId, CancellationToken? cancellationToken = default!)
        {
            OpenAIPromptExecutionSettings openAIPromptExecutionSettings = new()
            {
                FunctionChoiceBehavior = FunctionChoiceBehavior.Auto()
            };

            var history = HistoryService.GetChatHistory(connectionId);

            history.AddUserMessage(prompt);
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

            }
            history.AddAssistantMessage(responseContent);
        }



        public async Task<string> GetMessageAsync(string prompt, CancellationToken? cancellationToken = default!)
        {
            try
            {
                var history = HistoryService.GetChatHistory("some-connection-id");
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
                // Hata varsa string olarak dönebilirsin ya da throw edebilirsin.
                return $"Hata: {ex.Message}";
            }
        }

    }
}
