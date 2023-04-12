using Student_Management_System.Models;
using Microsoft.AspNetCore.Mvc;

namespace Student_Management_System.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentDb DataSet = new DepartmentDb();
        public IActionResult Index()
        {
           List<Department> depts= DataSet.GetAll();
            return View(depts);
        }

        public IActionResult Details(int? id)
        {
            if(id==null)
                return NotFound();
            Department dept = DataSet.GetByID((int)id);
            return View(dept);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Department dept = DataSet.GetByID((int)id);
            if (dept == null)
                return NotFound();

             DataSet.Delete(dept.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department dept)
        {
            if(ModelState.IsValid)
            {
                 DataSet.Add(dept);
                return RedirectToAction("Index");

            }else
                return View(dept);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
                return NotFound();
            Department dept = DataSet.GetByID((int)id);
            if (dept == null)
                return NotFound();

            return View(dept);
        }
        [HttpPost,ActionName("Edit")]
        public IActionResult EditDepartment(Department dept)
        {
            if(ModelState.IsValid)
            {

                DataSet.Update(dept);
                return RedirectToAction("Index");

            }else
                return View("Edit",dept);
        }
      


    }
}
