using LLMSharp.Google.Palm;

namespace InterviewAI.Infrastructure
{
  public interface IPalmService
  {
        Task<PalmTextCompletionResponse> GenerateQuestions(string prompt);
  }
}