using Microsoft.AspNetCore.Http;

namespace DotNetCoreMultipleFileUploadWithApi.Model
{
    public class TestDocumentModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IFormFile formFiles { get; set; }
    }
}
