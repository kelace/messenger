using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Chat.Application.Account
{
    public interface IPhotoFormFile
    {
        public Task CopyToAsync(Stream target, CancellationToken cancellationToken);
        public string Name { get; }
        public string FileName { get; }
    }
}
