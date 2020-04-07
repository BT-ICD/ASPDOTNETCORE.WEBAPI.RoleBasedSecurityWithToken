using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore3JWT.Data;
using AspNetCore3JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore3JWT.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServerTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ServerTypeController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult GetServerTypes()
        {
            var result = _context.ServerType.ToList();
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Add(ServerTypeDTOAddNewServerType serverTypeDTOAddNewServerType)
        {
            ServerType obj = new ServerType();
            obj.ServerTypeId = serverTypeDTOAddNewServerType.ServerTypeId;
            obj.Name = serverTypeDTOAddNewServerType.Name;
            obj.CreatedBy = serverTypeDTOAddNewServerType.CreatedBy;
            obj.CreatedDateTime = DateTime.Now;
            obj.DeletedBy = "";
            obj.DeletedDateTime = null;
            obj.IsDeleted = false;
            _context.ServerType.Add(obj);
            _context.SaveChanges();
            return Ok("Success");
        }
    }
}