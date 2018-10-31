using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDiet.Models
{
    public class User
    {
        public int ID { get; set; }
        public String UserName { get; set; }
        public String Password { get; set; }
        public bool IsAdmin { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
