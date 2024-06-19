using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GradesMaterAPI.DB;
using GradesMaterAPI.DB.DbModels;
using GradesMaterAPI.ApiModel;
using GradesMaterAPI.Services;
using System.Composition;

namespace GradesMaterAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly GradeMasterDbContext _context;
        private ICsvLoader _csvLoader;
        private IExport<Teacher> _export;

        // dependecy Injection - is the ICsvLoader loader
        public TeachersController(GradeMasterDbContext context, ICsvLoader loader, IExport<Teacher> export)
        {
            _context = context;
            _csvLoader = loader;
            _export = export;
        }

        // GET: api/Teachers/sorted
        [HttpGet("sorted")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachersSortedBy(string sortedBy="")
        {
            return await _context.Teachers.OrderBy(t => t.LastName).ToListAsync();
        }

        // GET: api/Teachers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync(); // SELECT SQL
        }


        [HttpGet("GetExcelPath/{path}")]
        public IActionResult GetExcelPath(string path)
        {
            // Assuming the path is relative to a specific directory in your server
            // TODO - need to change so the user will provide the hole path
            var basePath = Path.Combine(Directory.GetCurrentDirectory());
            var fullPath = Path.Combine(basePath, path);

            // Check if the file exists
            if (!System.IO.File.Exists(fullPath))
            {
                return NotFound(new { message = "File not found" });
            }

            _csvLoader.test(fullPath);
            return Ok(new { massage = $"{fullPath}" });
        }


        [HttpGet("GetExportExcel/{path}")]
        public IActionResult GetExportExcel(string path, GradeMasterDbContext dbContext)
        {
            List<Teacher> teachers = dbContext.Teachers.ToList();

            /*
            var people = new List<Person>
        {
            new Person { Id = 1, Name = "John Doe", Age = 30 },
            new Person { Id = 2, Name = "Jane Smith", Age = 25 }
        };
            */

            string csvFilePath = path;
            this._export.Export(path, teachers);
            return Ok();
            
        }

        // GET: api/Teachers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacher(int id)
        {
            /*
            if(_csvLoader != null)
            {
                _csvLoader.test();
            }
            */

            var teacher = await _context.Teachers.FindAsync(id); // SELECT Where

            if (teacher == null)
            {
                return NotFound();
            }

            return teacher;
        }

        // PUT: api/Teachers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTeacher(int id, Teacher teacher)
        {
            if (id != teacher.Id)
            {
                return BadRequest();
            }

            _context.Entry(teacher).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeacherExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Teachers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Teacher>> PostTeacher(TeacherDTO teacherDto)
        {
            //teacherDto.Id = _context.Entry();
            var teacher = new Teacher { FirstName = teacherDto.FirstName, LastName = teacherDto.LastName, Email = teacherDto.Email, PhoneNumber = teacherDto.PhoneNumber, Password = teacherDto.Password };
            
            
            // Add to Db
            _context.Teachers.Add(teacher);
            await _context.SaveChangesAsync();

            // 201
            // response header (location): api/teacher/5 in response header
            // body: teacher
            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }

        // DELETE: api/Teachers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            var teacher = await _context.Teachers.FindAsync(id);
            if (teacher == null)
            {
                return NotFound();
            }

            _context.Teachers.Remove(teacher);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeacherExists(int id)
        {
            return _context.Teachers.Any(e => e.Id == id);
        }
    }
}
