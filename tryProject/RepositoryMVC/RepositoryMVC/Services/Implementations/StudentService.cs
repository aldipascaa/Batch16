using RepositoryMVC.Models;
using RepositoryMVC.Repositories.Interfaces;
using RepositoryMVC.Services.Interfaces;
using System.Text.RegularExpressions;

namespace RepositoryMVC.Services.Repositories
{
    /// <summary>
    /// Student service implementation providing business logic for student management
    /// Coordinates between controllers and repositories while enforcing business rules
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor with dependency injection
        ///
        /// </summary>
        public async Task<Student> CreateStudentAsync(Student student)
        {
            try
            {
                // Business validation
                if (student == null)
                    throw new ArgumentNullException(nameof(student), "Student cannot be null");

                if (string.IsNullOrWhiteSpace(student.Name))
                    throw new ArgumentException("Student name is required", nameof(student));

                if (string.IsNullOrWhiteSpace(student.Email))
                    throw new ArgumentException("Student email is required", nameof(student));

                // Check for duplicate email
                if (await _studentRepository.IsEmailExistsAsync(student.Email))
                {
                    _logger.LogWarning("Attempt to create student with existing email: {Email}", student.Email);
                    throw new InvalidOperationException($"A student with email '{student.Email}' already exists");
                }

                // Set default enrollment date if not provided
                if (student.EnrollmentDate == default(DateTime))
                {
                    student.EnrollmentDate = DateTime.Today;
                    _logger.LogInformation("Set default enrollment date for student: {Email}", student.Email);
                }

                // Validate enrollment date is not in the future
                if (student.EnrollmentDate > DateTime.Today)
                {
                    throw new ArgumentException("Enrollment date cannot be in the future", nameof(student));
                }

                _logger.LogInformation("Creating new student: {Email}", student.Email);

                // Add student to repository
                await _studentRepository.AddAsync(student);

                // Save changes to database
                await _studentRepository.SaveChangesAsync();

                _logger.LogInformation("Successfully created student with ID: {StudentId}", student.StudentID);
                return student;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating student: {Email}", student?.Email);
                throw;
            }
        }

        /// <summary>
        /// Business logic: Update student with validation
        /// </summary>
        public async Task<bool> UpdateStudentAsync(Student student)
        {
            try
            {
                if (student == null)
                    throw new ArgumentNullException(nameof(student), "Student cannot be null");

                // Check if student exists
                var existingStudent = await _studentRepository.GetByIdAsync(student.StudentID);
                if (existingStudent == null)
                {
                    _logger.LogWarning("Attempt to update non-existent student: {StudentId}", student.StudentID);
                    return false;
                }

                // Check for duplicate email (excluding current student)
                if (await _studentRepository.IsEmailExistsAsync(student.Email, student.StudentID))
                {
                    _logger.LogWarning("Attempt to update student with existing email: {Email}", student.Email);
                    throw new InvalidOperationException($"Email '{student.Email}' is already taken by another student");
                }

                // Validate enrollment date
                if (student.EnrollmentDate > DateTime.Today)
                {
                    throw new ArgumentException("Enrollment date cannot be in the future", nameof(student));
                }

                _logger.LogInformation("Updating student: {StudentId}", student.StudentID);

                // Update student
                _studentRepository.Update(student);
                await _studentRepository.SaveChangesAsync();

                _logger.LogInformation("Successfully updated student: {StudentId}", student.StudentID);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating student: {StudentId}", student?.StudentID);
                throw;
            }
        }

        /// <summary>
        /// Business logic: Delete student and cascade delete grades
        /// </summary>
        public async Task<bool> DeleteStudentAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    _logger.LogWarning("Invalid student ID provided for deletion: {StudentId}", id);
                    return false;
                }

                var student = await _studentRepository.GetByIdAsync(id);
                if (student == null)
                {
                    _logger.LogWarning("Attempt to delete non-existent student: {StudentId}", id);
                    return false;
                }

                _logger.LogInformation("Deleting student: {StudentId} - {Name}", id, student.Name);

                // Remove student (grades will be cascade deleted due to FK constraint)
                _studentRepository.Remove(student);
                await _studentRepository.SaveChangesAsync();

                _logger.LogInformation("Successfully deleted student: {StudentId}", id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting student: {StudentId}", id);
                throw new InvalidOperationException($"Failed to delete student with ID {id}", ex);
            }
        }

        /// <summary>
        /// Business logic: Search students with validation
        /// </summary>
        public async Task<IEnumerable<Student>> SearchStudentsAsync(string searchTerm)
        {
            try
            {
                _logger.LogInformation("Searching students with term: {SearchTerm}", searchTerm);

                // If search term is null or empty, return all students
                if (string.IsNullOrWhiteSpace(searchTerm))
                {
                    return await GetAllStudentsAsync();
                }

                // Trim and validate search term
                searchTerm = searchTerm.Trim();
                if (searchTerm.Length < 2)
                {
                    _logger.LogWarning("Search term too short: {SearchTerm}", searchTerm);
                    return await GetAllStudentsAsync();
                }

                return await _studentRepository.SearchStudentsAsync(searchTerm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching students with term: {SearchTerm}", searchTerm);
                throw new InvalidOperationException("Failed to search students", ex);
            }
        }

        /// <summary>
        /// Business logic: Check if email is available
        /// </summary>
        public async Task<bool> IsEmailAvailableAsync(string email, int? excludeStudentId = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email))
                    return false;

                var exists = await _studentRepository.IsEmailExistsAsync(email, excludeStudentId);
                return !exists;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking email availability: {Email}", email);
                throw;
            }
        }

        /// <summary>
        /// Business logic: Get students by branch
        /// </summary>
        public async Task<IEnumerable<Student>> GetStudentsByBranchAsync(string branch)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(branch))
                    return await GetAllStudentsAsync();

                _logger.LogInformation("Getting students by branch: {Branch}", branch);
                return await _studentRepository.GetStudentsByBranchAsync(branch);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting students by branch: {Branch}", branch);
                throw;
            }
        }

        /// <summary>
        /// Business logic: Get students with calculated statistics
        /// </summary>
        public async Task<IEnumerable<Student>> GetStudentsWithStatisticsAsync()
        {
            try
            {
                _logger.LogInformation("Getting students with statistics");
                return await _studentRepository.GetStudentsWithAverageGradeAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting students with statistics");
                throw;
            }
        }
    }
}