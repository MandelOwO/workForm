using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace workForm.dataBindings
{
    public class WorksDataModel : IListSource
    {
        public BindingList<Tables.Work> Data { get; set; } = new BindingList<Tables.Work>();
        public MyContext context { get; set; } = new MyContext();

        public bool ContainsListCollection => throw new NotImplementedException();

        public WorksDataModel(Tables.Project project)
        {

            
            context.tbWorks.Where(x => x.idProject == project.IDproject).ToList();
            var work = context.tbWorks.Where(x => x.idProject == project.IDproject).ToList();
            Data = new BindingList<Tables.Work>(work.ToList());


            /*
            context.tbWorks.ToList();
            Data = context.tbWorks.Local.ToBindingList();
            */
        }
        public IList GetList()
        {
            return this.Data;
            throw new NotImplementedException();

        }
    }
}
