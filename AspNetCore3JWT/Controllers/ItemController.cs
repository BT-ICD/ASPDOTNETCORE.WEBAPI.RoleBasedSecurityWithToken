using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public IActionResult Get()
        {
            return Ok(new { 
            success ="Welcome Admin"
            });
        }
    }
}