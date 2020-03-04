using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreMultipleFileUploadWithApi.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DotNetCoreMultipleFileUploadWithApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController()
        {
        }

        [HttpGet("UploadFiles")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);
            var filePath = Path.GetTempFileName();

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }
            return Ok(new { count = files.Count, size, filePath });
        }
        [HttpPost("UploadModelFiles")]
        public async Task<IActionResult> UploadModelFiles([FromForm]TestDocumentModel formFiles)
        {
            var filePath = Path.Combine(@"C:\\Users\\Rezaul Khan\\Desktop\\New folder");
            if (formFiles.formFiles != null)
            {
                long sizeOfFile = formFiles.formFiles.Length;
                if (filePath.Length > 0)
                {
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    else
                    {
                        using (var fileStream = new FileStream(Path.Combine(filePath, formFiles.formFiles.FileName), FileMode.Create))
                        {
                            await formFiles.formFiles.CopyToAsync(fileStream);
                        }
                    }
                }
                return Ok(new { sizeOfFile, filePath });
            }
            else
            {
                long fileSize = 0;
                filePath = "File note Found";
                return Ok(new { fileSize, filePath });
            }
        }
    }
}
