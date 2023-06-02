using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SignalRServer.auth;
using SignalRServer.Models;
using System.Runtime.Intrinsics.X86;

namespace SignalRServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : Controller
    {
        private readonly JwtAuthenticationManager _jwtAuthenticationManager;
        private static readonly string[] User = new[]
{
        "Ammy", "json", "ken", "denny"
    };

        public LoginController(JwtAuthenticationManager jwtAuthenticationManager)
        {
            this._jwtAuthenticationManager = jwtAuthenticationManager;

        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(User user)
        {

            if (User.Contains(user.UserId))
            {
                string token = this._jwtAuthenticationManager.Authenticate(user);
                var vResult = new
                {
                    token = token,
                    userInfo = user
                };
                return Ok(vResult);
            }
            return NotFound();
        }
    }
}
