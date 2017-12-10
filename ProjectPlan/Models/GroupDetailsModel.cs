using System.Collections.Generic;

namespace ProjectPlan.Models
{
    public class GroupDetailsModel
    {
        public Group Group { get; set; }
        public List<Contact> Contacts { get; set; }

        public int GroupID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
