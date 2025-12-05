using Microsoft.EntityFrameworkCore;
using RepositoryMVC.Data;
using RepositoryMVC.Models;
using RepositoryMVC.Repositories.Interfaces;

namespace RepositoryMVC.Repositories.Implementations
{
    /// <summary>
    /// Student repository implementation providing student-specific data access operations
    /// Inherits from GenericRepository and implements IStudentRepository
    /// </summary>
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        /// <summary>
        /// Constructor that passes the database context to the base class
        /// </summary>
        /// <param name="context">The application database context</param>
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves all students with their grades included (eager loading)
        /// </summary>
        /// <returns>Students with their grades loaded</returns>
        public async Task<IEnumerable<Student>> GetAllStudentsWithGradesAsync()
        {
            try
            {
                return await _dbSet
                    .Include(s => s.Grades)
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving students with grades", ex);
            }
        }

        /// <summary>
        /// Retrieves a specific student with all their grades included
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>Student with grades loaded, or null if not found</returns>
        public async Task<Student?> GetStudentWithGradesAsync(int studentId)
        {
            try
            {
                return await _dbSet
                    .Include(s => s.Grades.OrderByDescending(g => g.GradeDate))
                    .FirstOrDefaultAsync(s => s.StudentID == studentId);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving student with ID {studentId} and their grades", ex);
            }
        }

        /// <summary>
        /// Searches for students by name using partial matching
        /// </summary>
        /// <param name="name">The name or partial name to search for</param>
        /// <returns>Students whose names contain the search term</returns>
        public async Task<IEnumerable<Student>> FindStudentsByNameAsync(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return await GetAllAsync();

                return await _dbSet
                    .Where(s => s.Name.ToLower().Contains(name.ToLower()))
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error searching students by name '{name}'", ex);
            }
        }

        /// <summary>
        /// Retrieves all students in a specific academic branch
        /// </summary>
        /// <param name="branch">The academic branch to filter by</param>
        /// <returns>Students in the specified branch</returns>
        public async Task<IEnumerable<Student>> GetStudentsByBranchAsync(string branch)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(branch))
                    return await GetAllAsync();

                return await _dbSet
                    .Where(s => s.Branch.ToLower() == branch.ToLower())
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving students by branch '{branch}'", ex);
            }
        }

        /// <summary>
        /// Retrieves all students in a specific section
        /// </summary>
        /// <param name="section">The section to filter by</param>
        /// <returns>Students in the specified section</returns>
        public async Task<IEnumerable<Student>> GetStudentsBySectionAsync(string section)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(section))
                    return await GetAllAsync();

                return await _dbSet
                    .Where(s => s.Section.ToLower() == section.ToLower())
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving students by section '{section}'", ex);
            }
        }

        /// <summary>
        /// Performs a comprehensive search across multiple student fields
        /// </summary>
        /// <param name="searchTerm">The term to search for in name, email, branch, or section</param>
        /// <returns>Students matching the search criteria</returns>
        public async Task<IEnumerable<Student>> SearchStudentsAsync(string searchTerm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchTerm))
                    return await GetAllStudentsWithGradesAsync();

                var lowerSearchTerm = searchTerm.ToLower();

                return await _dbSet
                    .Include(s => s.Grades)
                    .Where(s => s.Name.ToLower().Contains(lowerSearchTerm) ||
                               s.Email.ToLower().Contains(lowerSearchTerm) ||
                               s.Branch.ToLower().Contains(lowerSearchTerm) ||
                               s.Section.ToLower().Contains(lowerSearchTerm))
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error searching students with term '{searchTerm}'", ex);
            }
        }

        /// <summary>
        /// Checks if a student email already exists in the database
        /// </summary>
        /// <param name="email">The email address to check</param>
        /// <param name="excludeStudentId">Optional student ID to exclude from the check (for updates)</param>
        /// <returns>True if email exists, false otherwise</returns>
        public async Task<bool> IsEmailExistsAsync(string email, int? excludeStudentId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                var query = _dbSet.Where(s => s.Email.ToLower() == email.ToLower());

                if (excludeStudentId.HasValue)
                    query = query.Where(s => s.StudentID != excludeStudentId.Value);

                return await query.AnyAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error checking if email '{email}' exists", ex);
            }
        }

        /// <summary>
        /// Retrieves students enrolled in a specific date range
        /// </summary>
        /// <param name="startDate">Start date of enrollment range</param>
        /// <param name="endDate">End date of enrollment range</param>
        /// <returns>Students enrolled within the specified date range</returns>
        public async Task<IEnumerable<Student>> GetStudentsByEnrollmentDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                    throw new ArgumentException("Start date cannot be greater than end date");

                return await _dbSet
                    .Where(s => s.EnrollmentDate >= startDate && s.EnrollmentDate <= endDate)
                    .OrderBy(s => s.EnrollmentDate)
                    .ThenBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving students by enrollment date range {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}", ex);
            }
        }

        /// <summary>
        /// Gets students with their average grade calculated
        /// </summary>
        /// <returns>Students with calculated average grades</returns>
        public async Task<IEnumerable<Student>> GetStudentsWithAverageGradeAsync()
        {
            try
            {
                return await _dbSet
                    .Include(s => s.Grades)
                    .Select(s => new Student
                    {
                        StudentID = s.StudentID,
                        Name = s.Name,
                        Gender = s.Gender,
                        Branch = s.Branch,
                        Section = s.Section,
                        Email = s.Email,
                        PhoneNumber = s.PhoneNumber,
                        EnrollmentDate = s.EnrollmentDate,
                        DateOfBirth = s.DateOfBirth,
                        Address = s.Address,
                        Grades = s.Grades
                    })
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error retrieving students with average grades", ex);
            }
        }

        /// <summary>
        /// Retrieves students who have grades in a specific subject
        /// </summary>
        /// <param name="subject">The subject to filter by</param>
        /// <returns>Students who have grades in the specified subject</returns>
        public async Task<IEnumerable<Student>> GetStudentsBySubjectAsync(string subject)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(subject))
                    return new List<Student>();

                return await _dbSet
                    .Include(s => s.Grades)
                    .Where(s => s.Grades.Any(g => g.Subject.ToLower() == subject.ToLower()))
                    .OrderBy(s => s.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving students by subject '{subject}'", ex);
            }
        }
    }
}