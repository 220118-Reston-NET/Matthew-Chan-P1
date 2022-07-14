using System.Data.SqlClient;
using Shop.Models;


namespace Shop.DatabaseManagement.Interfaces
{
    public interface IProfileManagementDL
    {
        /// <summary>
        /// Adds a profile to the databse
        /// </summary>
        /// <param name="p_prof"></param>
        /// <returns></returns>
        Task<ProfileDto> AddNewProfile(ProfileDto p_prof);

        /// <summary>
        /// Gets all the profiles from the database
        /// </summary>
        /// <returns></returns>
        Task<List<ProfileDto>> GetAllProfiles();

        Task<ProfileDto> GetProfileByUID(string userId);

        /// <summary>
        /// Updates a profile
        /// </summary>
        /// <param name="p_profile"></param>
        /// <returns></returns>
        Task<ProfileDto> UpdateProfile(ProfileDto p_profile);
        //Task<Profile> UpdateProfilePicture(Profile p_profile);

        Task<ApplicationUser> GetUserByUserName(string p_username);
        Task<ApplicationUser> GetUserByUserID(string p_userID);
    }
}