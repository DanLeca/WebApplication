using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
