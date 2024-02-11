using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebAppP3PP3.Models;

namespace WebAppP3PP3.Controllers
{
    public class StudentsController : Controller
    {
        private readonly Phase3pp3Context _context;

        public StudentsController(Phase3pp3Context context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            var phase3pp3Context = _context.Students.Include(s => s.CidNavigation).Include(s => s.Subj);
            return View(await phase3pp3Context.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.CidNavigation)
                .Include(s => s.Subj)
                .FirstOrDefaultAsync(m => m.StuRollNo == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["Cid"] = new SelectList(_context.Classes, "Cid", "Cid");
            ViewData["SubjId"] = new SelectList(_context.Subjects, "SubId", "SubId");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StuName,StuRollNo,Cid,SubjId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cid"] = new SelectList(_context.Classes, "Cid", "Cid", student.Cid);
            ViewData["SubjId"] = new SelectList(_context.Subjects, "SubId", "SubId", student.SubjId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["Cid"] = new SelectList(_context.Classes, "Cid", "Cid", student.Cid);
            ViewData["SubjId"] = new SelectList(_context.Subjects, "SubId", "SubId", student.SubjId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StuName,StuRollNo,Cid,SubjId")] Student student)
        {
            if (id != student.StuRollNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StuRollNo))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Cid"] = new SelectList(_context.Classes, "Cid", "Cid", student.Cid);
            ViewData["SubjId"] = new SelectList(_context.Subjects, "SubId", "SubId", student.SubjId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(s => s.CidNavigation)
                .Include(s => s.Subj)
                .FirstOrDefaultAsync(m => m.StuRollNo == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Students == null)
            {
                return Problem("Entity set 'Phase3pp3Context.Students'  is null.");
            }
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
          return (_context.Students?.Any(e => e.StuRollNo == id)).GetValueOrDefault();
        }
    }
}
