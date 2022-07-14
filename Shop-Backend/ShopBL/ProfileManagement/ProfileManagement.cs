using Shop.Models;
using Shop.BuisnessManagement.Interfaces;
using Shop.DatabaseManagement.Interfaces;

namespace Shop.BuisnessManagement.Implements
{
    public class ProfileManagementBL : IProfileManagementBL
    {
        private readonly IProfileManagementDL _repo;
        //dependency injection
        public ProfileManagementBL(IProfileManagementDL repo)
        {
            _repo = repo;
        }

        public async Task<ProfileDto> AddNewProfile(ProfileDto p_profile)
        {
            // no need to try catch because people can have the exact same info in their profile and it's ok
            return await _repo.AddNewProfile(p_profile);
        }
        public async Task<ProfileDto> GetUserProfile(string p_userId)
        {
            try
            {
                return await _repo.GetProfileByUID(p_userId);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
        }

        public async Task<ProfileDto> UpdateProfile(ProfileDto p_profile)
        {
            try
            {
                return await _repo.UpdateProfile(p_profile);
            }
            catch (System.Exception exe)
            {
                throw new Exception(exe.Message);
            }
            
        }

        public async Task<ApplicationUser> GetUserByUserID(string p_userID)
        {
            return await _repo.GetUserByUserID(p_userID);
        }

        public async Task<ApplicationUser> GetUserByUserName(string p_username)
        {
            return await _repo.GetUserByUserName(p_username);
        }
    }
}