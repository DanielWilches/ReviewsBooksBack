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

        //public  async Task  Authenticate(string username, string PasswordHash, string jwtKey)
        //{
        //
        // 
        //    
        //    return "tokenString";
        //}
        public async Task<IdentityResult> CreateUserAsync(RegisterModel model)
        {
            var user = new UserEntity
            {
                UserName = model.UserName,
                Email = model.Email,
                Name = model.Name,
                LastName = model.LastName,
                CreatedDate = DateTime.UtcNow
            };
            return await _userManager.CreateAsync(user, model.Password);
        }

        public void LoginUser(string username, string password)
        {
            // Logic to login a user
            Console.WriteLine($"User {username} logged in successfully.");
        }


        public void LogOut() { }
        public void SingUpUser(){}
        public void RecoverUser(){}

    }
}
