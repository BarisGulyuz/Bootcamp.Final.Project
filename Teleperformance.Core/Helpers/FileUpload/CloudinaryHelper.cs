using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teleperformance.Core.Helpers.FileUpload
{
    public class CloudinaryHelper 
    {
        Cloudinary _cloudinary;
        public CloudinaryHelper()
        {
            Account account = new Account("", "", "");
            _cloudinary = new Cloudinary(account);
        }
        public string AddPhotoAndGetUrl(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams
                    {
                        File = new FileDescription(file.Name, stream)
                    };
                    uploadResult = _cloudinary.Upload(uploadParams);
                }
                return uploadResult.Url.ToString();
            }
            return null;
        }
    }
}
