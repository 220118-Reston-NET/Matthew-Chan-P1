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

/*
    created using:
    dotnet aspnet-codegenerator controller -name Profile -api -outDir Controllers -actions

    Auttogeneratie by utilizing aspnet-codegenrator tool
    https://docs.mo

    -To start
    --intsall tool first - dotnet tool install -g dotnet-sapnet-coegenerator
    --add package to api project - dotnet add package Microsoft.VisualStudio.WebCodeGeneration.Design

    -To Create a controller
    dotnet aspnet-codegenerator controller -name Profile -api -outDir Controllers -actions

    "dotnet aspent-codegenerator controller" - creates a controller

    "-name {NameOfController}" - names the controller to hwatever you put

    :-api" maeks the controller restful style api

    "_outDir Controllers - put controller insie controller folder in api prjcet
    "-action" - adds in action(methods) in your controller
*/



namespace ShopApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IProfileManagementBL _profileBL;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string? given_name;
        public CustomerController(IProfileManagementBL p_profileBL,
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
            string p_userID = userFromDB.Id;

            try
            {
                Log.Information("Getting profile information");
                return Ok(await _profileBL.GetUserProfile(p_userID));
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Profile + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

        // PUT: api/Customer/Profile
        [Authorize(Roles = "Customer")]
        [HttpPut(RouteConfigs.Profile)]
        public async Task<IActionResult> UpdateProfile([FromBody] ProfileDto p_profile)
        {
            var userFromDB = await _userManager.FindByNameAsync(given_name);
            string p_userID = userFromDB.Id;

            try
            {
                ProfileDto _updatedProfile = new ProfileDto()
                {
                    UserId = p_userID,
                    FirstName = p_profile.FirstName,
                    LastName = p_profile.LastName,
                    Age = p_profile.Age,
                    Address = p_profile.Address
                };
                await _profileBL.UpdateProfile(_updatedProfile);
                Log.Information("Profile successfully updated for " + p_profile.FirstName + " " + p_profile.LastName);
                return Ok("Profile Updated");
            }
            catch (System.Exception e)
            {
                Log.Warning("Route:" + RouteConfigs.Profile + ": " + e.Message);
                return NotFound(e.Message);
            }
        }

    }
}
