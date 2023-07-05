using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForestrySystem.Services
{
    public  class ImagesService
    {
        private readonly Microsoft.Extensions.Hosting.IHostingEnvironment environment;

        public ImagesService(Microsoft.Extensions.Hosting.IHostingEnvironment environment)
        {
            this.environment = environment;
        }
        public async Task<string> UploadFileAsync(IFormFile file)
        {
            var filePath = Path.Combine(environment.ContentRootPath, @"wwwroot\images", file.FileName);
            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);
            return filePath;
        }
    }
}
