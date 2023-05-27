using App.Repositories;
using App.Repositories.Helpers;
using App.Service;
using APP.Domain.DTOs;
using APP.Domain.Entities;

namespace App.Service
{

    public class UserService : IUserService
    {
        private IUserRepository _UserRepository;

        public UserService(IUserRepository UserRepository)
        {
            _UserRepository = UserRepository;
        }
        public async Task<bool> CheckMobileExist(string mobile)
        {

            return await _UserRepository.CheckMobileExist(mobile);

        } 
        public async Task<ApplicationUser> GetUserByMobileOrUserName(string mobile)
        {
           
            return await _UserRepository.GetUserByMobileOrUserName(mobile);

        }
    }
}
