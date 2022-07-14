using Microsoft.EntityFrameworkCore;
using Shop.DatabaseManagement.Interfaces;
using Shop.Models;

namespace Shop.DatabaseManagement.Implements
{
    public class ProfileManagementDL : IProfileManagementDL
    {
        private readonly ShopContext _context;

        public ProfileManagementDL(ShopContext context)
        {
            _context = context;
        }

        public async Task<ProfileDto> AddNewProfile(ProfileDto p_profileDto)
        {
            Profile p_profile = ProfileDtoToProfile(p_profileDto);
            //p_prof.CreatedAt = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time"));
            await _context.Profiles.AddAsync(p_profile);
            await _context.SaveChangesAsync();

            return ProfileToDto(p_profile);
        }

        public async Task<List<ProfileDto>> GetAllProfiles()
        {
            List<ProfileDto> _result = await _context.Profiles
                                                    .Select(p => new ProfileDto()
                                                    {
                                                        ProfileId = p.ProfileId,
                                                        UserId = p.UserId,
                                                        FirstName = p.FirstName,
                                                        LastName = p.LastName,
                                                        Age = p.Age,
                                                        Address = p.Address
                                                    }).ToListAsync();
            if (!_result.Any())
            {
                throw new Exception("Profile DNE");
            }
            else
            {
                return _result;
            }
        }

        public async Task<ProfileDto> GetProfileByUID(string userId)
        {
            ProfileDto? _result = await _context.Profiles
                                                .Select(p => new ProfileDto()
                                                {
                                                    ProfileId = p.ProfileId,
                                                    UserId = p.UserId,
                                                    FirstName = p.FirstName,
                                                    LastName = p.LastName,
                                                    Age = p.Age,
                                                    Address = p.Address
                                                }).FirstOrDefaultAsync(p => p.UserId == userId);
            if (_result == null)
            {
                throw new Exception("Profile DNE");
            }
            else
            {
                return _result;
            }
        }

        public async Task<ProfileDto> UpdateProfile(ProfileDto p_profile)
        {
            ProfileDto? profToUpdate = await _context.Profiles
                                                .Select(p => new ProfileDto()
                                                {
                                                    ProfileId = p.ProfileId,
                                                    UserId = p.UserId,
                                                    FirstName = p.FirstName,
                                                    LastName = p.LastName,
                                                    Age = p.Age,
                                                    Address = p.Address
                                                }).FirstOrDefaultAsync(p => p.UserId == p_profile.UserId);
            if(profToUpdate != null)
            {
                profToUpdate.FirstName = p_profile.FirstName;
                profToUpdate.LastName = p_profile.LastName;
                profToUpdate.Age = p_profile.Age;
                profToUpdate.Address = p_profile.Address;
            }
            else
            {
                throw new Exception("Profile DNE");
            }
            return profToUpdate;
        }
        public async Task<ApplicationUser> GetUserByUserName(string p_username)
        {
            return await _context.Users.SingleOrDefaultAsync(c => c.UserName.Equals(p_username));
        }
        public async Task<ApplicationUser> GetUserByUserID(string p_userID)
        {
            return await _context.Users.FindAsync(p_userID);
        }

        private ProfileDto ProfileToDto(Profile p_profile)
        {
            ProfileDto _profileDto = new ProfileDto()
            {
                ProfileId = p_profile.ProfileId,
                UserId = p_profile.UserId,
                FirstName = p_profile.FirstName,
                LastName = p_profile.LastName,
                Age = p_profile.Age,
                Address = p_profile.Address
            };
            return _profileDto;
        }
        private Profile ProfileDtoToProfile(ProfileDto p_profileDto)
        {
            Profile _profile = new Profile()
            {
                ProfileId = p_profileDto.ProfileId,
                UserId = p_profileDto.UserId,
                FirstName = p_profileDto.FirstName,
                LastName = p_profileDto.LastName,
                Age = p_profileDto.Age,
                Address = p_profileDto.Address
            };
            return _profile;
        }
    }
}