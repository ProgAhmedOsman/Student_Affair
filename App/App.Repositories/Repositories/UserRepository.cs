using App.Common.Enums;
using App.Repositories;
using App.Repositories.Helpers;
using APP.Domain.DTOs;
using APP.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CheckMobileExist(string mobile)
        {
            return await _context.Set<ApplicationUser>().AnyAsync(c => c.Mobile == mobile);

        }
        public async Task<ApplicationUser> GetUserByMobileOrUserName(string mobileOrUserName)
        {
            return await _context.Set<ApplicationUser>().FirstOrDefaultAsync(c => c.Mobile == mobileOrUserName || c.UserName == mobileOrUserName);

        }


    }



}
