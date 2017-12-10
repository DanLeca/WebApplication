namespace ProjectPlan.Models
{
    public class Comment
    {
        public int Id
        {
            get; set;
        }

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