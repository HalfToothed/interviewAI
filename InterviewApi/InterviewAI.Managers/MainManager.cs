using DocumentFormat.OpenXml.Packaging;
using InterviewAI.Infrastructure;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Reflection.PortableExecutable;
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

        public async Task<string> ExtractFromDocx(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                using (var doc = WordprocessingDocument.Open(memoryStream, false))
                {
                    var body = doc.MainDocumentPart.Document.Body;
                    string documentText = body.InnerText;
                    var result = await GenerateQuestions(documentText);
                    return result;
                }
            }
        }

        public async Task<string> ExtractFromPdf(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                memoryStream.Position = 0;
                string text = string.Empty;
                using (PdfReader reader = new PdfReader(memoryStream))
                {
                    for (int page = 1; page <= reader.NumberOfPages; page++)
                    {
                        text += PdfTextExtractor.GetTextFromPage(reader, page);
                    }
                }

                var result = await GenerateQuestions(text);
                return result;

            }
      
        }


        public async Task<string> GenerateQuestions(string request)
        { 
            var head = "please generate ten interview questions that delve into the candidate's specific skills, experiences, and achievements. Based on this : ";
            var prompt = head +"\n"+ request;
            var resut = await _service.GenerateQuestions(prompt);
            var response = resut.Candidates.FirstOrDefault(x => !string.IsNullOrEmpty(x?.Output)).Output;
            return response;
        }

    }
}