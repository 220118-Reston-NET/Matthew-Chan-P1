using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Shop.ShopAPI.AuthenticationService.Interfaces;
using Shop.ShopAPI.Consts;
using Shop.ShopAPI.DataTransferObjects;
using Shop.BuisnessManagement.Interfaces;
using Shop.Models;

namespace ShopAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IProfileManagementBL _profileBL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string? given_name;
        public UserController(IProfileManagementBL p_profileBL,
                                IHttpContextAccessor httpContextAccessor,
                                UserManager<ApplicationUser> userManager)
        {
            _profileBL = p_profileBL;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;

            var token = _httpContextAccessor.HttpContext.Request.Headers["authorization"].Single().Split(" ").Last();
            var tokenHandler = new JwtSecurityTokenHandler();
            given_name = tokenHandler.ReadJwtToken(token).Payload["given_name"].ToString();
        }

        // GET: api/User/Profile
        [Authorize(Roles = "Customer")]
        [HttpGet(RouteConfigs.Profile)]
        public async Task<IActionResult> GetProfile()
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            if(userFromDB == null)
            {

                return BadRequest("Profile could not be retrieved. User could not be Found");
            }
            string p_userID = userFromDB.Id;
            

            try
            {
                Log.Information("Getting " + given_name + "'s profile information");
                return Ok(await _profileBL.GetUserProfile(p_userID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Profile + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

        /*
        [HttpPost(RouteConfigs.Update)]
        public async Task<IActionResult> Update(string id, string role)
        {
            var userFromDB = await _userManager.FindByIdAsync(id);

            if (userFromDB == null)
            {
                Log.Warning("Route: " + RouteConfigs.Update);
                Log.Warning("Role Update Failed! UserId didn't exist in the database!");
                return BadRequest("Role Update Failed! UserId didn't exist in the database!");
            }

            await _userManager.AddToRoleAsync(userFromDB, role);
            
            var roles = await _userManager.GetRolesAsync(userFromDB);

            if (role != roles.ToString() )
            {
                Log.Warning("Route: " + RouteConfigs.Update);
                Log.Warning("Role Update Failed! UserId didn't exist in the database!");
                return BadRequest("Role Update Failed! UserId didn't exist in the database!");
            }

            Log.Information("Route: " + RouteConfigs.Update);
            Log.Information("Added User Role Succesfully!");
            Console.WriteLine(roles );
            return Ok(new
            {
                Username = userFromDB.UserName,
                Email = userFromDB.Email,
                Token = _accessTokenManager.GenerateToken(userFromDB, roles)
            });
        } */

    }
}
