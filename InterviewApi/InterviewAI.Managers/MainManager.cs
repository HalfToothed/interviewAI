using DocumentFormat.OpenXml.Packaging;
using InterviewAI.Infrastructure;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Http;
using System.Text;

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
                    var result = await GenerateQuestions(memoryStream);
                    return result;
                }
            }
        }

        public async Task<string> ExtractFromPdf(IFormFile file)
        {
            if (file != null && file.ContentType.ToLower() == "application/pdf")
            {
                try
                {
                    using (var reader = new PdfReader(file.OpenReadStream()))
                    {
                        using (var document = new PdfDocument(reader))
                        {
                            var text = new StringBuilder();

                            for (int page = 1; page <= document.GetNumberOfPages(); page++)
                            {   
                                text.Append(PdfTextExtractor.GetTextFromPage(document.GetPage(page)));
                            }
                            string extractedText = text.ToString();
                          
                            return extractedText;
                        }
                       
                    }
                }
                catch (Exception ex)
                {
                    return "Error occurred while reading PDF";
                }
            }
            else
            {
                return "Invalid file. Please provide a PDF file.";
            }
        }


        public async Task<string> GenerateQuestions(MemoryStream request)
        { 
            var head = "please Extract Usefult Information of the Candidate about Education, Experience, Work, Technology from this resume: ";
            var prompt = head +"\n"+ request;
            var resut = await _service.GenerateQuestions(prompt);
            var response = resut.Candidates.FirstOrDefault(x => !string.IsNullOrEmpty(x?.Output)).Output;
            return response;
        }

    }
}