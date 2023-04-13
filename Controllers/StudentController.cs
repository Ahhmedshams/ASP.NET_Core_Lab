using Student_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Student_Management_System.Service;

namespace Student_Management_System.Controllers
{
    public class StudentController : Controller
    {
        StudentDb DataSet = new StudentDb();
        DepartmentDb DepartmentDataSet = new DepartmentDb();
        public IActionResult Index()
        {
            List<Student> studs = DataSet.GetAll();
            return View(studs);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
                return NotFound();
            Student stud = DataSet.GetByID((int)id);
            return View(stud);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Student stud = DataSet.GetByID((int)id);
            if (stud == null)
                return NotFound();

             DataSet.Delete(stud.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            ViewBag.Depts = DepartmentDataSet.GetAll();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Student stud)
        {
            ModelState.Remove("Department");
            ViewBag.Depts = DepartmentDataSet.GetAll();
            if (ModelState.IsValid)
            {

                DataSet.Add(stud);
                return RedirectToAction("Index");
            }else
                return View(stud);
           
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            Student stud = DataSet.GetByID((int)id);
            if (stud == null)
                return NotFound();
            //send Data
            ViewBag.Depts = DepartmentDataSet.GetAll();
            return View(stud);
        }
        [HttpPost, ActionName("Edit")]
        public IActionResult EditStudent(Student stud)
        {
            ViewBag.Depts = DepartmentDataSet.GetAll();
            if (ModelState.IsValid)
            {
                DataSet.Update(stud);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Edit", stud);

            }

        }
        
    }
}
