using System.ComponentModel.DataAnnotations;

namespace ProjectPlan.Models
{
    public class Comment
    {
        public int Id
        {
            get; set;
        }

        [MinLength(3)]
        [Required]
        public string Body
        {
            get; set;
        }

        public virtual Group MyGroup
        {
            get; set;
        }


    }
}