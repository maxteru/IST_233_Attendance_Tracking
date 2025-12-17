using AttendanceTrackingMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingMVC.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsApiController : ControllerBase
    {
        private readonly AppDbContext context;

        public StudentsApiController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/students
        [HttpGet]
        public IActionResult GetAll()
        {
            var students = context.Students
                .OrderByDescending(s => s.Id)
                .ToList();

            return Ok(students);
        }

        // GET: api/students/5
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var student = context.Students.Find(id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        // POST: api/students
        [HttpPost]
        public IActionResult Create(Student student)
        {
            context.Students.Add(student);
            context.SaveChanges();

            return Ok(student);
        }

        // PUT: api/students/5
        [HttpPut("{id}")]
        public IActionResult Update(int id, Student student)
        {
            if (id != student.Id)
                return BadRequest();

            context.Students.Update(student);
            context.SaveChanges();

            return NoContent();
        }

        // DELETE: api/students/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = context.Students.Find(id);
            if (student == null)
                return NotFound();

            context.Students.Remove(student);
            context.SaveChanges();

            return NoContent();
        }
    }
}
