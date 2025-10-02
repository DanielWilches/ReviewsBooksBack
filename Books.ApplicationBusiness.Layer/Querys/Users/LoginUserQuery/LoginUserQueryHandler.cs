using MediatR;
using Books.Application.Layer.DTOs;
using Books.Domain.Layer.Entitys;
using Books.Application.Layer.Services;
using System.Threading;
using System.Threading.Tasks;

namespace Books.Application.Layer.Querys.Users.LoginUserQuery
{
    public class LoginUserCommand : IRequest<ResultDto<CustomUserProfile>>
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string JwtKey { get; set; }
        public LoginUserCommand(string userName, string password, string jwtKey)
        {
            UserName = userName;
            Password = password;
            JwtKey = jwtKey;
        }
        public LoginUserCommand() { }
    }

    public class LoginUserQueryHandler : IRequestHandler<LoginUserCommand, ResultDto<CustomUserProfile>>
    {
        private readonly UserServices<CustomUserProfile> _userService;
        public LoginUserQueryHandler(UserServices<CustomUserProfile> userService)
        {
            _userService = userService;
        }
        public async Task<ResultDto<CustomUserProfile>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await _userService.LoginUserAsync(request.UserName, request.Password, request.JwtKey);
        }
    }
}
