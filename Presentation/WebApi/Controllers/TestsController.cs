using Application.Features.Mediator.Commands.TestCommand;
using Application.Features.Mediator.Queries.TestQueries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]

    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTest(CreateTestCommand command)
        {
            await _mediator.Send(command);
            return Ok("Test successfully created.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTests()
        {
            var tests = await _mediator.Send(new GetAllTestQuery());
            return Ok(tests);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTestById(int id)
        {
            var test = await _mediator.Send(new GetTestByIdQuery(id));
            return Ok(test);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTest([FromBody] UpdateTestCommand command)
        {
            await _mediator.Send(command);
            return Ok("Test successfully updated.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveTest(int id)
        {
            await _mediator.Send(new RemoveTestCommand(id));
            return Ok("Test successfully removed.");
        }
        [HttpGet("get-test-with-questions/{id}")]
        public async Task<IActionResult> GetTestWithQuestions(int id)
        {
            // Token'dan kullanıcı ID'sini al
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdClaim, out int userId))
                return Unauthorized(new { error = "Geçersiz token veya kullanıcı bulunamadı." });

            // Query'yi oluştur ve token'dan gelen userId'yi ekle
            var query = new GetTestWithQuestionsQuery(id, userId);


            var result = await _mediator.Send(query);

            if (result == null)
                return NotFound();

            return Ok(result);
        }


    }
}
