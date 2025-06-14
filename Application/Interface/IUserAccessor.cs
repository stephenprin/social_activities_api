using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace Application.Interface
{
    public interface IUserAccessor
    {
        string GetUserId();
        Task<User> GetUserAsync();
    }
}
