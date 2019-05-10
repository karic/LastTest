using Microsoft.EntityFrameworkCore;


namespace LastTest.Models
{
    public class NotificationContext : DbContext
    {


        public NotificationContext(DbContextOptions<NotificationContext> options) :base (options)
        {
            
        }
        public DbSet<Notification> NotificationItems {get; set;}
    }        
}