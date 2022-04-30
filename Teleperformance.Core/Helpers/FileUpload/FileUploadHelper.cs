using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleperformance.Core.Helpers.FileUpload
{
    public class FileUploadHelper
    {
    
        public async Task<string> ImageUpload(IFormFile formFile, string name)
        {
            var pictureFile = formFile.FileName;
            string wwwroot = "~wwwroot/";
            DateTime dateTime = DateTime.Now;

            string fileExtension = Path.GetExtension(pictureFile);
            string fileName = $"{name}_{dateTime.ToShortDateString()}{fileExtension}";
            var path = Path.Combine($"{wwwroot}/images", fileName);
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await formFile.CopyToAsync(stream);
            }
            return fileName;
        }
    }
}
