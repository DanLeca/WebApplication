﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectPlan.Models
{
    public class Contact
    {
        public int Id
        {
            get; set;
        }

        public string FirstName
        {
            get; set;
        }

        public string LastName
        {
            get; set;
        }

        public virtual Group MyGroup
        {
            get; set;
        }
    }
}
