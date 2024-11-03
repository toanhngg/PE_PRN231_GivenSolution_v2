using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
          private readonly PE_PRN_24SumB1Context _context;

        public ReviewController(PE_PRN_24SumB1Context context)
        {
            _context = context;
        }
        [HttpGet("User/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var course = _context.Users
              .Include(x => x.Enrollments)
              .Include(x => x.Reviews).ThenInclude(x=> x.Course)
              .Where(x => x.UserId == id)
              .FirstOrDefault();
                var review = course.Reviews
                                     .Where(ss => ss.UserId == id)
                                     .Select(ss => new
                                     {
                                         courseId = ss.CourseId,
                                         title = ss.Course.Title,
                                         reviewText = ss.ReviewText,
                                         reviewDate = ss.ReviewDate,
                                         rating = ss.Rating
                                     });
                return Ok(review);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
