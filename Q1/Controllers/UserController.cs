using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Q1.Models;

namespace Q1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly PE_PRN_24SumB1Context _context;

        public UserController(PE_PRN_24SumB1Context context)
        {
            _context = context;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get()
        {
            try
            {

                var course = _context.Users.Include(x => x.Enrollments).ThenInclude(c => c.Course)
                    .ToList()
                    .Select(x => new
                    {
                        userId = x.UserId,
                        userName = x.Username,
                        email = x.Email,
                        courses = _context.Enrollments.Include(e => e.Course).Include(e => e.User)
                        .Where(e=> e.UserId == x.UserId)
                        .Select(
                            e => new
                            {
                                e.CourseId,
                                e.Course.Title,
                                e.Course.Description,
                            })
                    }).ToList();
                return Ok(course);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
