using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options):base(options){}
    
    public DbSet<Game> Games { get; set; }
    public DbSet<Game_ordered> Games_ordered { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Mode> Modes { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<Owned_game> Owned_games { get; set; }
    public DbSet<Platform> Platforms { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Spec> Specs { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //Genre
        modelBuilder.Entity<Game>()
            .HasMany(g => g.Genres)
            .WithMany(g => g.Games);
        modelBuilder.Entity<Genre>()
            .HasIndex(p => p.Name)
            .IsUnique();
        
        //Mode
        modelBuilder.Entity<Game>()
            .HasMany(g => g.Modes)
            .WithMany(m => m.Games);
        modelBuilder.Entity<Mode>()
            .HasIndex(p => p.Name)
            .IsUnique();
        
        //Platform
        modelBuilder.Entity<Game>()
            .HasMany(g => g.Platforms)
            .WithMany(m => m.Games);
        modelBuilder.Entity<Platform>()
            .HasIndex(p => p.Name)
            .IsUnique();
        
        //Spec
        modelBuilder.Entity<Spec>()
            .HasOne(s => s.Game)
            .WithMany(g => g.Specs)
            .HasForeignKey(s => s.Game_id)
            .OnDelete(DeleteBehavior.Cascade);
        
        //Game_ordered
        modelBuilder.Entity<Game_ordered>()
            .HasOne(g => g.Game)
            .WithMany(g => g.Games_ordered)
            .HasForeignKey(g => g.Game_id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Game_ordered>()
            .HasOne(g => g.Order)
            .WithMany(g => g.Games_ordered)
            .HasForeignKey(g => g.Order_id)
            .OnDelete(DeleteBehavior.Cascade);
        
        //Order
        modelBuilder.Entity<Order>()
            .HasOne(o => o.User)
            .WithMany(o => o.Orders)
            .HasForeignKey(o => o.User_id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();
        
        //Owned_games
        modelBuilder.Entity<Owned_game>()
            .HasKey(o => new { o.Game_id, o.User_id });
        modelBuilder.Entity<Owned_game>()
            .HasOne(g => g.Game)
            .WithMany(g => g.Owned_games)
            .HasForeignKey(g => g.Game_id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Owned_game>()
            .HasOne(g => g.User)
            .WithMany(o => o.Owned_games)
            .HasForeignKey(g => g.User_id)
            .OnDelete(DeleteBehavior.Cascade);
        
        //User
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        modelBuilder.Entity<User>()
            .HasIndex(p =>p.Phone)
            .IsUnique();
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasDefaultValue(1);
        
        //Game
        modelBuilder.Entity<Game>()
            .HasOne(g => g.Developer)
            .WithMany(g => g.Games)
            .HasForeignKey(g => g.Developer_id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Game>()
            .HasOne(g => g.Publisher)
            .WithMany(g => g.Games)
            .HasForeignKey(g => g.Publisher_id)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Game>()
            .HasIndex(g => g.Title)
            .IsUnique();
        
        //Publisher
        modelBuilder.Entity<Publisher>()
            .HasIndex(p => p.Name)
            .IsUnique();
     
        //Image
        modelBuilder.Entity<Image>()
            .HasOne(i => i.Game)
            .WithMany(g => g.Images)
            .HasForeignKey(i => i.Game_id)
            .OnDelete(DeleteBehavior.Cascade);
    }
}