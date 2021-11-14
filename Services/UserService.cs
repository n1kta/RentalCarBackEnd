using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using RentalCar.Contracts;
using RentalCar.Dtos;
using RentalCar.Entity;
using RentalCar.Enums;
using RentalCar.Models;
using RentalCar.Repository;

namespace RentalCar.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepositoryAsync _baseRepositoryAsync;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JWT _jwt;

        public UserService(IBaseRepositoryAsync baseRepositoryAsync,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptions<JWT> jwt,
            IHttpContextAccessor httpContextAccessor)
        {
            _baseRepositoryAsync = baseRepositoryAsync;
            _userManager = userManager;
            _signInManager = signInManager;
            _jwt = jwt.Value;

            UserId = httpContextAccessor.HttpContext?.User?.FindFirstValue("uid");
        }

        public string UserId { get; set; }

        public async Task<ResponseModel> RegisterAsync(RegisterDto dto)
        {
            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = $"{dto.FirstName}{dto.LastName}"
            };

            var userWithSameEmail = await _userManager.FindByEmailAsync(dto.Email);

            if (userWithSameEmail == null)
            {
                var result = await _userManager.CreateAsync(user, dto.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, nameof(Role.Customer));
                    var createdUser = await _userManager.GetUserIdAsync(user);

                    var customer = new Customer
                    {
                        FirstName = dto.FirstName,
                        LastName = dto.LastName,
                        Address = dto.Address,
                        Phone = dto.Phone,
                        ApplicationUserId = createdUser,
                        CreatedDate = DateTime.Now
                    };

                    await _baseRepositoryAsync.Create(customer);

                    return new ResponseModel
                    {
                        IsSuccess = true,
                        Message = $"User Registered with username {user.UserName}"
                    };
                }
                else
                {
                    return new ResponseModel
                    {
                        IsSuccess = false,
                        Message = "User Error"
                    };
                }
            }
            else
            {
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "User Error"
                };
            }
        }

        public async Task<AuthenticationDto> GetTokenAsync(TokenDto model)
        {
            var authenticationModel = new AuthenticationDto();
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                authenticationModel.IsAuthenticated = false;
                authenticationModel.Message = $"No Accounts Registered with {model.Email}.";
                return authenticationModel;
            }
            if (await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, lockoutOnFailure: false);
                authenticationModel.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = await CreateJwtToken(user);
                authenticationModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                authenticationModel.Email = user.Email;
                authenticationModel.UserName = user.UserName;
                var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
                authenticationModel.Roles = rolesList.FirstOrDefault();
                return authenticationModel;
            }
            authenticationModel.IsAuthenticated = false;
            authenticationModel.Message = $"Incorrect Credentials for user {user.Email}.";
            return authenticationModel;
        }

        public async Task<CustomerDto> GetCurrentCustomer(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var userId = handler.ReadJwtToken(token).Claims.FirstOrDefault(x => x.Type == "uid")?.Value;
            var customer = (await _baseRepositoryAsync.GetWithIncludeAsync<Customer>(x => x.ApplicationUserId == userId, m => m.ApplicationUser)).FirstOrDefault();
            if (customer != null)
            {
                var result = new CustomerDto
                {
                    Id = customer.Id,
                    Email = customer.ApplicationUser.Email,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    Address = customer.Address,
                    Phone = customer.Phone
                };

                return result;
            }
            else
            {
                throw new Exception("No such customer");
            }
        }

        public async Task<bool> IsAdmin(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var userId = handler.ReadJwtToken(token).Claims.FirstOrDefault(x => x.Type == "uid")?.Value;
            var appUser = (await _baseRepositoryAsync.Fetch<ApplicationUser>(x => x.Id == userId)).FirstOrDefault();
            var user = await _userManager.GetRolesAsync(appUser);

            return user.Contains("Admin");
        }

        public async Task UpdateCustomer(int customerId, CustomerUpdateDto dto)
        {

            var customer =
                (await _baseRepositoryAsync.GetWithIncludeAsync<Customer>(x => x.Id == customerId,
                    m => m.ApplicationUser)).FirstOrDefault();
            if (customer != null)
            {
                customer.ApplicationUser.Email = dto.Email;
                customer.FirstName = dto.FirstName;
                customer.LastName = dto.LastName;
                customer.Address = dto.Address;
                customer.Phone = dto.Phone;
                if (!string.IsNullOrWhiteSpace(dto.Password))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(customer.ApplicationUser);
                    await _userManager.ResetPasswordAsync(customer.ApplicationUser, token, dto.Password);
                }
                _baseRepositoryAsync.Update(customer);
                await _userManager.UpdateAsync(customer.ApplicationUser);
            }
        }

        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("uid", user.Id)
                }
                .Union(userClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials);
            return jwtSecurityToken;
        }
    }
}
