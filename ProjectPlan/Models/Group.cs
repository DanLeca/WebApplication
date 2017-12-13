using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectPlan.Models
{
    public class Group
    {
        [Required]
        public int Id
        {
            get; set;
        }

        [Required(ErrorMessage = "Enter a title for the blog post")]
        [StringLength(50, MinimumLength = 3)]
        public string Title
        {
            get; set;
        }

        [Required(ErrorMessage = "Enter a description for the blog post")]
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

        public int viewed
        {
            get; set;
        }
    }
}