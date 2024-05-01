using LLMSharp.Google.Palm;

namespace InterviewAI.Infrastructure
{
  public interface IPalmService
  {
        Task<string> GenerateContent(string prompt);
  }
}