using BookStore.Application.Contracts.Authentication;
using BookStore.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace BookStore.Application.Abstract.Services
{
    public interface IAuthenticateService
    {
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<LoginResponse> LoginAsync(LoginModel model);
        Task<AuthResponse> RegisterUserAsync(RegisterModel model, bool isAdmin = false);
    }
}
