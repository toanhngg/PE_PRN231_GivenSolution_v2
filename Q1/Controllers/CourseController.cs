using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

    public class CourseController : ControllerBase
    {
        private readonly PE_PRN_24SumB1Context _context;

        public CourseController(PE_PRN_24SumB1Context context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var course = _context.Courses
                .Include(x => x.Categories)
                .Include(x => x.Enrollments)
                .Include(x => x.Reviews)
                .Where(x => x.CourseId == id)
                .Select(x=> new { 
                   courseId =  x.CourseId,
                   courseTitle =  x.Title,
                   user = _context.Enrollments.Include(e=> e.User)
                   .Where(e => e.CourseId == id)
                   .Select(u=> new
                   {
                       userId = u.UserId,
                       userName = u.User.Username,
                       enrolledDate= u.EnrolledDate

                   }).ToList(),
                    }
                )
                .FirstOrDefault();
                return Ok(course);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
