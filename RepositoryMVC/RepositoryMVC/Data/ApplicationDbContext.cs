using Microsoft.EntityFrameworkCore;
using RepositoryMVC.Models;

namespace RepositoryMVC.Data
{
    /// <summary>
    /// Application database context for Entity Framework Core
    /// Configures database tables, relationships, and constraints
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Constructor that receives database context options
        /// </summary>
        /// <param name="options">Database context configuration options</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        /// <summary>
        /// Students table representation
        /// </summary>
        public DbSet<Student> Students { get; set; } = null!;

        /// <summary>
        /// Grades table representation
        /// </summary>
        public DbSet<Grade> Grades { get; set; } = null!;

        /// <summary>
        /// Configures entity relationships and constraints using Fluent API
        /// </summary>
        /// <param name="modelBuilder">Model builder for configuration</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Student entity
            ConfigureStudentEntity(modelBuilder);

            // Configure Grade entity
            ConfigureGradeEntity(modelBuilder);

            // Configure relationships
            ConfigureRelationships(modelBuilder);

            // Add seed data
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Configures the Student entity properties and constraints
        /// </summary>
        /// <param name="modelBuilder">Model builder instance</param>
        private static void ConfigureStudentEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>(entity =>
            {
                // Primary key
                entity.HasKey(s => s.StudentID);

                // Property configurations
                entity.Property(s => s.Name)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(s => s.Gender)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(s => s.Branch)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(s => s.Section)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(s => s.PhoneNumber)
                    .HasMaxLength(20);

                entity.Property(s => s.Address)
                    .HasMaxLength(500);

                entity.Property(s => s.EnrollmentDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                // Indexes for performance
                entity.HasIndex(s => s.Email)
                    .IsUnique()
                    .HasDatabaseName("IX_Student_Email");

                entity.HasIndex(s => s.Branch)
                    .HasDatabaseName("IX_Student_Branch");

                entity.HasIndex(s => s.Section)
                    .HasDatabaseName("IX_Student_Section");

                entity.HasIndex(s => s.EnrollmentDate)
                    .HasDatabaseName("IX_Student_EnrollmentDate");

                // Table name
                entity.ToTable("Students");
            });
        }

        /// <summary>
        /// Configures the Grade entity properties and constraints
        /// </summary>
        /// <param name="modelBuilder">Model builder instance</param>
        private static void ConfigureGradeEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Grade>(entity =>
            {
                // Primary key
                entity.HasKey(g => g.GradeID);

                // Property configurations
                entity.Property(g => g.Subject)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(g => g.GradeValue)
                    .IsRequired()
                    .HasColumnType("decimal(5,2)");

                entity.Property(g => g.LetterGrade)
                    .HasMaxLength(5);

                entity.Property(g => g.GradeDate)
                    .IsRequired()
                    .HasDefaultValueSql("GETDATE()");

                entity.Property(g => g.Semester)
                    .HasMaxLength(20);

                entity.Property(g => g.AcademicYear)
                    .IsRequired();

                entity.Property(g => g.Comments)
                    .HasMaxLength(500);

                // Indexes for performance
                entity.HasIndex(g => g.StudentID)
                    .HasDatabaseName("IX_Grade_StudentID");

                entity.HasIndex(g => g.Subject)
                    .HasDatabaseName("IX_Grade_Subject");

                entity.HasIndex(g => g.GradeDate)
                    .HasDatabaseName("IX_Grade_GradeDate");

                entity.HasIndex(g => new { g.StudentID, g.Subject, g.GradeDate })
                    .IsUnique()
                    .HasDatabaseName("IX_Grade_StudentSubjectDate");

                // Table name
                entity.ToTable("Grades");
            });
        }

        /// <summary>
        /// Configures entity relationships using Fluent API
        /// </summary>
        /// <param name="modelBuilder">Model builder instance</param>
        private static void ConfigureRelationships(ModelBuilder modelBuilder)
        {
            // Student-Grade relationship (One-to-Many)
            modelBuilder.Entity<Grade>()
                .HasOne(g => g.Student)
                .WithMany(s => s.Grades)
                .HasForeignKey(g => g.StudentID)
                .OnDelete(DeleteBehavior.Cascade) // Cascade delete grades when student is deleted
                .HasConstraintName("FK_Grade_Student");
        }

        /// <summary>
        /// Seeds initial data for testing and demonstration purposes
        /// </summary>
        /// <param name="modelBuilder">Model builder instance</param>
        private static void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Students
            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    StudentID = 1,
                    Name = "John Doe",
                    Gender = "Male",
                    Branch = "Computer Science",
                    Section = "A",
                    Email = "john.doe@university.edu",
                    PhoneNumber = "+1-555-0101",
                    EnrollmentDate = new DateTime(2023, 9, 1),
                    DateOfBirth = new DateTime(2000, 5, 15),
                    Address = "123 Main Street, University City"
                },
                new Student
                {
                    StudentID = 2,
                    Name = "Jane Smith",
                    Gender = "Female",
                    Branch = "Computer Science",
                    Section = "A",
                    Email = "jane.smith@university.edu",
                    PhoneNumber = "+1-555-0102",
                    EnrollmentDate = new DateTime(2023, 9, 1),
                    DateOfBirth = new DateTime(2001, 3, 22),
                    Address = "456 Oak Avenue, University City"
                },
                new Student
                {
                    StudentID = 3,
                    Name = "Mike Johnson",
                    Gender = "Male",
                    Branch = "Information Technology",
                    Section = "B",
                    Email = "mike.johnson@university.edu",
                    PhoneNumber = "+1-555-0103",
                    EnrollmentDate = new DateTime(2023, 9, 1),
                    DateOfBirth = new DateTime(2000, 8, 10),
                    Address = "789 Pine Road, University City"
                },
                new Student
                {
                    StudentID = 4,
                    Name = "Sarah Wilson",
                    Gender = "Female",
                    Branch = "Information Technology",
                    Section = "B",
                    Email = "sarah.wilson@university.edu",
                    PhoneNumber = "+1-555-0104",
                    EnrollmentDate = new DateTime(2023, 9, 1),
                    DateOfBirth = new DateTime(2001, 1, 5),
                    Address = "321 Elm Street, University City"
                },
                new Student
                {
                    StudentID = 5,
                    Name = "David Brown",
                    Gender = "Male",
                    Branch = "Computer Science",
                    Section = "C",
                    Email = "david.brown@university.edu",
                    PhoneNumber = "+1-555-0105",
                    EnrollmentDate = new DateTime(2023, 9, 1),
                    DateOfBirth = new DateTime(2000, 11, 18),
                    Address = "654 Maple Lane, University City"
                }
            );

            // Seed Grades
            modelBuilder.Entity<Grade>().HasData(
                // John Doe's grades
                new Grade
                {
                    GradeID = 1,
                    StudentID = 1,
                    Subject = "Programming Fundamentals",
                    GradeValue = 85.5m,
                    LetterGrade = "B",
                    GradeDate = new DateTime(2024, 1, 15),
                    Semester = "Fall 2023",
                    AcademicYear = 2023,
                    Comments = "Good understanding of basic concepts"
                },
                new Grade
                {
                    GradeID = 2,
                    StudentID = 1,
                    Subject = "Data Structures",
                    GradeValue = 92.0m,
                    LetterGrade = "A",
                    GradeDate = new DateTime(2024, 1, 20),
                    Semester = "Fall 2023",
                    AcademicYear = 2023,
                    Comments = "Excellent performance"
                },
                // Jane Smith's grades
                new Grade
                {
                    GradeID = 3,
                    StudentID = 2,
                    Subject = "Programming Fundamentals",
                    GradeValue = 88.0m,
                    LetterGrade = "B+",
                    GradeDate = new DateTime(2024, 1, 15),
                    Semester = "Fall 2023",
                    AcademicYear = 2023,
                    Comments = "Strong programming skills"
                },
                new Grade
                {
                    GradeID = 4,
                    StudentID = 2,
                    Subject = "Database Systems",
                    GradeValue = 95.5m,
                    LetterGrade = "A",
                    GradeDate = new DateTime(2024, 1, 25),
                    Semester = "Fall 2023",
                    AcademicYear = 2023,
                    Comments = "Outstanding database design skills"
                },
                // Mike Johnson's grades
                new Grade
                {
                    GradeID = 5,
                    StudentID = 3,
                    Subject = "Network Administration",
                    GradeValue = 78.5m,
                    LetterGrade = "C+",
                    GradeDate = new DateTime(2024, 1, 18),
                    Semester = "Fall 2023",
                    AcademicYear = 2023,
                    Comments = "Needs improvement in practical applications"
                },
                new Grade
                {
                    GradeID = 6,
                    StudentID = 3,
                    Subject = "System Analysis",
                    GradeValue = 82.0m,
                    LetterGrade = "B-",
                    GradeDate = new DateTime(2024, 1, 22),
                    Semester = "Fall 2023",
                    AcademicYear = 2023,
                    Comments = "Good analytical thinking"
                }
            );
        }
    }
}