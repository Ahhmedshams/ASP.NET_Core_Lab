using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Student_Management_System.Models;
using Student_Management_System.Service;


namespace Student_Management_System.Controllers
{
    public class CoursesController : Controller
    {
        private ICourseRepository CourseRepo;


        public CoursesController(ICourseRepository _CourseRepo)
        {
            CourseRepo = _CourseRepo;
        }

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View( await CourseRepo.GetAll());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null )
                  return NotFound();
            
            Course Crs =  await CourseRepo.GetByID((int) id );

            if (Crs == null)
            {
                return NotFound();
            }

            return View(Crs);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Lect_Hours,Lab_Hours")] Course course)
        {
            if (ModelState.IsValid)
            {
                CourseRepo.Create(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Course Crs = await CourseRepo.GetByID((int)id);

            if (Crs == null)
            {
                return NotFound();
            }
            return View(Crs);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Lect_Hours,Lab_Hours")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Course Crs = await CourseRepo.GetByID((int)id);

                if (Crs == null)
                {
                    return NotFound();
                }
                CourseRepo.Update(course);
                
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null )
            {
                return NotFound();
            }
             Course Crs = await CourseRepo.GetByID((int)id);

             if (Crs == null)
              {
                    return NotFound();
              }
            

            return View(Crs);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                CourseRepo.Delete(id);
                return RedirectToAction("index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Exception", ex.InnerException.Message);//
                //exception client
                return View(nameof(Details));
            }
        }

      
    }
}
