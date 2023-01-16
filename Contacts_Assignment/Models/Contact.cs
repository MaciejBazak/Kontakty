using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts_Assignment.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        //Pole przeznaczone na imie, max dlugosc 100 znaków, wymagane

        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("First Name")]
        [Required]
        public string FirstName { get; set; }


        //Pole przeznaczone na nazwisko, max dlugosc 100 znaków, wymagane
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Last Name")]
        [Required]
        public string LastName { get; set; }

        //Pole przeznaczone na email, max dlugosc 100 znaków, wymagane, sprawdzanie czy poprawny email
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("E-mail")]
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        //Pole przeznaczone na kategorie, mogloby byc krotsze patrzac ze jest wybierane z listy, wymagane
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Category")]
        [Required]
        public string Category { get; set; }

        //Pole przeznaczone na podkategorie, max dlugosc 100 znaków
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Role")]
        public string Subcategory { get; set; }

        //Pole przeznaczone na numer telefonu
        [Column(TypeName = "nvarchar(100)")]
        [DisplayName("Phone Number")]
        [Phone]
        [Required]
        public string TelephoneNumber { get; set; }

        //Pole przeznaczone na date urodzin, w formacie dzień,miesiac, rok bez godziny, wymagane
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Date of Birth")]
        [Required]
        public DateTime DoB { get; set; }

    }
}
