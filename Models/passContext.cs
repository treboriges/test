 using Microsoft.EntityFrameworkCore;
 
            namespace pass.Models
            {
                public class passContext : DbContext
                {
                    // base() calls the parent class' constructor passing the "options" parameter along
                    public passContext(DbContextOptions<passContext> options) : base(options) { }
                    //the links between our DB and our models
                    public DbSet<Student> students {get;set;}
                    public DbSet<Belt> belts {get;set;}
                    public DbSet<Success> success {get;set;}
               
                }
            }