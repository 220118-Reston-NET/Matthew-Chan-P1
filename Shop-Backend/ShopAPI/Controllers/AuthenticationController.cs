using System;
using System.Collections.Generic;
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

using System.ComponentModel.DataAnnotations;

namespace ShopAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAccessTokenManager _accessTokenManager;
        private readonly IProfileManagementBL _profileBL;
        public AuthenticationController(UserManager<ApplicationUser> userManager,
                                        SignInManager<ApplicationUser> signInManager,
                                        RoleManager<IdentityRole> roleManager,
                                        IAccessTokenManager accessTokenManager,
                                        IProfileManagementBL profileBL)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _accessTokenManager = accessTokenManager;
            _profileBL = profileBL;
        }

        // POST: api/Authentication/Register
        [HttpPost(RouteConfigs.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterForm registerFrom)
        {
            if (!(await _roleManager.RoleExistsAsync("Customer")))
            {
                await _roleManager.CreateAsync(new IdentityRole("Customer"));
            }

            var newId = Guid.NewGuid().ToString();
            ApplicationUser _identity = new ApplicationUser()
            {
                Id = newId,
                UserName = registerFrom.Username,
                Email = registerFrom.Email,
                PhoneNumber = registerFrom.PhoneNumber,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(_identity, registerFrom.Password);

            if (result.Succeeded)
            {
                var userFromDB = await _userManager.FindByNameAsync(_identity.UserName);

                await _profileBL.AddNewProfile(new ProfileDto()
                {
                    ProfileId = Guid.NewGuid().ToString(),
                    UserId = userFromDB.Id,
                    FirstName = registerFrom.FirstName,
                    LastName = registerFrom.LastName
                });

                // Add default role to user("User")
                await _userManager.AddToRoleAsync(userFromDB, "Customer");

                Log.Warning("Route: " + RouteConfigs.Register);
                Log.Information("Register Sucees " + _identity.UserName);
                return Ok(new { Result = "Register Success!" });
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    stringBuilder.Append(error.Description);
                }

                Log.Warning("Route: " + RouteConfigs.Register);
                Log.Warning($"Register Fail: {stringBuilder.ToString()}");
                return BadRequest(new { Result = $"Register Fail: {stringBuilder.ToString()}" });
            }
        }

        // POST: api/Authentication/Register
        [HttpPost(RouteConfigs.RegisterManager)]
        public async Task<IActionResult> ManagerRegister([FromBody] RegisterForm registerFrom)
        {
            if (!(await _roleManager.RoleExistsAsync("Manager")))
            {
                await _roleManager.CreateAsync(new IdentityRole("Manager"));
            }

            var newId = Guid.NewGuid().ToString();
            ApplicationUser _identity = new ApplicationUser()
            {
                Id = newId,
                UserName = registerFrom.Username,
                Email = registerFrom.Email,
                PhoneNumber = registerFrom.PhoneNumber,
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(_identity, registerFrom.Password);

            if (result.Succeeded)
            {
                var userFromDB = await _userManager.FindByNameAsync(_identity.UserName);

                await _profileBL.AddNewProfile(new ProfileDto()
                {
                    ProfileId = Guid.NewGuid().ToString(),
                    UserId = userFromDB.Id,
                    FirstName = registerFrom.FirstName,
                    LastName = registerFrom.LastName
                });

                // Add default role to user("User")
                await _userManager.AddToRoleAsync(userFromDB, "Manager");

                Log.Warning("Route: " + RouteConfigs.Register);
                Log.Information("Register Sucees " + _identity.UserName);
                return Ok(new { Result = "Register Success!" });
            }
            else
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (var error in result.Errors)
                {
                    stringBuilder.Append(error.Description);
                }

                Log.Warning("Route: " + RouteConfigs.Register);
                Log.Warning($"Register Fail: {stringBuilder.ToString()}");
                return BadRequest(new { Result = $"Register Fail: {stringBuilder.ToString()}" });
            }
        }

        // POST: api/Authentication/Login
        [HttpPost(RouteConfigs.Login)]
        public async Task<IActionResult> Login([FromBody] LoginForm loginForm)
        {
            var userFromDB = await _userManager.FindByNameAsync(loginForm.Username);

            if (userFromDB == null)
            {
                Log.Warning("Route: " + RouteConfigs.Login);
                Log.Warning("Login Failed! User didn't exist in the database!");
                return BadRequest("Login Failed! User didn't exist in the database!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(userFromDB, loginForm.Password, false);

            if (!result.Succeeded)
            {
                Log.Warning("Route: " + RouteConfigs.Login);
                Log.Warning("Login Failed! Password didn't matched in the database!");
                return BadRequest("Login Failed! Password didn't matched in the database!");
            }
            var roles = await _userManager.GetRolesAsync(userFromDB);

            Log.Information("Route: " + RouteConfigs.Login);
            Log.Information("Login Succesfully!");
            return Ok(new
            {
                Result = result,
                Username = userFromDB.UserName,
                Email = userFromDB.Email,
                Token = _accessTokenManager.GenerateToken(userFromDB, roles)
            });
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