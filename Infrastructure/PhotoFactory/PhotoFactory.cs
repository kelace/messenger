using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Chat.Application.Account.Settings;
using System.Threading;
using Chat.Application.Account;

namespace Chat.Infrastructure.Photo
{
    public class PhotoFactory : IPhotoFactory
    {
        IWebHostEnvironment _webHostEnvironment;
        public PhotoFactory(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<string> CreatePhoto(IPhotoFormFile Photo)
        {
            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

            var fileName = Guid.NewGuid().ToString() + Photo.FileName;

            var path = Path.Combine(uploadsFolder, fileName);

            await Photo.CopyToAsync(new FileStream(path, FileMode.Create), CancellationToken.None);

            return fileName;
        }
    }
}
