using Student_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Student_Management_System.Service;

namespace Student_Management_System.Controllers
{
    public class StudentController : Controller
    {
        IStudentRepository StdDb;
        IDepartmentRepository DeptRepo ;

       public StudentController(IStudentRepository _StdDb , IDepartmentRepository _DeptRepo)
        {
            StdDb = _StdDb;
            DeptRepo = _DeptRepo;
        }
        public async Task<IActionResult> Index()
        {
            List<Student> studs = await StdDb.GetAll();
            return View(studs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            Student stud = await StdDb.GetByID((int)id);
            return View(stud);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Student stud = await StdDb.GetByID((int)id);
            if (stud == null)
                return NotFound();

            await StdDb.Delete(stud.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Depts = await DeptRepo.GetAll();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Student stud)
        {
            ModelState.Remove("Department");
            ViewBag.Depts = DeptRepo.GetAll();
            if (ModelState.IsValid)
            {

                await StdDb.Create(stud);
                return RedirectToAction("Index");
            }else
                return View(stud);
           
        }

        public  async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            Student stud = await StdDb.GetByID((int)id);
            if (stud == null)
                return NotFound();
            //send Data
            ViewBag.Depts = await DeptRepo.GetAll();
            return View(stud);
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditStudent(Student stud)
        {
            ViewBag.Depts = await DeptRepo.GetAll();
            if (ModelState.IsValid)
            {
                StdDb.Update(stud);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", stud);

            }

        }
        
    }
}
