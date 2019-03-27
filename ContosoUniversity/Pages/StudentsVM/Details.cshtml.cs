using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Models;
using ContosoUniversity.ViewModels;
using ContosoUniversity.Tools;

namespace ContosoUniversity.Pages.StudentsVM
{
    public class DetailsModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public DetailsModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        public StudentVM StudentVM { get; set; }
        public Student Student { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student = await _context.Student.FirstOrDefaultAsync(m => m.ID == id);
            if (null != Student)
            {
                StudentVM = new StudentVM();
                StudentVM.CopyPropertiesFrom(Student);
            }
            //StudentVM = await _context.StudentVM.FirstOrDefaultAsync(m => m.ID == id);

            if (StudentVM == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
