using RepositoryMVC.Models;

namespace RepositoryMVC.Services.Interfaces
{
    /// <summary>
    /// Grade service interface defining business logic operations for grade management
    /// This service handles grade-related business rules and validation
    /// </summary>
    public interface IGradeService
    {
        /// <summary>
        /// Retrieves all grades with student information
        /// </summary>
        /// <returns>Collection of grades with associated student data</returns>
        Task<IEnumerable<Grade>> GetAllGradesAsync();

        /// <summary>
        /// Retrieves a specific grade by its unique identifier
        /// </summary>
        /// <param name="gradeId">The grade's unique identifier</param>
        /// <returns>Grade details with student information, or null if not found</returns>
        Task<Grade?> GetGradeByIdAsync(int gradeId);

        /// <summary>
        /// Creates a new grade record with validation and business rules
        /// </summary>
        /// <param name="grade">The grade information to create</param>
        /// <returns>The created grade with calculated letter grade</returns>
        Task<Grade> CreateGradeAsync(Grade grade);

        /// <summary>
        /// Updates an existing grade record with validation
        /// </summary>
        /// <param name="grade">The updated grade information</param>
        /// <returns>True if update was successful, false otherwise</returns>
        Task<bool> UpdateGradeAsync(Grade grade);

        /// <summary>
        /// Deletes a grade record
        /// </summary>
        /// <param name="gradeId">The grade's unique identifier</param>
        /// <returns>True if deletion was successful, false if grade not found</returns>
        Task<bool> DeleteGradeAsync(int gradeId);

        /// <summary>
        /// Retrieves all grades for a specific student
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>All grades belonging to the specified student</returns>
        Task<IEnumerable<Grade>> GetGradesByStudentAsync(int studentId);

        /// <summary>
        /// Retrieves grades for a specific subject across all students
        /// </summary>
        /// <param name="subject">The subject name</param>
        /// <returns>All grades in the specified subject</returns>
        Task<IEnumerable<Grade>> GetGradesBySubjectAsync(string subject);

        /// <summary>
        /// Validates grade data before create or update operations
        /// </summary>
        /// <param name="grade">The grade data to validate</param>
        /// <param name="isUpdate">True if this is an update operation, false for create</param>
        /// <returns>List of validation error messages, empty if valid</returns>
        //Task<List<string>> ValidateGradeAsync(Grade grade, bool isUpdate = false);

        /// <summary>
        /// Calculates the average grade for a student
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>Average grade value, or null if no grades exist</returns>
        Task<decimal?> CalculateStudentAverageAsync(int studentId);

        /// <summary>
        /// Calculates the average grade for a subject
        /// </summary>
        /// <param name="subject">The subject name</param>
        /// <returns>Average grade value for the subject</returns>
        Task<decimal?> CalculateSubjectAverageAsync(string subject);

        /// <summary>
        /// Retrieves grades within a specific date range
        /// </summary>
        /// <param name="startDate">Start date of the range</param>
        /// <param name="endDate">End date of the range</param>
        /// <returns>Grades assigned within the specified date range</returns>
        //Task<IEnumerable<Grade>> GetGradesByDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Checks if a grade already exists for the same student, subject, and date
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <param name="subject">The subject name</param>
        /// <param name="gradeDate">The grade date</param>
        /// <returns>True if duplicate exists, false otherwise</returns>
        Task<bool> IsDuplicateGradeAsync(int studentId, string subject, DateTime gradeDate);

        /// <summary>
        /// Retrieves grade statistics for reporting purposes
        /// </summary>
        /// <returns>Grade statistics and analytics</returns>
        Task<GradeStatistics> GetGradeStatisticsAsync();
    }

    /// <summary>
    /// Data transfer object for grade statistics and analytics
    /// </summary>
    public class GradeStatistics
    {
        public int TotalGrades { get; set; }
        public int TotalStudents { get; set; }
        public int UniqueSubjects { get; set; }
        public decimal OverallAverage { get; set; }
        public decimal HighestGrade { get; set; }
        public decimal LowestGrade { get; set; }
        public int PassingGrades { get; set; }
        public int FailingGrades { get; set; }
        public double PassingPercentage { get; set; }
        public Dictionary<string, decimal> AveragesBySubject { get; set; } = new();
        public Dictionary<string, int> GradeDistribution { get; set; } = new();
    }
}