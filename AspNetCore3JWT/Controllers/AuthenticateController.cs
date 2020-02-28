using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNetCore3JWT.Data;
using AspNetCore3JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
/// <summary>
/// Learning Reference - https://blog.pedrofelix.org/2012/11/27/json-web-tokens-and-the-new-jwtsecuritytokenhandler-class/
/// https://vosseburchttechblog.azurewebsites.net/index.php/2015/09/19/generating-and-consuming-json-web-tokens-with-net/
/// https://blog.pedrofelix.org/2015/01/18/jwt-and-jose-specifications-approved-for-publication-as-rfcs/
/// </summary>
namespace AspNetCore3JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        private UserManager<ApplicationUser> userManager;

        public AuthenticateController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }


        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {

                //To get list of roles for current user
                var userRoles = await userManager.GetRolesAsync(user);
                var cnt = userRoles.Count;
                var tokenHandler = new JwtSecurityTokenHandler();
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aaaaaaaaaaaaaaaa"));
                var roleName = userRoles[0];
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.UserName),
                            new Claim(ClaimTypes.Role, roleName)
                            
                        }
                        ),
                    Issuer = "Self",
                    Expires = DateTime.Now.AddHours(3),
                    SigningCredentials= new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)

                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new
                {
                    token = tokenString,
                    expiration = token.ValidTo,
                    roles = roleName
                }); 
            }
            return Unauthorized();
        }

        //Alternate way to generate token - without Role
        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> Login([FromBody] LoginModel model)
        //{

        //    var user = await userManager.FindByNameAsync(model.Username);
        //    if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
        //    {

        //        var authClaims = new[]
        //        {
        //            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
        //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //        };
        //        //Changed Key To solve error- System.ArgumentOutOfRangeException: IDX10603: Decryption failed. Keys tried: '[PII is hidden 
        //        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("aaaaaaaaaaaaaaaa"));

        //        //Using JwtSecurityToken
        //        var token = new JwtSecurityToken(
        //            //issuer: "http://krishnasoftech.com",
        //            //audience: "http://krishnasoftech.com",

        //            expires: DateTime.Now.AddHours(3),
        //            claims: authClaims,

        //            signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        //            );

        //        return Ok(new
        //        {
        //            token = new JwtSecurityTokenHandler().WriteToken(token),
        //            expiration = token.ValidTo,
        //            roles = "Admin"
        //        }); ;
        //    }
        //    return Unauthorized();
        //}
    }
}