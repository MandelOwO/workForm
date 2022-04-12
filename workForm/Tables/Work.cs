using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workForm.Tables
{
    public class Work
    {
        public Work()
        {
        }

        public Work(int iDwork, string descripton, DateTime start, DateTime end, int idProject, bool completed)
        {
            IDwork = iDwork;
            Descripton = descripton;
            Start = start;
            End = end;
            this.idProject = idProject;
            Completed = completed;
        }

        [Key]
        public int IDwork { get; set; }
        public string Descripton { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int idProject { get; set; }
        public bool Completed { get; set; }


    }
}
