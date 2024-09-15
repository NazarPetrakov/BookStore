using BookStore.Application.Abstract.Services;
using BookStore.Application.Common.Identity;
using BookStore.Application.Contracts.Authentication;
using BookStore.Domain.Exceptions;
using BookStore.Domain.Models.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookStore.Application.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public AuthenticateService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                throw new EntityNotFoundException("User not found");

            return user;
        }
        public async Task<LoginResponse> LoginAsync(LoginModel model)
        {
            var user = await _userManager.FindByNameAsync(model.Username);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApplicationSettings:JWT_Secret"]));

                var jwtToken = new JwtSecurityToken(
                    claims: authClaims,
                    notBefore: DateTime.UtcNow,
                    expires: DateTime.UtcNow.AddDays(30),
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

                return new LoginResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                    Expiration = jwtToken.ValidTo
                };
            }
            return new LoginResponse { Status = "Error", Message = "Username or Password is wrong" };
        }

        public async Task<AuthResponse> RegisterUserAsync(RegisterModel model, bool isAdmin = false)
        {
            var userExists = await _userManager.FindByNameAsync(model.Username);
            if (userExists != null)
            {
                return new AuthResponse { Status = "Error", Message = "User already exists!" };
            }

            ApplicationUser user = new ApplicationUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username,
                FullName = model.Name + " " + model.Surname
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return new AuthResponse
                {
                    Status = "Error",
                    Message = "User creation failed!",
                    Errors = errors
                };
            }

            if (isAdmin)
            {
                await EnsureRoleExistsAsync(UserRoles.Admin);

                await _userManager.AddToRoleAsync(user, UserRoles.Admin);
            }
            else
            {
                await EnsureRoleExistsAsync(UserRoles.User);

                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }

            return new AuthResponse { Status = "Success", Message = "User created successfully!" };
        }
        private async Task EnsureRoleExistsAsync(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
