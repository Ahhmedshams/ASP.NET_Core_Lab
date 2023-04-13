using Student_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Service;

namespace Student_Management_System.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentDb DataSet = new DepartmentDb();
        CoursesDb CourseDataSet = new CoursesDb();
        StudentCoursesDb StudentCoursesDb = new StudentCoursesDb();

        public IActionResult Index()
        {
            List<Department> depts = DataSet.GetAll();
            return View(depts);
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
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
            if (ModelState.IsValid)
            {
                DataSet.Add(dept);
                return RedirectToAction("Index");

            } else
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
        [HttpPost, ActionName("Edit")]
        public IActionResult EditDepartment(Department dept)
        {
            if (ModelState.IsValid)
            {

                DataSet.Update(dept);
                return RedirectToAction("Index");

            } else
                return View("Edit", dept);
        }
        [HttpGet]
        public async Task<IActionResult> Courses(int id)
        {
            var Dept = await DataSet.GetDeptWithCourses(id);
            var Courses = await CourseDataSet.AllNotMatchWith(Dept.courses);
            ViewBag.NewCourses = Courses;
            return View(Dept);
        }

        [HttpPost]
        public async Task<IActionResult> Courses(int Id,
            [FromForm] int[] CoursesToRemove, [FromForm] int[] CoursesToAdd)
        {


            var Dept = await DataSet.GetDeptWithCourses(Id);
            int count = Dept.courses.Count();
            if (CoursesToRemove != null)
            {
                foreach (var item in CoursesToRemove)
                {
                    var crs = await CourseDataSet.GetByID((int)item);
                    bool res = Dept.courses.Remove(crs);
                }
            }


            foreach (var item in CoursesToAdd)
            {
                var crs = await CourseDataSet.GetByID(item);
                Dept.courses.Add(crs);

            }
            await DataSet.SaveDepartmentChanges();
            return RedirectToAction(nameof(Courses), new { id = Id });
        }
        public async Task<IActionResult> StudentGrade(int crsId, int deptId)
        {
            Department department= await DataSet.GetDeptWithStudent(deptId);
            ViewBag.Course = await CourseDataSet.GetByID(crsId);


            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> StudentGrade(int crsId, int deptId, Dictionary<int, int > std)
        {
            
            foreach (var item in std)
            {
                var stdCrs = new StudentCourses () {  CourseId = crsId , StudentId = item.Key ,Degree = item.Value};
                await StudentCoursesDb.Add(stdCrs);
            }
            Department department = await DataSet.GetDeptWithStudent(deptId);
            ViewBag.Course = await CourseDataSet.GetByID(crsId);


            return RedirectToAction(nameof(StudentGrade), department);
        }
    }
}
