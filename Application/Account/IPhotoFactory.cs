using Chat.Application.Account.Settings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Account
{
    public interface IPhotoFactory
    {
        public Task<string> CreatePhoto(IPhotoFormFile photo);
    }
}
