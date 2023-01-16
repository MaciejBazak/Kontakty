using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Contacts_Assignment.Models
{
    public class Subcategory
    {

        [Key]
        public int SubcategoryId { get; set; }

        [Column(TypeName = "nvarchar(100)")]
        public string SubcategoryName { get; set; }
    }
}
