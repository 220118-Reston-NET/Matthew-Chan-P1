using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Shop.Models;

namespace Shop.ShopAPI.AuthenticationService.Interfaces
{
    public interface IAccessTokenManager
    {
        string GenerateToken(ApplicationUser user, IList<string> roles);
        Task<bool> IsCurrentActiveToken();
        Task<bool> IsActiveAsync(string token);
    }
}