using Shop.Models;

namespace Shop.BuisnessManagement.Interfaces
{
    public interface IProfileManagementBL
    {
        Task<ProfileDto> AddNewProfile( ProfileDto p_profile );
        Task<ProfileDto> GetUserProfile( string p_userID );
        Task<ProfileDto> UpdateProfile( ProfileDto p_profile );
        Task<ApplicationUser> GetUserByUserID(string p_userID);
        Task<ApplicationUser> GetUserByUserName(string p_username);
    }
}