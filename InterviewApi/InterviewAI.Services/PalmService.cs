using Google.Ai.Generativelanguage.V1Beta2;
using InterviewAI.Infrastructure;
using LLMSharp.Google.Palm;

namespace InterviewAI.Services
{
  public class PalmService : IPalmService
  {
    public static string apiKey = "YOUR-API-KEY";

    public async Task<PalmTextCompletionResponse> GenerateQuestions(string prompt)
    {
        GooglePalmClient client = new GooglePalmClient(apiKey);
        return await client.GenerateTextAsync(prompt);
         
    }
  }
}
