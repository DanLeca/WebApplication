using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPlan.Models
{
    public class Group
    {
        public int Id
        {
            get; set;
        }

        [Required]
        public string Title
        {
            get; set;
        }

        [Required]
        public string Description
        {
            get; set;
        }

        public ApplicationUser ApplicationUser
        {
            get; set;
        }

        public virtual ICollection<Comment> Comment
        {
            get; set;
        }
    }
}