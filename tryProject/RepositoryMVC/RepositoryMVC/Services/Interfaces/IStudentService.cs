using RepositoryMVC.Models;

namespace RepositoryMVC.Services.Interfaces
{
    /// <summary>
    /// Student service interface defining business logic operations for student management
    /// This service coordinates between controllers and repositories while implementing business rules
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Retrieves all students with their associated grades
        /// </summary>
        /// <returns>Collection of students with grades</returns>
        Task<IEnumerable<Student>> GetAllStudentsAsync();

        /// <summary>
        /// Retrieves a specific student by their unique identifier
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>Student details with grades, or null if not found</returns>
        Task<Student?> GetStudentByIdAsync(int studentId);

        /// <summary>
        /// Creates a new student record with validation
        /// </summary>
        /// <param name="student">The student information to create</param>
        /// <returns>The created student with assigned ID</returns>
        Task<Student> CreateStudentAsync(Student student);

        /// <summary>
        /// Updates an existing student record with validation
        /// </summary>
        /// <param name="student">The updated student information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<bool> UpdateStudentAsync(Student student);

        /// <summary>
        /// Deletes a student and all associated grades
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>True if deletion was successful, false if student not found</returns>
        Task<bool> DeleteStudentAsync(int studentId);

        /// <summary>
        /// Searches for students using multiple criteria
        /// </summary>
        /// <param name="searchTerm">Search term to match against name, email, branch, or section</param>
        /// <returns>Students matching the search criteria</returns>
        Task<IEnumerable<Student>> SearchStudentsAsync(string searchTerm);

        /// <summary>
        /// Validates student data before create or update operations
        /// </summary>
        /// <param name="student">The student data to validate</param>
        /// <param name="isUpdate">True if this is an update operation, false for create</param>
        /// <returns>List of validation error messages, empty if valid</returns>
        Task<List<string>> ValidateStudentAsync(Student student, bool isUpdate = false);

        /// <summary>
        /// Retrieves students by academic branch
        /// </summary>
        /// <param name="branch">The academic branch to filter by</param>
        /// <returns>Students in the specified branch</returns>
        Task<IEnumerable<Student>> GetStudentsByBranchAsync(string branch);

        /// <summary>
        /// Retrieves students by section
        /// </summary>
        /// <param name="section">The section to filter by</param>
        /// <returns>Students in the specified section</returns>
        Task<IEnumerable<Student>> GetStudentsBySectionAsync(string section);

        /// <summary>
        /// Checks if a student email is available for use
        /// </summary>
        /// <param name="email">The email to check</param>
        /// <param name="excludeStudentId">Student ID to exclude from check (for updates)</param>
        /// <returns>True if email is available, false if already in use</returns>
        Task<bool> IsEmailAvailableAsync(string email, int? excludeStudentId = null);

        /// <summary>
        /// Calculates statistics for a student (average grade, total grades, etc.)
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>Student statistics object</returns>
        Task<StudentStatistics?> GetStudentStatisticsAsync(int studentId);
    }

    /// <summary>
    /// Data transfer object for student statistics
    /// </summary>
    public class StudentStatistics
    {
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int TotalGrades { get; set; }
        public decimal? AverageGrade { get; set; }
        public decimal? HighestGrade { get; set; }
        public decimal? LowestGrade { get; set; }
        public string? HighestGradeSubject { get; set; }
        public string? LowestGradeSubject { get; set; }
        public int PassingGrades { get; set; }
        public int FailingGrades { get; set; }
        public double PassingPercentage { get; set; }
    }
}