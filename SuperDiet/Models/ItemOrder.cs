using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SuperDiet.Models
{
    public class ItemOrder
    {
        public int ItemID { get; set; }
        public Item Item { get; set; }
        public int OrderID { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
    }
}
