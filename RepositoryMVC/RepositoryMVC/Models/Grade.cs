using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RepositoryMVC.Models
{
    /// <summary>
    /// Grade entity representing a student's grade in a specific subject
    /// Includes automatic letter grade calculation and validation
    /// </summary>
    public class Grade
    {
        /// <summary>
        /// Primary key for the Grade entity
        /// </summary>
        [Key]
        public int GradeID { get; set; }

        /// <summary>
        /// Foreign key referencing the Student entity
        /// </summary>
        [Required(ErrorMessage = "Student ID is required")]
        [Display(Name = "Student")]
        public int StudentID { get; set; }

        /// <summary>
        /// Subject name for which the grade is assigned
        /// </summary>
        [Required(ErrorMessage = "Subject is required")]
        [StringLength(100, ErrorMessage = "Subject name cannot exceed 100 characters")]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Numerical grade value (0-100)
        /// </summary>
        [Required(ErrorMessage = "Grade value is required")]
        [Range(0, 100, ErrorMessage = "Grade must be between 0 and 100")]
        [Column(TypeName = "decimal(5,2)")]
        [Display(Name = "Grade Value")]
        public decimal GradeValue { get; set; }

        /// <summary>
        /// Letter grade computed from numerical value
        /// </summary>
        [StringLength(2, ErrorMessage = "Letter grade cannot exceed 2 characters")]
        [Display(Name = "Letter Grade")]
        public string? LetterGrade { get; set; }

        /// <summary>
        /// Date when the grade was assigned
        /// </summary>
        [Required(ErrorMessage = "Grade date is required")]
        [DataType(DataType.Date)]
        [Display(Name = "Grade Date")]
        public DateTime GradeDate { get; set; }

        /// <summary>
        /// Optional comments about the grade
        /// </summary>
        [StringLength(500, ErrorMessage = "Comments cannot exceed 500 characters")]
        public string? Comments { get; set; }

        /// <summary>
        /// Academic semester or term
        /// </summary>
        [StringLength(20, ErrorMessage = "Semester cannot exceed 20 characters")]
        public string? Semester { get; set; }

        /// <summary>
        /// Academic year for the grade
        /// </summary>
        [Range(2000, 3000, ErrorMessage = "Academic year must be between 2000 and 3000")]
        [Display(Name = "Academic Year")]
        public int? AcademicYear { get; set; }

        /// <summary>
        /// Navigation property to the associated Student
        /// </summary>
        [ForeignKey("StudentID")]
        public virtual Student? Student { get; set; }

        /// <summary>
        /// Calculated property to determine if the grade is passing
        /// </summary>
        [NotMapped]
        public bool IsPassing => GradeValue >= 60;

        /// <summary>
        /// Method to calculate letter grade based on numerical value
        /// </summary>
        /// <returns>Letter grade representation</returns>
        public string CalculateLetterGrade()
        {
            return GradeValue switch
            {
                >= 97 => "A+",
                >= 93 => "A",
                >= 90 => "A-",
                >= 87 => "B+",
                >= 83 => "B",
                >= 80 => "B-",
                >= 77 => "C+",
                >= 73 => "C",
                >= 70 => "C-",
                >= 67 => "D+",
                >= 65 => "D",
                >= 60 => "D-",
                _ => "F"
            };
        }

        /// <summary>
        /// Method to get grade point value for GPA calculation
        /// </summary>
        /// <returns>Grade point value (0.0 - 4.0)</returns>
        public decimal GetGradePoint()
        {
            return GradeValue switch
            {
                >= 97 => 4.0m,
                >= 93 => 4.0m,
                >= 90 => 3.7m,
                >= 87 => 3.3m,
                >= 83 => 3.0m,
                >= 80 => 2.7m,
                >= 77 => 2.3m,
                >= 73 => 2.0m,
                >= 70 => 1.7m,
                >= 67 => 1.3m,
                >= 65 => 1.0m,
                >= 60 => 0.7m,
                _ => 0.0m
            };
        }
    }
}