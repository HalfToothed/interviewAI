using InterviewAI.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Runtime.CompilerServices;

namespace InterviewAI.Api.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class MasterController : ControllerBase
  {
        private readonly IMainManager _manager;

    public MasterController(IMainManager manager)
    {
            _manager = manager;
    }

    [HttpPost("GenerateQuestions")]
    public async Task<IActionResult> GenerateQuestions(Model text)
    {
            var result = await _manager.GenerateQuestions(text.Prompt);
            return Ok(result);
    }
  }
}
