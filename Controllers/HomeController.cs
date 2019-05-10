using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LastTest.Data;
using Microsoft.EntityFrameworkCore;



namespace LastTest.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ApiController : ControllerBase {

        private readonly ApplicationDbContext _context;

        public ApiController(ApplicationDbContext context) => _context = context;

// Get api/ - returns all notifications
        [HttpGet]
        public ActionResult<IEnumerable<Notification>> GetNotifications() => _context.NotificationItems;


        // Get api/$n - returns notification under an id
        [HttpGet("{id}")]
        public ActionResult<Notification> GetNotification(int id) {
            var notification = _context.NotificationItems.Find(id);
            if (notification == null)
            {
            return NotFound();
            }
            return notification;
        }
        // Post api/ - Add a new notification
        [HttpPost]
        public ActionResult<Notification> PostNotification([FromBody]Notification notification){
            System.Diagnostics.Debug.WriteLine(notification);
            _context.NotificationItems.Add(notification);
            _context.SaveChanges();

            return CreatedAtAction("GetNotification", new Notification{Id = notification.Id}, notification);
        }
        
        [HttpPut("{id}")]
        public ActionResult<Notification> PutNotification(int id, [FromBody]Notification notification){
            if (id != notification.Id)
            {
                return BadRequest();
            }

            _context.Entry(notification).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]

        public ActionResult<Notification> DeleteNotification(int id){
            var notification = _context.NotificationItems.Find(id);
            if (notification == null)
            {
            return NotFound();
            }
            _context.NotificationItems.Remove(notification);
            _context.SaveChanges();
            return notification;        
        }

    }
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController(ApplicationDbContext context) => _context = context;


        [HttpGet]
        public  IActionResult Index()
        {
                  if (User.Identity.IsAuthenticated) {

            // return View (_context.NotificationItems);
            return View ();
                  
                  } else {
        return Redirect ("/Identity/Account/Login");
      }
        }

        [HttpGet("{id}")]
        public  IActionResult Index(int? id)
        {
            if (id == null)
            {
            return View (_context.NotificationItems);
            }
            var notification = _context.NotificationItems.Find(id);
            var enumerable = new [] { notification };
           return View (enumerable);
        }




        public IActionResult Privacy()
        {
            return View();
        }

        // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // public IActionResult Error()
        // {
        //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        // }
    }
}
