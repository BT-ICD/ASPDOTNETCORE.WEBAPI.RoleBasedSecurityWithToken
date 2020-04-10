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
            int res = _context.SaveChanges();
            var objResult = new { result = res };
            return Ok(objResult);
        }
        //Access Id as Route Parameter - using attribute routing
        [HttpPost]
        [Route("{Id:int}")]
        public IActionResult Delete(int Id)
        {
            var result = 0;
            ServerType obj = _context.ServerType.Where(c => c.ServerTypeId == Id).FirstOrDefault();
            if (obj == null)
                result = 0;
            else
            {
                obj.IsDeleted = true;
                obj.DeletedDateTime = DateTime.Now;

            //  _context.ServerType.Remove(obj); //Perform soft delete instead of actual deleting record
            result = _context.SaveChanges();
            }
            var objResult = new { result = result };
            return Ok(objResult);
        }
        [HttpPost]
        public IActionResult Update(ServerTypeDTOAddNewServerType objData)
        {
            int result = 0;
            var obj = _context.ServerType.Where(c => c.ServerTypeId == objData.ServerTypeId).FirstOrDefault();
            if (obj == null)
                result = 0;
            else
            { 
            obj.CreatedBy = objData.CreatedBy;
            obj.Name = objData.Name;
           // _context.ServerType.Update(obj); //No need to update - Objects are by referen
            
                result = _context.SaveChanges();
            }
            var objResult = new { result = result };
            return Ok(objResult);
        }
        [HttpGet]
        [Route("{Id:int}")]
        public IActionResult GetServerTypeById(int Id)
        {
            var result = _context.ServerType.Where(c => c.ServerTypeId == Id).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            ServerTypeDTOAddNewServerType objResult = new ServerTypeDTOAddNewServerType();
            objResult.ServerTypeId = result.ServerTypeId;
            objResult.Name = result.Name;
            objResult.CreatedBy = result.CreatedBy;
             return Ok(objResult);
        }
    }
}