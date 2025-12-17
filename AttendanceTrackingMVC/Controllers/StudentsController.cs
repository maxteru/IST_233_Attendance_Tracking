using AttendanceTrackingMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace AttendanceTrackingMVC.Controllers
{
    public class StudentsController : Controller
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment environment;

        public StudentsController(AppDbContext context, IWebHostEnvironment environment)
        {
            this.context = context;
            this.environment = environment;
        }

        public IActionResult Index()
        {
            var students = context.Students.OrderByDescending(b => b.Id).ToList();
            return View(students);
        }

        [HttpGet]
        public IActionResult Index(string studentName, string group, string teacher)
        {
            var students = context.Students.AsQueryable();

            if (!string.IsNullOrEmpty(studentName))
            {
                students = students.Where(c => c.StudentName.Contains(studentName));
            }

            if (!string.IsNullOrEmpty(group))
            {
                students = students.Where(c => c.Group.Contains(group));
            }

            if (!string.IsNullOrEmpty(teacher))
            {
                students = students.Where(c => c.Teacher.Contains(teacher));
            }

            var result = students.OrderByDescending(c => c.Id).ToList(); 

            return View(result); 
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(StudentDto studentDto)
        {
            if (studentDto.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "The image file is required");
            }

            if (!ModelState.IsValid)
            {
                return View(studentDto);
            }

            // save the image file
            string newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            newFileName += Path.GetExtension(studentDto.ImageFile!.FileName);

            string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
            using (var stream = System.IO.File.Create(imageFullPath))
            {
                studentDto.ImageFile.CopyTo(stream);
            }

            // save the new student in the database
            Student student = new Student()
            {
                StudentName = studentDto.StudentName,
                ImageFileName = newFileName,
                NumberPasses = studentDto.NumberPasses,
                NumberVisits = studentDto.NumberVisits,
                Reason = studentDto.Reason,
                StatusReason = studentDto.StatusReason,
                Group = studentDto.Group,    
                Teacher = studentDto.Teacher,
                CreatedAt = studentDto.CreatedAt
            };

            context.Students.Add(student);
            context.SaveChanges();

            return RedirectToAction("Index", "Students");
        }

        public IActionResult Edit(int id)
        {
            var student = context.Students.Find(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            // create studentDto from student
            var studentDto = new StudentDto()
            {
                StudentName = student.StudentName,
                NumberPasses = student.NumberPasses,
                NumberVisits = student.NumberVisits,
                Reason= student.Reason,
                StatusReason = student.StatusReason,  
                Group = student.Group,
                Teacher = student.Teacher,
                CreatedAt = student.CreatedAt              
            };

            ViewData["StudentId"] = student.Id;
            ViewData["ImageFileName"] = student.ImageFileName;           

            return View(studentDto);
        }

        [HttpPost]
        public IActionResult Edit(int id, StudentDto studentDto)
        {
            var student = context.Students.Find(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                ViewData["StudentId"] = student.Id;
                ViewData["ImageFileName"] = student.ImageFileName;
                
                return View(studentDto);
            }

            // update the image file if we have a new image file
            string newFileName = student.ImageFileName;
            if (studentDto.ImageFile != null)
            {
                newFileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                newFileName += Path.GetExtension(studentDto.ImageFile!.FileName);

                string imageFullPath = environment.WebRootPath + "/images/" + newFileName;
                using (var stream = System.IO.File.Create(imageFullPath))
                {
                    studentDto.ImageFile.CopyTo(stream);
                }

                // delete the old image
                string oldImageFullPath = environment.WebRootPath + "/images/" + student.ImageFileName;
                System.IO.File.Delete(oldImageFullPath);
            }

            // update the student in the database
            student.StudentName = studentDto.StudentName;
            student.ImageFileName = newFileName;
            student.NumberPasses = studentDto.NumberPasses;
            student.NumberVisits = studentDto.NumberVisits;
            student.Reason = studentDto.Reason;
            student.StatusReason = studentDto.StatusReason;
            student.Group = studentDto.Group;
            student.Teacher = studentDto.Teacher;
            student.CreatedAt = studentDto.CreatedAt;

            context.Students.Update(student); // обновляем студента в контексте
            context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var student = await context.Students.FindAsync(id);

            if (student == null)
            {

                return RedirectToAction("Index", "Students");
            }

            string imageFullPath = environment.WebRootPath + "/images/" + student.ImageFileName;
            System.IO.File.Create(imageFullPath);
            context.Remove(student);

            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
