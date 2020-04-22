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
    public class ProjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ProjectController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpGet]
        public IActionResult List()
        {
            var result = _context.Project.Where(c=>c.IsDeleted==false) .ToList();
            return Ok(result);
        }
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetProjectById(int id)
        {
            var result = _context.Project.Where(c => c.IsDeleted == false && c.ProjectId == id).FirstOrDefault();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpPost]
        public IActionResult Add(ProjectDTOAdd projectDTOAdd)
        {
            Project obj = new Project()
            {
                //ProjectId = -1,
                Name = projectDTOAdd.Name,
                About = projectDTOAdd.About,
                Notes = projectDTOAdd.Notes,
                SourceCodeLocation = projectDTOAdd.SourceCodeLocation,
                CreatedBy = "Admin",
                CreatedDateTime = DateTime.Now,
                IsDeleted = false
            };
            _context.Project.Add(obj);
            int result = _context.SaveChanges();
            var response = new { Result = result };
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Edit(ProjectDTOEdit projectDTOEdit)
        {
            int result = 0;
            var obj = _context.Project.Where(c => c.IsDeleted == false && c.ProjectId == projectDTOEdit.ProjectId).FirstOrDefault();
            if (obj == null)
            {
                return NotFound();
            }
            obj.Name = projectDTOEdit.Name;
            obj.About = projectDTOEdit.About;
            obj.Notes = projectDTOEdit.Notes;
            obj.SourceCodeLocation = projectDTOEdit.SourceCodeLocation;
            obj.LastUpdatedBy = "Admin";
            obj.LastUpdateDate = DateTime.Now;
            result = _context.SaveChanges();
            var response = new { Result = result };
            return Ok(response);
        }
        [HttpPost]
        [Route("{id:int}")]
        public IActionResult Delete(int id)
        {
            int result = 0;
            var obj = _context.Project.Where(c => c.IsDeleted == false && c.ProjectId == id).FirstOrDefault();
            if (obj == null)
                return NotFound();
            obj.IsDeleted = true;
            obj.DeletedBy = "Admin";
            obj.DeletedDateTime = DateTime.Now;
            result = _context.SaveChanges();
            var response = new { Result = result };
            return Ok(response);
        }
    }
}