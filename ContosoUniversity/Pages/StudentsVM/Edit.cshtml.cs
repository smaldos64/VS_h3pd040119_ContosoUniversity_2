using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using ContosoUniversity.Tools;

namespace ContosoUniversity.Pages.StudentsVM
{
    public class EditModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public EditModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudentVM StudentVM { get; set; }

        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student.FirstOrDefaultAsync(m => m.ID == id);
            if (null == Student)
            {
                return NotFound();
            }
            StudentVM = new StudentVM();
            StudentVM.CopyPropertiesFrom(Student);
            //StudentVM.FirstMidName = Student.FirstMidName;
            //StudentVM.LastName = Student.LastName;
            //StudentVM.ID = Student.ID;
            //StudentVM.EnrollmentDate = Student.EnrollmentDate;
            //StudentVM = await _context.StudentVM.FirstOrDefaultAsync(m => m.ID == id);

            //if (StudentVM == null)
            //{
            //    return NotFound();
            //}
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Student = new Student();
            Student.CopyPropertiesFrom(StudentVM);
            _context.Attach(Student).State = EntityState.Modified;

            //_context.Attach(StudentVM).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentVMExists(StudentVM.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool StudentVMExists(int id)
        {
            //return _context.StudentVM.Any(e => e.ID == id);
            return _context.Student.Any(e => e.ID == id);
        }
    }
}
