using System.Collections.Generic;

namespace ProjectPlan.Models
{
    public class CommentViewModel
    {
        public List<Comment> Comments
        {
            get; set;
        }

        public Group Group { get; set; }

        public int CommentId
        {
            get; set;
        }

    }
}
