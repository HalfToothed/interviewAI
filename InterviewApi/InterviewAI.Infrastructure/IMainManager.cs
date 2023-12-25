using LLMSharp.Google.Palm;
using Microsoft.AspNetCore.Http;

namespace InterviewAI.Infrastructure
{
  public interface IMainManager
  {
        Task<string> GenerateQuestions(string prompt);
        Task<string> ExtractFromPdf(IFormFile file);
        Task<string> ExtractFromDocx(IFormFile file);
  }
}