using System.ComponentModel.DataAnnotations;

namespace TaskVahe.Models
{
    public class UserUpdateModel
    {
        public int Id { get; set; }
        [Required, MinLength(3)]
        [DataType(DataType.Text)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }


        [Required, MinLength(3)]
        [Display(Name = "LastName")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(256)]
        [DataType(DataType.Text)]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

    }
}
