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

        public ProjectWithCustomerName(int iDproject, string name, int rate, Customer customer, string customerNameString, User user, bool completed)
        {
            IDproject = iDproject;
            Name = name;
            Rate = rate;
            Customer = customer;
            CustomerNameString = customerNameString;
            User = user;
            Completed = completed;
        }

        public int IDproject { get; set; }
        public string Name { get; set; }
        public int Rate { get; set; }
        public Customer Customer { get; set; }
        public string CustomerNameString { get; set; }
        public User User { get; set; }
        public bool Completed { get; set; }


    }
}
