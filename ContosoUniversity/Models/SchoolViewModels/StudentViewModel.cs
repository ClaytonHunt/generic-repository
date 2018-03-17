using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ContosoUniversity.Pages.Students;

namespace ContosoUniversity.Models.SchoolViewModels
{
    public class StudentViewModel : IStandardIdentity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Column("FirstName")]
        [StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters")]        
        [Display(Name = "First Name")]
        public string FirstMidName { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Enrollment Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        [Display(Name = "Full Name")] 
        public string FullName => $"{LastName}, {FirstMidName}";

        public IEnumerable<EnrollmentViewModel> Enrollments { get; set; }
    }
}