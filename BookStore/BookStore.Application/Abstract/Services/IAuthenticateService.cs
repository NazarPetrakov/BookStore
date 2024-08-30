using BookStore.Application.Contracts.Authentication;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace BookStore.Application.Abstract.Services
{
    public interface IAuthenticateService
    {
        Task<LoginResponse> LoginAsync(LoginModel model);
        Task<AuthResponse> RegisterUserAsync(RegisterModel model, bool isAdmin = false);
    }
}
