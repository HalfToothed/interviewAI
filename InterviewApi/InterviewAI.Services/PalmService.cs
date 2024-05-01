using InterviewAI.Infrastructure;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;

namespace InterviewAI.Services
{
  public class PalmService : IPalmService
  {
    public static string apiKey = Environment.GetEnvironmentVariable("Gemini-API-Key");

    public async Task<string> GenerateContent(string prompt)
    {
            var url = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key=" + apiKey;
            var content = new StringContent($"{{\"contents\":[{{\"parts\":[{{\"text\": \"{prompt}\"}}]}}]}}", Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                var response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    dynamic jsonObject = JsonConvert.DeserializeObject(responseString);

                    var text = jsonObject.candidates[0].content.parts[0].text;

                    return text.ToString();
                }
                else
                {
                    throw new Exception($"Error: {response.StatusCode}");
                }
            }
        }
  }
}
