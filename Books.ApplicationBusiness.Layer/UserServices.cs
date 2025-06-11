using Books.EnterpriseBusiness.Layer.Entitys;
using Books.EnterpriseBusiness.Layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Books.ApplicationBusiness.Layer
{
    public class UserServices <T> where T : UserEntity
    {
        private readonly UserManager<UserEntity> _userManager;

        public UserServices(UserManager<UserEntity> userManager)
        {
            _userManager = userManager;
        }

        public async Task<string?> LoginUserAsync(string username, string password, string jwtKey)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return null;

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
                return null;

            // Generate JWT token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(jwtKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name,  user.Name)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };  
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public async Task<IdentityResult> CreateUserAsync(RegisterModel model)
        {
            var user = new UserEntity
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                LastName = model?.LastName ?? string.Empty,
                CreatedDate = DateTime.UtcNow,
                RefreshToken = string.Empty,
                RefreshTokenExpiryTime = DateTime.UtcNow
            };
            return await _userManager.CreateAsync(user, model?.Password ?? string.Empty);
        }
     
        public async Task<bool> LogoutAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
                return false;

            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;
            await _userManager.UpdateAsync(user);
            return true;
        }

       
        public void SingUpUser(){}
        public void RecoverUser(){}
    }
}
