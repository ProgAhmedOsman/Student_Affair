using APP.Domain.Entities;
using App.Repositories;
using APP.Domain.DTOs;
using App.Repositories.Helpers;
 
namespace App.Repositories
{
    public interface IUserRepository 
    {
        Task<bool> CheckMobileExist(string Mobile);
        Task<ApplicationUser> GetUserByMobileOrUserName(string mobileOrUserName);
    }
}
