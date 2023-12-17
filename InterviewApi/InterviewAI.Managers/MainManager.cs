using InterviewAI.Infrastructure;
using System.Runtime.CompilerServices;

namespace InterviewAI.Managers
{
    public class MainManager : IMainManager
    {
        private readonly IPalmService _service;
        public MainManager(IPalmService service) 
        {
            _service = service;
        }

        public async Task<string> GenerateQuestions(string request)
        {
            
            var head = "Based on the provided resume, please generate ten interview questions with answers below them that delve into the candidate's specific skills, experiences, and achievements. Ensure the questions cover a range of competencies, reflecting the details presented in the resume and do not highlight give simple text";
            var prompt = head +"\n"+ request;
            var resut = await _service.GenerateQuestions(prompt);
            var response = resut.Candidates.FirstOrDefault(x => !string.IsNullOrEmpty(x?.Output)).Output;
            return response;
        }

    }
}