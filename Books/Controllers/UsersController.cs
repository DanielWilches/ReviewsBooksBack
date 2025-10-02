using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Books.Application.Layer.DTOs;
using Books.Domain.Layer.Entitys;
using MediatR;
using Books.Domain.Layer.Constants;
using Books.Application.Layer.Command.Users.CreateUser;
using Books.Application.Layer.Querys.Users.LoginUserQuery;
using Books.Application.Layer.Querys.Users.LogoutUser;

namespace BooksPresentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/user")]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly string _jwtKey;

        public UsersController(ISender sender, IConfiguration configuration)
        {
            _sender = sender;
            _jwtKey = configuration[Constants.JWT_KEY] ?? "u7!xPz$2kL9@wQe4rT6yBvN8mC5sJ1hG2DOD#4";
        }

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ResultDto<CustomUserProfile>>> CreateUser([FromBody] RegisterDto model)
        {
            var result = await _sender.Send(new CreateUserCommand(model));
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Inicia sesión de usuario
        /// </summary>
        [HttpPost("login")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ResultDto<CustomUserProfile>>> Login([FromBody] LoginDto login)
        {
            var result = await _sender.Send(new LoginUserCommand(login.UserName, login.Password, _jwtKey));
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Cierra la sesión de un usuario
        /// </summary>
        [HttpPost("logout")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Logout([FromBody] LoginDto login)
        {
            var result = await _sender.Send(new LogoutUserCommand(login.UserName));
            if (!result)
                return NotFound(new { mensaje = "Usuario no encontrado." });
            return Ok(new { mensaje = "Sesión cerrada correctamente." });
        }
    }
}
