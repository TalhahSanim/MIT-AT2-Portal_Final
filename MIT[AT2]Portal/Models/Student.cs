using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace MIT_AT2_Portal.Models
{
    public class Student
    {
        public long Id { get; set; }

        [Display(Name ="Name")]
        [Required(ErrorMessage ="Please Enter Name")]
        [MinLength(1,ErrorMessage = "Name must have 1 character")]
        public string? fullName { get; set; }
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage = "Please Enter Date of Birth")]
        public DateTime? dateOfBirth { get; set; }

        [Display(Name = "Qualification")]
        [Required(ErrorMessage = "Please Select Qualification")]
        public string? qualification { get; set; }

        public List<SelectListItem> qualificationList = new List<SelectListItem>();
        [Display(Name = "GPA")]
        [Range(3.0,4.0,ErrorMessage ="GPA Must be Minimum 3.0 out of 4.0")]
        [Required(ErrorMessage = "Please Enter GPA")]
        
        public double gpa { get; set; }
        [Display(Name = "University")]
        [Required(ErrorMessage = "Please Select University")]

        public string? university { get; set; }


        public List<SelectListItem> uniList = new List<SelectListItem>();
    }
}
