#region using directives
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
#endregion

#region Project Usings
using Books.Application.Layer.DTOs;
using Books.Domain.Layer.Entitys;
using Books.Domain.Layer.Enums;
using Books.Domain.Layer.Interfaces;
using Books.Domain.Layer.Constants;
#endregion


namespace Books.Application.Layer.Services
{
    public class UserServices <T> where T : CustomUserProfile
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IRepository<T> _repository;
        private readonly IResultDto<T> _ResultDto;

        public UserServices(UserManager<UserEntity> userManager , IRepository<T> repository, IResultDto<T> ResultDto)
        {
            _userManager = userManager;
            _repository = repository;
            _ResultDto = ResultDto;
        }

        public async Task<ResultDto<T>> LoginUserAsync(string username, string password, string jwtKey)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username);
                if (user == null)
                {
                    _ResultDto.Code = (int)CodesResponse.Unauthorized;
                    _ResultDto.Message = $"{Constants.MSG_FAILURE} User or password not found ";
                    return (ResultDto<T>)_ResultDto;
                }


                var passwordValid = await _userManager.CheckPasswordAsync(user, password);
                if (!passwordValid)
                {
                    _ResultDto.Code = (int)CodesResponse.Unauthorized;
                    _ResultDto.Message = $"{Constants.MSG_FAILURE} User or password not found ";
                    return (ResultDto<T>)_ResultDto;
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
                var CustomUuser = await _repository.GetListAsync(r => r.IdentityUserId == user.Id);

                _ResultDto.Code = (int)CodesResponse.OK;
                _ResultDto.Data = CustomUuser.ToList();
                _ResultDto.Message = $"{Constants.MSG_SUCCESS} User logged in successfully";
                _ResultDto.Token = tokenHandler.WriteToken(token);
            }
            catch (Exception ex )
            {
                _ResultDto.Token = string.Empty;
                _ResultDto.Message = $"{Constants.MSG_FAILURE} An error occurred while logging in the user";
                _ResultDto.Code = (int)CodesResponse.InternalServerError;
                Console.WriteLine($"An error occurred while logging in the user: {ex.Message}");
            }
            ;
            return (ResultDto<T>)_ResultDto;
        }

        public async Task<ResultDto<T>> CreateUserAsync(RegisterDto user)
        {
           _ResultDto.Code = (int)CodesResponse.OK;

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


                _ResultDto.Code = result.Succeeded ? (int)CodesResponse.OK : (int)CodesResponse.BadRequest;
                _ResultDto.Message = result.Succeeded ? $"{Constants.MSG_SUCCESS} User created successfully" : $"{Constants.MSG_FAILURE} User creation failed";
            }
            catch (Exception ex )
            {
                _ResultDto.Code = (int)CodesResponse.InternalServerError;

                Console.WriteLine($"An error occurred while creating the user: {ex.Message}");
            }
            
            return (ResultDto<T>)_ResultDto; 
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
