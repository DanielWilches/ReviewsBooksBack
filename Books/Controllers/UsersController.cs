using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using Books.ApplicationBusiness.Layer;
using Books.EnterpriseBusiness.Layer.Entitys;
using Books.EnterpriseBusiness.Layer.Models;
using Books.EnterpriseBusiness.Layer.Constants;

namespace BooksPresentation.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/user")]
    public class UsersController : ControllerBase
    {
        private readonly UserServices<CustomUserProfile> _userService;
        private readonly string _jwtKey;

        public UsersController(UserServices<CustomUserProfile> userService, IConfiguration configuration)
        {
            _userService = userService;
            _jwtKey = configuration[Constants.JWT_KEY] ?? "u7!xPz$2kL9@wQe4rT6yBvN8mC5sJ1hG2DOD#4";
        }

        /// <summary>
        /// Crea un nuevo usuario
        /// </summary>
        [HttpPost("create")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ModelResult<CustomUserProfile>>> CreateUser([FromBody] RegisterModel model)
        {
            var result = await _userService.CreateUserAsync(model);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Inicia sesión de usuario
        /// </summary>
        [HttpPost("login")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult<ModelResult<CustomUserProfile>>> Login([FromBody] LoginModel login)
        {
            var result = await _userService.LoginUserAsync(login.UserName, login.Password, _jwtKey);
            return StatusCode(result.Code, result);
        }

        /// <summary>
        /// Cierra la sesión de un usuario
        /// </summary>
        [HttpPost("logout")]
        [MapToApiVersion("1.0")]
        public async Task<ActionResult> Logout([FromBody] LoginModel login)
        {
            var result = await _userService.LogoutAsync(login.UserName);
            
            if (!result)
                return NotFound(new { mensaje = "Usuario no encontrado." });

            return Ok(new { mensaje = "Sesión cerrada correctamente." });
        }
    }
}
