using FirebaseAdmin.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductNegotiations.API.Models;
using System.Security.Claims;

namespace ProductNegotiations.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet("api/Users/GetUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Get()
        {
            var data = await (FirebaseAuth.DefaultInstance.ListUsersAsync(new ListUsersOptions()).ReadPageAsync(200));
            return Ok(data);
        }

        [HttpPost("api/Users/SetAsAdmin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> SetAsAdmin([FromBody] string uid)
        {
            var claims = new Dictionary<string, object>();

            claims.Add(ClaimTypes.Role, "Admin");

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(uid, claims);

            return Ok();
        }
        [HttpPost("api/Users/RemoveRoles")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRoles([FromBody] string uid)
        {
            var claims = new Dictionary<string, object>();

            claims.Add(ClaimTypes.Role, null);

            await FirebaseAuth.DefaultInstance.SetCustomUserClaimsAsync(uid, claims);

            return Ok();
        }
    }
}
