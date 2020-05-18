using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore3JWT.Controllers
{
    [Authorize(Roles = "Admin")] //This is working with SecurityTokenDescriptor
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        public async Task<IActionResult> GetAsync()
        {
            //To access parameters related to authentication from HTTP Request
            var uname = this.HttpContext.User.Identity.Name;
            var isLoggedIn = this.HttpContext.User.Identity.IsAuthenticated;
            var claims = this.HttpContext.User.Claims;
            //To access token
            var token = await HttpContext.GetTokenAsync("access_token");
           
            //var authenticationInfo = User.FindFirst("token"); 
            //foreach (var item in claims)
            //{
            //    var t = item.Properties;

            //}
            return Ok(new { 
            success ="Welcome Admin"
            });
        }
    }
}