using RepositoryMVC.Models;

namespace RepositoryMVC.Repositories.Interfaces
{
    /// <summary>
    /// Grade-specific repository interface that extends the generic repository
    /// with grade-specific operations and queries
    /// </summary>
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        /// <summary>
        /// Retrieves all grades for a specific student
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>All grades belonging to the specified student</returns>
        Task<IEnumerable<Grade>> GetGradesByStudentIdAsync(int studentId);

        /// <summary>
        /// Retrieves grades for a specific student in a particular subject
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <param name="subject">The subject to filter by</param>
        /// <returns>Grades for the student in the specified subject</returns>
        Task<IEnumerable<Grade>> GetGradesByStudentAndSubjectAsync(int studentId, string subject);

        /// <summary>
        /// Retrieves all grades for a specific subject across all students
        /// </summary>
        /// <param name="subject">The subject to filter by</param>
        /// <returns>All grades in the specified subject</returns>
        Task<IEnumerable<Grade>> GetGradesBySubjectAsync(string subject);

        /// <summary>
        /// Checks if a grade already exists for a student in a specific subject and date
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <param name="subject">The subject name</param>
        /// <param name="gradeDate">The grade date</param>
        /// <returns>True if a grade already exists, false otherwise</returns>
        Task<bool> IsGradeExistsAsync(int studentId, string subject, DateTime gradeDate);

        /// <summary>
        /// Calculates the average grade for a specific student
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>The average grade value, or null if no grades exist</returns>
        Task<decimal?> GetAverageGradeForStudentAsync(int studentId);

        /// <summary>
        /// Calculates the average grade for a specific subject across all students
        /// </summary>
        /// <param name="subject">The subject name</param>
        /// <returns>The average grade value for the subject</returns>
        Task<decimal?> GetAverageGradeForSubjectAsync(string subject);

        /// <summary>
        /// Retrieves grades within a specific date range
        /// </summary>
        /// <param name="startDate">Start date of the range</param>
        /// <param name="endDate">End date of the range</param>
        /// <returns>Grades assigned within the specified date range</returns>
        Task<IEnumerable<Grade>> GetGradesByDateRangeAsync(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Retrieves grades for a specific semester and academic year
        /// </summary>
        /// <param name="semester">The semester identifier</param>
        /// <param name="academicYear">The academic year</param>
        /// <returns>Grades for the specified semester and year</returns>
        Task<IEnumerable<Grade>> GetGradesBySemesterAsync(string semester, int academicYear);

        /// <summary>
        /// Retrieves the highest grade achieved by a student
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>The highest grade for the student, or null if no grades exist</returns>
        Task<Grade?> GetHighestGradeForStudentAsync(int studentId);

        /// <summary>
        /// Retrieves the lowest grade achieved by a student
        /// </summary>
        /// <param name="studentId">The student's unique identifier</param>
        /// <returns>The lowest grade for the student, or null if no grades exist</returns>
        Task<Grade?> GetLowestGradeForStudentAsync(int studentId);
    }
}