using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workForm.Tables
{
    public class Project
    {
        [Key]
        public int IDproject { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public int idCustomer { get; set; }
        public int idUser { get; set; }

    }
}
