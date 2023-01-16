using Contacts_Assignment.Models;

namespace Contacts_Assignment.Models
{
    public class ContactEditViewModel
    {
        public Contact contact { get; set; }

        public List<Category> Categories { get; set; }
    }
}
