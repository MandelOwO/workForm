using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workForm.Tables
{
    public class Customer
    {
        [Key]
        public int IDcustomer { get; set; }
        public string Name { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string PSC { get; set; }
        public string ICO { get; set; }
        public string DIC { get; set; }

    }
}
