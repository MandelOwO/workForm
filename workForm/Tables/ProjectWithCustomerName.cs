using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workForm.Tables
{
    public class ProjectWithCustomerName
    {

        public ProjectWithCustomerName()
        {

        }

        public ProjectWithCustomerName(int iDproject, string name, int rate, string customerNameString, bool completed)
        {
            IDproject = iDproject;
            Name = name;
            Rate = rate;
            CustomerNameString = customerNameString;
            Completed = completed;
        }

        public int IDproject { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public string CustomerNameString { get; set; }
        public bool Completed { get; set; }


    }
}
