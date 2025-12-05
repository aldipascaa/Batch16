using RepositoryMVC.Models;

namespace RepositoryMVC.Repositories.Interfaces
{
    /// <summary>
    /// Student-specific repository interface that extends the generic repository
    /// with student-specific operations and queries
    /// </summary>
    public interface IStudentRepository : IGenericRepository<Student>
    {
        /// <summary>
        /// Retrieves all students with their grades included (eager loading)
        /// </summary>
        /// <returns>Students with their grades loaded</returns>
        Task<IEnumerable<Student>> GetAllStudentsWithGradesAsync();

        /// <summary>
        /// Retrieves a specific student with all their grades included
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>Student with grades loaded, or null if not found</returns>
        Task<Student?> GetStudentWithGradesAsync(int studentId);

        /// <summary>
        /// Searches for students by name using partial matching
        /// </summary>
        /// <param name="name">The name or partial name to search for</param>
        /// <returns>Students whose names contain the search term</returns>
        Task<IEnumerable<Student>> FindStudentsByNameAsync(string name);

        /// <summary>
        /// Retrieves all students in a specific academic branch
        /// </summary>
        /// <param name="branch">The academic branch to filter by</param>
        /// <returns>Students in the specified branch</returns>
        Task<IEnumerable<Student>> GetStudentsByBranchAsync(string branch);

        /// <summary>
        /// Retrieves all students in a specific section
        /// </summary>
        /// <param name="section">The section to filter by</param>
        /// <returns>Students in the specified section</returns>
        Task<IEnumerable<Student>> GetStudentsBySectionAsync(string section);

        /// <summary>
        /// Performs a comprehensive search across multiple student fields
        /// </summary>
        /// <param name="searchTerm">The term to search for in name, email, branch, or section</param>
        /// <returns>Students matching the search criteria</returns>
        Task<IEnumerable<Student>> SearchStudentsAsync(string searchTerm);

        /// <summary>
        /// Checks if a student email already exists in the database
        /// </summary>
        /// <param name="email">The email address to check</param>
        /// <param name="excludeStudentId">Optional student ID to exclude from the check (for updates)</param>
        /// <returns>True if email exists, false otherwise</returns>
        Task<bool> IsEmailExistsAsync(string email, int? excludeStudentId = null);

        /// <summary>
        /// Retrieves students enrolled in a specific date range
        /// </summary>
        /// <param name="startDate">Start date of enrollment range</param>
        /// <param name="endDate">End date of enrollment range</param>
        /// <returns>Students enrolled within the specified date range</returns>
        Task<IEnumerable<Student>> GetStudentsByEnrollmentDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Gets students with their average grade calculated
        /// </summary>
        /// <returns>Students with calculated average grades</returns>
        Task<IEnumerable<Student>> GetStudentsWithAverageGradeAsync();

        /// <summary>
        /// Retrieves students who have grades in a specific subject
        /// </summary>
        /// <param name="subject">The subject to filter by</param>
        /// <returns>Students who have grades in the specified subject</returns>
        Task<IEnumerable<Student>> GetStudentsBySubjectAsync(string subject);
    }
}