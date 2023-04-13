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
        private readonly CoursesDb DataSet = new CoursesDb();

        //public CoursesController(CoursesDb _DataSet)
        //{
        //    DataSet = _DataSet;
        //}

        // GET: Courses
        public async Task<IActionResult> Index()
        {
            return View( await DataSet.GetAll());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null )
                  return NotFound();
            
            Course Crs =  await DataSet.GetByID((int) id );

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
                DataSet.Add(course);
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            Course Crs = await DataSet.GetByID((int)id);

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
                Course Crs = await DataSet.GetByID((int)id);

                if (Crs == null)
                {
                    return NotFound();
                }
                DataSet.Update(course);
                
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
             Course Crs = await DataSet.GetByID((int)id);

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
                DataSet.Delete(id);
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
