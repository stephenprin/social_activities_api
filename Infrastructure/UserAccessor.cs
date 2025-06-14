using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.Interface;
using Domain;
using Microsoft.AspNetCore.Http;
using Persistence;

namespace Infrastructure
{
    public class UserAccessor(IHttpContextAccessor httpContextAccessor, AppDbContext dbContext) : IUserAccessor
    {
        public async Task<User> GetUserAsync()
        {
           return await dbContext.Users.FindAsync(GetUserId()) ?? throw new UnauthorizedAccessException("No User log in");  
        }

        public string GetUserId()
        {
           return httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? throw new Exception("User not found");
        }
    }
}
