namespace WebApi
{
    public class ViewModels
    {
        public record ChatRequestVM(string Prompt, string ConnectionId)
        {

        }
    }
}
