using Chat.Application.Account;
using Microsoft.AspNetCore.Http;

using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Infrastructure.Photo
{
    public class PhotoFormFile :  IPhotoFormFile
    {
        IFormFile _formFile;
        public PhotoFormFile(IFormFile formFile)
        {
            _formFile = formFile;
        }

        public string Name => _formFile.Name;

        public string FileName => _formFile.FileName;

        public async Task CopyToAsync(Stream target, CancellationToken cancellationToken)
        {
            await _formFile.CopyToAsync(target, cancellationToken);
        }
    }
}
