using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts_Assignment.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("E-mail")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Category")]
        [Required]
        public string Category { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Role")]
        public string Subcategory { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Phone Number")]
        [Phone]
        [Required]
        public string TelephoneNumber { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Birth")]
        [Required]
        public DateTime DoB { get; set; }

    }
}
