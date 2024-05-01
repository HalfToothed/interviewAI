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
                    var result = await GenerateQuestions(documentText);
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

                            var response = await GenerateQuestions(extractedText);
                            return response;
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


        public async Task<string> GenerateQuestions(string request)
        {
            var prompt = request  + "\n \n" + "this is a resume ask detailed interview questions only based on this resume";
            var response = await _service.GenerateContent(prompt);
            return response;
        }

    }
}