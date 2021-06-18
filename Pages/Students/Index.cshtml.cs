using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesStudent.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RazorPagesStudent.Pages_Students
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesStudentContext _context;

        public IndexModel(RazorPagesStudentContext context)
        {
            _context = context;
        }

        public IList<Student> Student { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        public SelectList Genres { get; set; }
        [BindProperty(SupportsGet = true)]
        public string StudentGenre { get; set; }


        public async Task OnGetAsync()
        {
            IQueryable<string> genreQuery = from m in _context.Student
                                            orderby m.Genre
                                            select m.Genre;

            var students = from m in _context.Student
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                students = students.Where(s => s.Title.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(StudentGenre))
            {
                students = students.Where(x => x.Genre == StudentGenre);
            }
            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Student = await students.ToListAsync();
        }

    }
}
