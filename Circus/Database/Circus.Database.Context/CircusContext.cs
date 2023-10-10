using Microsoft.EntityFrameworkCore;
using Circus.Database.Models; 

namespace Circus.Database.Context;

public class CircusContext : DbContext
{
    public DbSet<Actor> Actors { get; set; }
    
    public DbSet<ActorShow> ActorShows { get; set; }
    
    public DbSet<Feedback> Feedbacks { get; set; }
    
    public DbSet<File> Files { get; set; }
    
    public DbSet<Hall> Halls { get; set; }
    
    public DbSet<Row> Rows { get; set; }
    
    public DbSet<Seat> Seats { get; set; }
    
    public DbSet<Sector> Sectors { get; set; }
    
    public DbSet<Session> Sessions { get; set; }
    
    public DbSet<Show> Shows { get; set; }
    
    public DbSet<Ticket> Tickets { get; set; }
    
    public DbSet<User> Users { get; set; }

    public CircusContext()
    {
    }

    public CircusContext(DbContextOptions<CircusContext> options)
        : base(options)
    {
    }
}
