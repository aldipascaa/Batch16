using Microsoft.EntityFrameworkCore;
using RepositoryMVC.Data;
using RepositoryMVC.Models;
using RepositoryMVC.Repositories.Interfaces;

namespace RepositoryMVC.Repositories.Implementations
{
    /// <summary>
    /// Grade repository implementation providing grade-specific data access operations
    /// Inherits from GenericRepository and implements IGradeRepository
    /// </summary>
    public class GradeRepository : GenericRepository<Grade>, IGradeRepository
    {
        /// <summary>
        /// Constructor that passes the database context to the base class
        /// </summary>
        /// <param name="context">The application database context</param>
        public GradeRepository(ApplicationDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves all grades for a specific student
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>All grades belonging to the specified student</returns>
        public async Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId)
        {
            try
            {
                return await _dbSet
                    .Include(g => g.Student)
                    .Where(g => g.StudentID == studentId)
                    .OrderByDescending(g => g.GradeDate)
                    .ThenBy(g => g.Subject)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving grades for student ID {studentId}", ex);
            }
        }

        /// <summary>
        /// Retrieves grades for a specific student in a particular subject
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <param name="subject">The subject to filter by</param>
        /// <returns>Grades for the student in the specified subject</returns>
        public async Task<IEnumerable<Grade>> GetGradesByStudentAndSubjectAsync(int studentId, string subject)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(subject))
                    return await GetGradesByStudentIdAsync(studentId);

                return await _dbSet
                    .Include(g => g.Student)
                    .Where(g => g.StudentID == studentId &&
                               g.Subject.ToLower() == subject.ToLower())
                    .OrderByDescending(g => g.GradeDate)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving grades for student ID {studentId} in subject '{subject}'", ex);
            }
        }

        /// <summary>
        /// Retrieves all grades for a specific subject across all students
        /// </summary>
        /// <param name="subject">The subject to filter by</param>
        /// <returns>All grades in the specified subject</returns>
        public async Task<IEnumerable<Grade>> GetGradesBySubjectAsync(string subject)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(subject))
                    return new List<Grade>();

                return await _dbSet
                    .Include(g => g.Student)
                    .Where(g => g.Subject.ToLower() == subject.ToLower())
                    .OrderByDescending(g => g.GradeValue)
                    .ThenBy(g => g.Student!.Name)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving grades for subject '{subject}'", ex);
            }
        }

        /// <summary>
        /// Checks if a grade already exists for a student in a specific subject and date
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <param name="subject">The subject name</param>
        /// <param name="gradeDate">The grade date</param>
        /// <returns>True if a grade already exists, false otherwise</returns>
        public async Task<bool> IsGradeExistsAsync(int studentId, string subject, DateTime gradeDate)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(subject))
                    return false;

                return await _dbSet.AnyAsync(g =>
                    g.StudentID == studentId &&
                    g.Subject.ToLower() == subject.ToLower() &&
                    g.GradeDate.Date == gradeDate.Date);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error checking if grade exists for student ID {studentId}, subject '{subject}', date {gradeDate:yyyy-MM-dd}", ex);
            }
        }

        /// <summary>
        /// Calculates the average grade for a specific student
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>The average grade value, or null if no grades exist</returns>
        public async Task<decimal?> GetAverageGradeForStudentAsync(int studentId)
        {
            try
            {
                var grades = await _dbSet
                    .Where(g => g.StudentID == studentId)
                    .ToListAsync();

                if (!grades.Any())
                    return null;

                return grades.Average(g => g.GradeValue);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error calculating average grade for student ID {studentId}", ex);
            }
        }

        /// <summary>
        /// Calculates the average grade for a specific subject across all students
        /// </summary>
        /// <param name="subject">The subject name</param>
        /// <returns>The average grade value for the subject</returns>
        public async Task<decimal?> GetAverageGradeForSubjectAsync(string subject)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(subject))
                    return null;

                var grades = await _dbSet
                    .Where(g => g.Subject.ToLower() == subject.ToLower())
                    .ToListAsync();

                if (!grades.Any())
                    return null;

                return grades.Average(g => g.GradeValue);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error calculating average grade for subject '{subject}'", ex);
            }
        }

        /// <summary>
        /// Retrieves grades within a specific date range
        /// </summary>
        /// <param name="startDate">Start date of the range</param>
        /// <param name="endDate">End date of the range</param>
        /// <returns>Grades assigned within the specified date range</returns>
        public async Task<IEnumerable<Grade>> GetGradesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                if (startDate > endDate)
                    throw new ArgumentException("Start date cannot be greater than end date");

                return await _dbSet
                    .Include(g => g.Student)
                    .Where(g => g.GradeDate >= startDate && g.GradeDate <= endDate)
                    .OrderByDescending(g => g.GradeDate)
                    .ThenBy(g => g.Student!.Name)
                    .ThenBy(g => g.Subject)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving grades by date range {startDate:yyyy-MM-dd} to {endDate:yyyy-MM-dd}", ex);
            }
        }

        /// <summary>
        /// Retrieves grades for a specific semester and academic year
        /// </summary>
        /// <param name="semester">The semester identifier</param>
        /// <param name="academicYear">The academic year</param>
        /// <returns>Grades for the specified semester and year</returns>
        public async Task<IEnumerable<Grade>> GetGradesBySemesterAsync(string semester, int academicYear)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(semester))
                    return new List<Grade>();

                return await _dbSet
                    .Include(g => g.Student)
                    .Where(g => g.Semester != null &&
                               g.Semester.ToLower() == semester.ToLower() &&
                               g.AcademicYear == academicYear)
                    .OrderBy(g => g.Student!.Name)
                    .ThenBy(g => g.Subject)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving grades for semester '{semester}' and academic year {academicYear}", ex);
            }
        }

        /// <summary>
        /// Retrieves the highest grade achieved by a student
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>The highest grade for the student, or null if no grades exist</returns>
        public async Task<Grade?> GetHighestGradeForStudentAsync(int studentId)
        {
            try
            {
                return await _dbSet
                    .Include(g => g.Student)
                    .Where(g => g.StudentID == studentId)
                    .OrderByDescending(g => g.GradeValue)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving highest grade for student ID {studentId}", ex);
            }
        }

        /// <summary>
        /// Retrieves the lowest grade achieved by a student
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>The lowest grade for the student, or null if no grades exist</returns>
        public async Task<Grade?> GetLowestGradeForStudentAsync(int studentId)
        {
            try
            {
                return await _dbSet
                    .Include(g => g.Student)
                    .Where(g => g.StudentID == studentId)
                    .OrderBy(g => g.GradeValue)
                    .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error retrieving lowest grade for student ID {studentId}", ex);
            }
        }
    }
}