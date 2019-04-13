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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ContosoUniversity.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly ContosoUniversity.Models.SchoolContext _context;

        public IndexModel(ContosoUniversity.Models.SchoolContext context)
        {
            _context = context;
        }

        //public IList<Student> Student { get;set; }
        public PaginatedList<Student> Student { get; set; }

        public string NameSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        // LTPE below
        public int PageSize { get; set; } = 3;
        //public List<int> PageSizesAvailable { get; set; }

        public SelectList PageSizesAvailable { get; set; }

        public int NumberOfElementsOverall { get; set; }

        //public async Task OnGetAsync()
        //{
        //    Student = await _context.Student.ToListAsync();
        //}

        //public async Task OnGetAsync(string sortOrder)
        //{
        //    NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    DateSort = sortOrder == "Date" ? "date_desc" : "Date";

        //    IQueryable<Student> studentIQ = from s in _context.Student
        //                                    select s;

        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            studentIQ = studentIQ.OrderByDescending(s => s.LastName);
        //            break;
        //        case "Date":
        //            studentIQ = studentIQ.OrderBy(s => s.EnrollmentDate);
        //            break;
        //        case "date_desc":
        //            studentIQ = studentIQ.OrderByDescending(s => s.EnrollmentDate);
        //            break;
        //        default:
        //            studentIQ = studentIQ.OrderBy(s => s.LastName);
        //            break;
        //    }

        //    Student = await studentIQ.AsNoTracking().ToListAsync();
        //}

        //public async Task OnGetAsync(string sortOrder, string searchString)
        //{
        //    NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        //    DateSort = sortOrder == "Date" ? "date_desc" : "Date";
        //    CurrentFilter = searchString;

        //    IQueryable<Student> studentIQ = from s in _context.Student
        //                                    select s;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        studentIQ = studentIQ.Where(s => s.LastName.ToUpper().Contains(searchString.ToUpper())
        //                               || s.FirstMidName.ToUpper().Contains(searchString.ToUpper()));
        //    }

        //    switch (sortOrder)
        //    {
        //        case "name_desc":
        //            studentIQ = studentIQ.OrderByDescending(s => s.LastName);
        //            break;
        //        case "Date":
        //            studentIQ = studentIQ.OrderBy(s => s.EnrollmentDate);
        //            break;
        //        case "date_desc":
        //            studentIQ = studentIQ.OrderByDescending(s => s.EnrollmentDate);
        //            break;
        //        default:
        //            studentIQ = studentIQ.OrderBy(s => s.LastName);
        //            break;
        //    }

        //    Student = await studentIQ.AsNoTracking().ToListAsync();
        //}

        //public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex)
        public async Task OnGetAsync(string sortOrder, string currentFilter, string searchString, int? pageIndex, int? pageSize)
        {
            // LTPE added below
            List<PagingOnPage> PagingOnPageList = new List<PagingOnPage>();
            if (null != pageSize)
            {
                PageSize = (int)pageSize;
            }

            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            CurrentFilter = searchString;

            IQueryable<Student> studentIQ = from s in _context.Student
                                            select s;
            
            if (!String.IsNullOrEmpty(searchString))
            {
                studentIQ = studentIQ.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstMidName.Contains(searchString));
            }

            // LTPE added code below.
            for (int Counter = 0; Counter < studentIQ.Count(); Counter++)
            {
                PagingOnPageList.Add(new PagingOnPage((Counter + 1), (Counter + 1).ToString()));
            }
            PageSizesAvailable = new SelectList(PagingOnPageList,
                        "ElementsOnPage", "ElementsOnPageString", (object)PageSize);

            NumberOfElementsOverall = studentIQ.Count();

            switch (sortOrder)
            {
                case "name_desc":
                    studentIQ = studentIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentIQ = studentIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentIQ = studentIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentIQ = studentIQ.OrderBy(s => s.LastName);
                    break;
            }

            // LTPE removed below.
            //int pageSize = 3;
            //Student = await PaginatedList<Student>.CreateAsync(
            //    studentIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            // LTPE added below
            Student = await PaginatedList<Student>.CreateAsync(
                studentIQ.AsNoTracking(), pageIndex ?? 1, PageSize);
        }
    }
}
