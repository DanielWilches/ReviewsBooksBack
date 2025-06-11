using Books.ApplicationBusiness.Layer.Interfaces;
using Books.EnterpriseBusiness.Layer.Constants;
using Books.EnterpriseBusiness.Layer.Entitys;
using Books.EnterpriseBusiness.Layer.Enums;
using Books.EnterpriseBusiness.Layer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Xml.Linq;

namespace Books.ApplicationBusiness.Layer
{
    public class UserServices <T> where T : CustomUserProfile
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IRepository<T> _repository;
        private readonly IModelResult<T> _modelResult;

        public UserServices(UserManager<UserEntity> userManager , IRepository<T> repository, IModelResult<T> modelResult)
        {
            _userManager = userManager;
            _repository = repository;
            _modelResult = modelResult;
        }

        public async Task<ModelResult<T>> LoginUserAsync(string username, string password, string jwtKey)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    _modelResult.Code = (int)CodesResponse.Unauthorized;
                    _modelResult.Message = $"{Constants.MSG_FAILURE} User or password not found ";
                    return (ModelResult<T>)_modelResult;
                }


                var passwordValid = await _userManager.CheckPasswordAsync(user, password);
                if (!passwordValid)
                {
                    _modelResult.Code = (int)CodesResponse.Unauthorized;
                    _modelResult.Message = $"{Constants.MSG_FAILURE} User or password not found ";
                    return (ModelResult<T>)_modelResult;
                }


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

                _modelResult.Code = (int)CodesResponse.OK;
                _modelResult.Message = $"{Constants.MSG_SUCCESS} User logged in successfully";
                _modelResult.Token = tokenHandler.WriteToken(token);
            }
            catch (Exception ex )
            {
                _modelResult.Token = string.Empty;
                _modelResult.Message = $"{Constants.MSG_FAILURE} An error occurred while logging in the user";
                _modelResult.Code = (int)CodesResponse.InternalServerError;
                Console.WriteLine($"An error occurred while logging in the user: {ex.Message}");
            }
            ;
            return (ModelResult<T>)_modelResult;
        }

        public async Task<ModelResult<T>> CreateUserAsync(RegisterModel user)
        {
           _modelResult.Code = (int)CodesResponse.OK;

            try
            {

                if (user == null)
                    throw new ArgumentNullException(nameof(user));

                var newUser = new UserEntity
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Name = user.Name,
                    LastName = user?.LastName ?? string.Empty,
                    CreatedDate = DateTime.UtcNow,
                    RefreshToken = string.Empty,
                    RefreshTokenExpiryTime = DateTime.UtcNow
                };



                var  result = await _userManager.CreateAsync(newUser, user?.Password ?? string.Empty);

                if (result.Succeeded)
                {
                    await AddUserToRepository((T)new CustomUserProfile
                    {
                        IdentityUserId = newUser.Id,
                        Name = newUser.Name,
                        LastName = newUser.LastName,
                        CreatedDate = newUser.CreatedDate
                    });
                }


                _modelResult.Code = result.Succeeded ? (int)CodesResponse.OK : (int)CodesResponse.BadRequest;
                _modelResult.Message = result.Succeeded ? $"{Constants.MSG_SUCCESS} User created successfully" : $"{Constants.MSG_FAILURE} User creation failed";
            }
            catch (Exception ex )
            {
                _modelResult.Code = (int)CodesResponse.InternalServerError;

                Console.WriteLine($"An error occurred while creating the user: {ex.Message}");
            }
            
            return (ModelResult<T>)_modelResult; 
        }

        private Task AddUserToRepository(T user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));
            return _repository.AddAsync(user);
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

    }
}
