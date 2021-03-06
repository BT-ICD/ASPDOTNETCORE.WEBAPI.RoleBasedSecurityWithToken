﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore3JWT.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore3JWT.Controllers
{
    [Authorize(Roles="User")] //This is working with SecurityTokenDescriptor
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SecureController : ControllerBase
    {
        public IActionResult Get()
        {
            var res = Product.GetProducts();
            return Ok(res.ToList());
        }
    }
}