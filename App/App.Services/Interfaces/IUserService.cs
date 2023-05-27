using App.Repositories.Helpers;
using APP.Domain.DTOs;
using APP.Domain.Entities;

namespace App.Service
{
    public interface IUserService
    {
        Task<bool> CheckMobileExist(string mobile);
        Task<ApplicationUser> GetUserByMobileOrUserName(string mobileOrUserName);
    }
}
