using RepositoryMVC.Models;
using RepositoryMVC.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace RepositoryMVC.Services.Implementations
{
    /// <summary>
    /// Grade service implementation containing business logic for grade management
    /// </summary>
    public class GradeService : IGradeService
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly ILogger<GradeService> _logger;

        /// <summary>
        /// Constructor with dependency injection
        /// </summary>
        public GradeService(IGradeRepository gradeRepository, ILogger<GradeService> logger)
        {
            _gradeRepository = gradeRepository;
            _logger = logger;
        }

        /// <summary>
        /// Business logic: Get all grades
        /// </summary>
        public async Task<IEnumerable<Grade>> GetAllGradesAsync()
        {
            try
            {
                _logger.LogInformation("Retrieving all grades with student information");
                return await _gradeRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all grades");
                throw new InvalidOperationException("Failed to retrieve grades", ex);
            }
        }

        /// <summary>
        /// Business logic: Get grade by ID
        /// </summary>
        public async Task<Grade?> GetGradeByIdAsync(int gradeId)
        {
            try
            {
                if (gradeId <= 0)
                {
                    _logger.LogWarning("Invalid grade ID provided: {GradeId}", gradeId);
                    return null;
                }

                _logger.LogInformation("Retrieving grade with ID: {GradeId}", gradeId);
                return await _gradeRepository.GetByIdAsync(gradeId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving grade with ID: {GradeId}", gradeId);
                throw new InvalidOperationException($"Failed to retrieve grade with ID {gradeId}", ex);
            }
        }

        /// <summary>
        /// Business logic: Create new grade
        /// </summary>
        public async Task<Grade> CreateGradeAsync(Grade grade)
        {
            try
            {
                // Business validation
                if (grade == null)
                    throw new ArgumentNullException(nameof(grade), "Grade cannot be null");

                if (grade.StudentID <= 0)
                    throw new ArgumentException("Invalid student ID", nameof(grade));

                if (string.IsNullOrWhiteSpace(grade.Subject))
                    throw new ArgumentException("Subject is required", nameof(grade));

                // Calculate letter grade if not provided
                if (string.IsNullOrEmpty(grade.LetterGrade))
                    grade.LetterGrade = grade.CalculateLetterGrade();

                // Set grade date if not provided
                if (grade.GradeDate == default(DateTime))
                    grade.GradeDate = DateTime.Today;

                // Add grade to repository
                await _gradeRepository.AddAsync(grade);
                
                // Save changes to database
                await _gradeRepository.SaveChangesAsync();
                
                _logger.LogInformation("Successfully created grade for student ID: {StudentId}, Subject: {Subject}", grade.StudentID, grade.Subject);
                return grade;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating grade for student ID: {StudentId}", grade?.StudentID);
                throw;
            }
        }

        /// <summary>
        /// Business logic: Update grade
        /// </summary>
        public async Task<bool> UpdateGradeAsync(Grade grade)
        {
            try
            {
                if (grade == null)
                    throw new ArgumentNullException(nameof(grade), "Grade cannot be null");

                // Check if grade exists
                var existingGrade = await _gradeRepository.GetByIdAsync(grade.GradeID);
                if (existingGrade == null)
                {
                    _logger.LogWarning("Attempt to update non-existent grade: {GradeId}", grade.GradeID);
                    return false;
                }

                // Update grade
                _gradeRepository.Update(grade);
                await _gradeRepository.SaveChangesAsync();
                
                _logger.LogInformation("Successfully updated grade for student ID: {StudentId}, Subject: {Subject}", grade.StudentID, grade.Subject);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating grade for ID: {GradeId}", grade?.GradeID);
                throw;
            }
        }

        /// <summary>
        /// Business logic: Delete grade
        /// </summary>
        public async Task<bool> DeleteGradeAsync(int gradeId)
        {
            try
            {
                if (gradeId <= 0)
                {
                    _logger.LogWarning("Invalid grade ID provided for deletion: {GradeId}", gradeId);
                    return false;
                }

                var grade = await _gradeRepository.GetByIdAsync(gradeId);
                if (grade == null)
                {
                    _logger.LogWarning("Attempt to delete non-existent grade: {GradeId}", gradeId);
                    return false;
                }

                _logger.LogInformation("Deleting grade ID: {GradeId} for student ID: {StudentId}", gradeId, grade.StudentID);

                // Remove grade
                _gradeRepository.Remove(grade);
                await _gradeRepository.SaveChangesAsync();
                
                _logger.LogInformation("Successfully deleted grade ID: {GradeId}", gradeId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting grade: {GradeId}", gradeId);
                throw new InvalidOperationException($"Failed to delete grade with ID {gradeId}", ex);
            }
        }

        /// <summary>
        /// Business logic: Get grades for a student
        /// </summary>
        public async Task<IEnumerable<Grade>> GetGradesByStudentAsync(int studentId)
        {
            try
            {
                return await _gradeRepository.GetGradesByStudentIdAsync(studentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving grades for student ID: {StudentId}", studentId);
                throw;
            }
        }

        /// <summary>
        /// Business logic: Get grades for a subject
        /// </summary>
        public async Task<IEnumerable<Grade>> GetGradesBySubjectAsync(string subject)
        {
            try
            {
                return await _gradeRepository.GetGradesBySubjectAsync(subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving grades for subject: {Subject}", subject);
                throw;
            }
        }

        ///<summary>
        /// Business logic: Validate grade data for a subject
        /// </summary>
        public async Task<bool> ValidateGradeDataAsync(string subject, bool isUpdate = false) 
        {
            try
            {
                var grades = await _gradeRepository.GetGradesBySubjectAsync(subject);
                return grades != null && grades.Any();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error validating grade data for subject: {Subject}", subject);
                throw;
            }
        }

        /// <summary>
        /// Business logic: Calculate student's average grade
        /// </summary>
        public async Task<decimal?> CalculateStudentAverageAsync(int studentId)
        {
            try
            {
                return await _gradeRepository.GetAverageGradeForStudentAsync(studentId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating average grade for student ID: {StudentId}", studentId);
                throw;
            }
        }

        /// <summary>
        /// Business logic: Calculate subject average grade
        /// </summary>
        public async Task<decimal?> CalculateSubjectAverageAsync(string subject)
        {
            try
            {
                return await _gradeRepository.GetAverageGradeForSubjectAsync(subject);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calculating average grade for subject: {Subject}", subject);
                throw;
            }
        }

        /// <summary>
        /// Business logic: Check if grade exists
        /// </summary>
        public async Task<bool> IsDuplicateGradeAsync(int studentId, string subject, DateTime gradeDate)
        {
            try
            {
                return await _gradeRepository.IsGradeExistsAsync(studentId, subject, gradeDate);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking for duplicate grade for student ID: {StudentId}, subject: {Subject}, date: {GradeDate}", studentId, subject, gradeDate);
                throw;
            }
        }

        /// <summary>
        /// Business logic: Get grade statistics
        /// </summary>
        public async Task<GradeStatistics> GetGradeStatisticsAsync()
        {
            try
            {
                var grades = await _gradeRepository.GetAllAsync();

                var totalGrades = grades.Count();
                var totalStudents = grades.Select(g => g.StudentID).Distinct().Count();
                var uniqueSubjects = grades.Select(g => g.Subject).Distinct().Count();
                var overallAverage = grades.Average(g => g.GradeValue);
                var highestGrade = grades.Max(g => g.GradeValue);
                var lowestGrade = grades.Min(g => g.GradeValue);
                var passingGrades = grades.Count(g => g.GradeValue >= 60);
                var failingGrades = grades.Count(g => g.GradeValue < 60);
                var passingPercentage = (double)passingGrades / totalGrades * 100;

                var averagesBySubject = grades
                    .GroupBy(g => g.Subject)
                    .ToDictionary(g => g.Key, g => g.Average(x => x.GradeValue));

                var gradeDistribution = grades
                    .GroupBy(g => g.LetterGrade)
                    .ToDictionary(g => g.Key, g => g.Count());

                return new GradeStatistics
                {
                    TotalGrades = totalGrades,
                    TotalStudents = totalStudents,
                    UniqueSubjects = uniqueSubjects,
                    OverallAverage = overallAverage,
                    HighestGrade = highestGrade,
                    LowestGrade = lowestGrade,
                    PassingGrades = passingGrades,
                    FailingGrades = failingGrades,
                    PassingPercentage = passingPercentage,
                    AveragesBySubject = averagesBySubject,
                    GradeDistribution = gradeDistribution
                };
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Error calculating grade statistics", ex);
            }
        }
    }
}