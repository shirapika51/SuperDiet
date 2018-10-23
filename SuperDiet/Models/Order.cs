using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SuperDiet.Models
{
    public class Order
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public DateTime Date { get; set; }
        public int Total { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual User User { get; set; }
    }
}
