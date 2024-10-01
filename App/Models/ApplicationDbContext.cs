using Microsoft.EntityFrameworkCore;

namespace App.Models
{
    public class ApplicationDbContext : DbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Book> Books { get; set; }
        public DbSet<Copy> Copies { get; set; }
        public DbSet<BorrowingRecord> BorrowingRecords { get; set; }
        public DbSet<Student> Students { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Book - Copy relationship(One - to - Many)
            modelBuilder.Entity<Book>()
                .HasMany(b => b.Copies)
                .WithOne(b => b.Book)
                .HasForeignKey(c => c.BookId);

            // Student - BorrowingRecord relationship (One-to-Many)
            modelBuilder.Entity<Student>()
                .HasMany(s => s.BorrowingRecords)
                .WithOne(br => br.Student)
                .HasForeignKey(br => br.StudentId);


            // Copy - BorrowingRecord relationship (One-to-Many)
            modelBuilder.Entity<Copy>()
                .HasMany(c => c.BorrowingRecords)
                .WithOne(br => br.Copy)
                .HasForeignKey(br => br.CopyId);

            modelBuilder.Entity<Student>()
                .HasData(
                    new Student { Id = 1, Name = "Ali", Email = "Ali@enozom.com", Phone = "01222224400" },
                    new Student { Id = 2, Name = "Mohamed", Email = "mohamed@enozom.com", Phone = "0111155000" },
                    new Student { Id = 3, Name = "Ahmed", Email = "ahmed@enozom.com", Phone = "0155553311" }
                );

            modelBuilder.Entity<Book>()
                .HasData(
                    new Book { Id = 1, Title = "Clean Code" },
                    new Book { Id = 2, Title = "Algorithms" }
                );

            modelBuilder.Entity<Copy>()
                .HasData(
                    new Copy { Id = 1, BookId = 1, Status = "Good" },
                    new Copy { Id = 2, BookId = 2, Status = "Good" },
                    new Copy { Id = 3, BookId = 1, Status = "Good" }

                );

        }
    }
}