using Microsoft.AspNetCore.Mvc;
using static WebApi.ViewModels;
using WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces.IShopRepository;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class AIController : ControllerBase
{
    private readonly AIService _aiService;
    private readonly IShopRepository _repository;

    public AIController(AIService aiService, IShopRepository repository)
    {
        _aiService = aiService;
        _repository = repository;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> Chat([FromBody] ChatRequestVM chatRequest, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");
        bool hasAiAccess = await _repository.HasActiveShopItemAsync(userId, 1);
        var data = new Dictionary<string, string>
        {
            { "data", "Bu özelliği kullanmak için AI analiz paketini satın almanız gerekiyor" },
        };
        if (!hasAiAccess)
            return new JsonResult(data);
        await _aiService.GetMessageStreamAsync(chatRequest.Prompt, chatRequest.ConnectionId, cancellationToken);
        return Ok(); // ya da response dönüyorsan onu return et
    }

    [HttpPost("analyze")]
    public async Task<IActionResult> Analyze([FromBody] AnalysisRequestVM request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");
        bool hasAiAccess = await _repository.HasActiveShopItemAsync(userId, 1);
        var data = new Dictionary<string, string>
        {
            { "Bilgi", "Bu özelliği kullanmak için AI analiz paketini satın almanız gerekiyor" },
        };
        if (!hasAiAccess)
            return new JsonResult(data);

        var result = await _aiService.GetAnalysisSuggestionsAsync(request);
        return Ok(result);
    }
}
