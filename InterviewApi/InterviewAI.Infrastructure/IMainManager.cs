using LLMSharp.Google.Palm;

namespace InterviewAI.Infrastructure
{
  public interface IMainManager
  {
        Task<string> GenerateQuestions(string prompt);
  }
}