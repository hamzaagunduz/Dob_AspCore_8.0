using Microsoft.AspNetCore.Mvc;
using static WebApi.ViewModels;
using WebApi.Services;
using Microsoft.AspNetCore.Authorization;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class AIController : ControllerBase
{
    private readonly AIService _aiService;

    public AIController(AIService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> Chat([FromBody] ChatRequestVM chatRequest, CancellationToken cancellationToken)
    {
        await _aiService.GetMessageStreamAsync(chatRequest.Prompt, chatRequest.ConnectionId, cancellationToken);
        return Ok(); // ya da response dönüyorsan onu return et
    }

    [HttpPost("analyze")]
    public async Task<IActionResult> Analyze([FromBody] AnalysisRequestVM request)
    {
        var result = await _aiService.GetAnalysisSuggestionsAsync(request);
        return Ok(result);
    }
}
