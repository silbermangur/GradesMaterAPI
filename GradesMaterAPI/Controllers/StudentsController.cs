using GradesMaterAPI.DB;
using GradesMaterAPI.DB.DbModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GradesMaterAPI.Controllers
    // the cotroler will be the route api
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // GET: api/<StudentsController>
        [HttpGet]
        public IActionResult Get()
        {
            //Go to DB And Get All Data
            StudentRepository studentsRepository = new StudentRepository();
            List<Student> students = studentsRepository.GetAllStudents();
            return Ok(students);
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            StudentRepository studentsRepository = new StudentRepository();
            Student s = studentsRepository.GetStudent(id);
            if (s != null)
            {
                return Ok(s);
            }
            return NotFound("Student Not Found");
        }

        // POST api/<StudentsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Student studentInput)
        {
            StudentRepository studentsRepository = new StudentRepository();
            string error = await studentsRepository.InsertStudentAsync(studentInput);
            if (error == string.Empty)
            {
                return Ok(studentInput);
            }
            return BadRequest(error);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Student studentInput)
        {
            StudentRepository studentsRepository = new StudentRepository();
            string result = studentsRepository.UpdateStudent(id, studentInput);
            if (string.IsNullOrEmpty(result))
            {
                return Ok(new { message = "Student updated successfully" });
            }
            return BadRequest(new { error = result });

        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            StudentRepository studentsRepository = new StudentRepository();
            string resualt = studentsRepository.DeleteStudent(id);
            if (resualt == string.Empty)
            {
                return Ok(new { message = "Student deleted successfully" });
            }
            return BadRequest(new { error = resualt });
        }
    }
}
