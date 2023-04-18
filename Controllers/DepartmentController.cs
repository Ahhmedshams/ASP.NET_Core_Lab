using Student_Management_System.Models;
using Microsoft.AspNetCore.Mvc;
using Student_Management_System.Service;
using Student_Management_System.Service.interfaces;

namespace Student_Management_System.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepository DeptRepo ;
        ICourseRepository CourseRepo ;
        IStudentCoursesRepository StudentCoursesRepo ;

        public DepartmentController(IDepartmentRepository _deptRepo, ICourseRepository _CourseRepo,
                                                     IStudentCoursesRepository _StudentCoursesRepo)
        {
            DeptRepo = _deptRepo;
            CourseRepo = _CourseRepo;
            StudentCoursesRepo = _StudentCoursesRepo;
        }


        public async Task<IActionResult> Index()
        {
            List<Department> depts = await DeptRepo.GetAll();
            return View(depts);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();
            Department dept = await DeptRepo.GetByID((int)id);
            return View(dept);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            Department dept = await DeptRepo.GetByID((int)id);
            if (dept == null)
                return NotFound();

          await  DeptRepo.Delete(dept.Id);
            return RedirectToAction("Index");
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(Department dept)
        {
            if (ModelState.IsValid)
            {
               await DeptRepo.Create(dept);
                return  RedirectToAction("Index");

            } else
                return View(dept);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();
            Department dept = await DeptRepo.GetByID((int)id);
            if (dept == null)
                return NotFound();

            return View(dept);
        }
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditDepartment(Department dept)
        {
            if (ModelState.IsValid)
            {

               await DeptRepo.Update(dept);
                return RedirectToAction("Index");

            } else
                return View("Edit", dept);
        }
        [HttpGet]
        public async Task<IActionResult> Courses(int id)
        {
            var Dept = await DeptRepo.GetDeptWithCourses(id);
            var Courses = await CourseRepo.AllNotMatchWith(Dept.courses);
            ViewBag.NewCourses = Courses;
            return View(Dept);
        }

        [HttpPost]
        public async Task<IActionResult> Courses(int Id,
            [FromForm] int[] CoursesToRemove, [FromForm] int[] CoursesToAdd)
        {


            var Dept = await DeptRepo.GetDeptWithCourses(Id);
            int count = Dept.courses.Count();
            if (CoursesToRemove != null)
            {

                foreach (var item in CoursesToRemove)
                {
                    var crs = await CourseRepo.GetByID((int)item);
                    bool res = Dept.courses.Remove(crs);

                }
                await DeptRepo.SaveChanges();

            }


            foreach (var item in CoursesToAdd)
            {
                var crs = await CourseRepo.GetByID(item);
                Dept.courses.Add(crs);

            }
            await DeptRepo.SaveChanges();
            return RedirectToAction(nameof(Courses), new { id = Id });
        }



        public async Task<IActionResult> UpdateStudentDegree(int crsId, int deptId)
        {
            Department department= await DeptRepo.GetDeptWithStudent(deptId);
            ViewBag.Course = await CourseRepo.GetByID(crsId);


            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStudentDegree(int crsId, int deptId, Dictionary<int, int > std)
        {
            
            foreach (var item in std)
            {
                var stud = await  StudentCoursesRepo.check(item.Key, crsId);
                if (stud == null)
                {
                    var stdCrs = new StudentCourses () {  CourseId = crsId , StudentId = item.Key ,Degree = item.Value};
                    await StudentCoursesRepo.Create(stdCrs);

                }
                else
                {
                   await StudentCoursesRepo.UpdateDegree(item.Key, crsId, item.Value);
                }
            }
            Department department = await DeptRepo.GetDeptWithStudent(deptId);
            ViewBag.Course = await CourseRepo.GetByID(crsId);


            return RedirectToAction(nameof(Index), department);
        }
    }
}
