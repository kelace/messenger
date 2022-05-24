
using Chat.Application.ViewModel;
using Chat.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Factories
{
    public interface IInterlocutorModelFactory
    {
        public Task<InterlocutorVm> Form(User user);
    }
}
