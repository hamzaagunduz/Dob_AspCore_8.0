using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Application.Interfaces.IShopRepository;
using Persistence.Models;
using Persistence.Services;
using static Application.ViewModels.ViewModels;

[Route("api/[controller]")]
[ApiController]
public class AIController : ControllerBase
{
    private readonly AIServiceV2 _aiServiceV2;
    private readonly IShopRepository _repository;

    public AIController(
        AIServiceV2 aiServiceV2,
        IShopRepository repository)
    {
        _aiServiceV2 = aiServiceV2;
        _repository = repository;
    }




    // 🔄 V2 Chat (yeni sistem)
    [HttpPost("chat/v2")]
    public async Task<IActionResult> ChatV2([FromBody] ChatRequestVM chatRequest, CancellationToken cancellationToken)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");

        bool hasAiAccess = await _repository.HasActiveShopItemAsync(userId, 1);
        if (!hasAiAccess)
        {
            return new JsonResult(new Dictionary<string, string>
            {
                { "data", "Bu özelliği kullanmak için AI analiz paketini satın almanız gerekiyor" }
            });
        }

        await _aiServiceV2.GetMessageStreamAsync(chatRequest.Prompt, chatRequest.ConnectionId, cancellationToken);
        return Ok();
    }

    // 🔄 V2 Analyze (yeni sistem)
    [HttpPost("analyze/v2")]
    public async Task<IActionResult> AnalyzeV2([FromBody] AnalysisRequestVMV2 request)
    {
        var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(userIdClaim, out int userId))
            return Unauthorized("Geçersiz token veya kullanıcı bulunamadı.");

        bool hasAiAccess = await _repository.HasActiveShopItemAsync(userId, 1);
        if (!hasAiAccess)
        {
            return new JsonResult(new Dictionary<string, string>
            {
                { "Bilgi", "Bu özelliği kullanmak için AI analiz paketini satın almanız gerekiyor" }
            });
        }

        var result = await _aiServiceV2.GetAnalysisSuggestionsAsync(request);
        return Ok(result);
    }
}
