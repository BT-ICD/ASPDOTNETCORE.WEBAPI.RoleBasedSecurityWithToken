using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore3JWT.Data;
using AspNetCore3JWT.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspNetCore3JWT.Controllers
{
    /// <summary>
    /// To access server details
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ServerController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetServer()
        {
            var result = _context.Server.Where(c => c.IsDeleted == false).ToList();
            return Ok(result);
        }
        //Joins in Entity Framework
        //Reference URL - https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/ef/language-reference/query-expression-syntax-examples-join-operators
        [HttpGet]
        public IActionResult GetServerList()
        {
            DbSet<Server> Servers = _context.Server;
            DbSet<ServerType> ServerTypes = _context.ServerType;
            var result = from serverType in ServerTypes
                         join server in Servers on serverType.ServerTypeId equals server.ServerTypeId
                         where server.IsDeleted == false
                         select new ServerDTOList
                         {
                             ServerID = server.ServerId,
                             ServerName = server.ServerName,
                             ServerTypeId = server.ServerTypeId,
                             ServerTypeName = serverType.Name,
                             Notes=server.Notes
                         };
            return Ok(result);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetServerById(int id)
        {
            var servers = _context.Server;
            var serverTypes = _context.ServerType;
            var result = (from _serverTypes in serverTypes
                         join _server in servers on _serverTypes.ServerTypeId equals _server.ServerTypeId
                         where _server.IsDeleted == false && _server.ServerId==id
                         select new ServerDTOEdit
                         {
                             ServerId =_server.ServerId??0, //To avoid null check - if ServerID is null replace with 0
                             ServerName = _server.ServerName,
                             ServerTypeId = _server.ServerTypeId,
                             ServerTypeName = _serverTypes.Name,
                             InternalIP = _server.InternalIP,
                             ExternalIP= _server.ExternalIP,
                             URLToAccess=_server.URLToAccess,
                             Notes = _server.Notes
                         }).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Add(ServerDTOAdd serverDTOAdd)
        {
            Server obj = new Server()
            {
                //ServerID = -1,
                ServerName = serverDTOAdd.ServerName,
                ServerTypeId = serverDTOAdd.ServerTypeId,
                InternalIP = serverDTOAdd.InternalIP,
                ExternalIP = serverDTOAdd.ExternalIP,
                Notes = serverDTOAdd.Notes,
                CreatedBy = "Admin",
                CreatedDateTime = DateTime.Now,
                IsDeleted = false
            };
            _context.Server.Add(obj);
            var result = _context.SaveChanges();
            var response = new { Result = result };
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Edit(ServerDTOEdit serverDTOEdit) {
            int result = 0;
            var obj = _context.Server.Where(c => c.ServerId == serverDTOEdit.ServerId).FirstOrDefault();
            if(obj == null)
            {
                return NotFound();
            }
            obj.ServerName = serverDTOEdit.ServerName;
            obj.ServerTypeId= serverDTOEdit.ServerTypeId;
            obj.InternalIP = serverDTOEdit.InternalIP;
            obj.ExternalIP= serverDTOEdit.ExternalIP;
            obj.URLToAccess= serverDTOEdit.URLToAccess;
            obj.Notes = serverDTOEdit.Notes;
            obj.LastUpdateDate = DateTime.Now;
            obj.LastUpdatedBy = "Admin";
            result = _context.SaveChanges();
            var response = new { Result = result };
            return Ok(response);
        }

        [HttpPost]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            int result = 0;
            var obj = _context.Server.Where(c => c.ServerId == id).FirstOrDefault();
            if(obj == null)
            {
                return NotFound();
            }
            obj.DeletedBy = "Admin";
            obj.IsDeleted = true;
            obj.LastUpdatedBy = "Admin";
            obj.LastUpdateDate = DateTime.Now;
            result = _context.SaveChanges();
            var resonse = new { Result = result };
            return Ok(resonse);
        }
    }
}