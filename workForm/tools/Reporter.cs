using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using workForm.Tables;

namespace workForm.tools
{

    public class Reporter
    {
        public MyContext Context { get; set; } = new MyContext();

        public FileInfo File;

        public User CurrentUser { get; set; } = new User();
        public List<Customer> CustomerList { get; set; } = new List<Customer>();
        public List<Project> ProjectList { get; set; } = new List<Project>();
        public List<Work> WorkList { get; set; } = new List<Work>();

        public Reporter(User usr, string file)
        {
            CurrentUser = usr;
            File = new FileInfo(file);
            LoadData();


        }
        private class SummedWork
        {
            public SummedWork(string description, double duration, int idProject)
            {
                Description = description;
                Duration = duration;
                this.idProject = idProject;
            }
            public string Description { get; set; }
            public double Duration { get; set; }
            public int idProject { get; set; }
        }

        public void GenerateSummaryReport()
        {
            HtmlBegin("Summary Report");

            HtmlBodySummed();

            HtmlEnd();
        }

        private void HtmlBodySummed()
        {

            using (StreamWriter sw = File.AppendText())
            {
                foreach (var customer in CustomerList)
                {
                    sw.WriteLine($"<h1>{customer.Name}</h1>");

                    foreach (var project in ProjectList)
                    {
                        if (project.idCustomer == customer.IDcustomer)
                        {
                            double totalDuration = 0;
                            double totalPrice = 0;

                            sw.WriteLine($"<h2>{project.Name}</h2>");

                            sw.WriteLine($"<table><tr>" +
                                        $"<th>Work</th><th>Duration</th><th>Price</th>" +
                                        $"</tr>");
                            foreach (var work in GroupWorks())
                            {

                                if (work.idProject == project.IDproject)
                                {
                                    totalDuration += work.Duration;
                                    totalPrice += work.Duration * project.Rate;
                                    sw.WriteLine($"<tr>" +
                                        $"<td>{work.Description}</td><td>{(TimeSpan.FromHours((double)work.Duration).ToString())}</td><td>{(work.Duration * project.Rate).ToString("n")} Kč</td>" +
                                        $"</tr>");
                                }

                            }
                            sw.WriteLine($"<tr> " +
                                        $"<th>Total</th><th>{(TimeSpan.FromHours((double)totalDuration).ToString())}</th><th>{totalPrice.ToString("n")} Kč</th>" +
                                        $"</tr>");

                            sw.WriteLine("</table>");
                        }
                    }
                }
            }
        }

        private List<SummedWork> GroupWorks()
        {
            var filteredList = WorkList
    .GroupBy(x => new { x.Descripton, x.idProject })
    .Select(a => new
    {
        Description = a.Key.Descripton,
        Duration = a.Sum(x => x.Duration.TotalHours),
        idProject = a.Key.idProject
    });
            List<SummedWork> workList = new List<SummedWork>();
            foreach (var item in filteredList)
            {
                SummedWork work = new SummedWork(item.Description, item.Duration, item.idProject);
                workList.Add(work);
            }
            return workList;
        }

        public void GenerateDetailedReport()
        {
            HtmlBegin("Detailed Report");

            HtmlBodyDetailed(WorkList);

            HtmlEnd();


        }

        private void LoadData()
        {
            var projects = Context.tbProjects.Where(x => x.idUser == CurrentUser.IDuser).ToList<Project>();
            var customers = Context.tbCustomers.ToList<Customer>();
            var works = Context.tbWorks.ToList<Work>();

            List<Customer> customerList = new List<Customer>();

            foreach (var project in projects)
            {
                ProjectList.Add(project);
                foreach (var customer in customers)
                {
                    if (project.idCustomer == customer.IDcustomer)
                        customerList.Add(customer);
                }
                foreach (var work in works)
                {
                    if (work.idProject == project.IDproject)
                        WorkList.Add(work);
                }
            }
            CustomerList = customerList.Distinct().ToList();

        }

        private void HtmlBegin(string title)
        {
            using (StreamWriter sw = File.CreateText())
            {
                sw.WriteLine($"<html><head><title>{title}</title>");

                sw.WriteLine($"<style>" +
                    "table, th, td {border: 1px solid;}" +
                    "table {border-collapse: collapse;}" +
                    "th {background-color: #e8e8e8}" +
                    "th, td {padding: 0.5rem;}" +
                    $"</style></head>");

                sw.WriteLine("<body>");
            }
        }

        private void HtmlBodyDetailed(List<Work> workList)
        {

            using (StreamWriter sw = File.AppendText())
            {
                foreach (var customer in CustomerList)
                {
                    sw.WriteLine($"<h1>{customer.Name}</h1>");

                    foreach (var project in ProjectList)
                    {
                        if (project.idCustomer == customer.IDcustomer)
                        {
                            TimeSpan totalDuration = new TimeSpan();
                            double totalPrice = 0;

                            sw.WriteLine($"<h2>{project.Name}</h2>");

                            sw.WriteLine($"<table><tr>" +
                                        $"<th>Work</th><th>Begin</th><th>End</th><th>Duration</th><th>Price</th>" +
                                        $"</tr>");
                            foreach (var work in workList)
                            {

                                if (work.idProject == project.IDproject)
                                {
                                    totalDuration = totalDuration.Add(work.Duration);
                                    totalPrice += work.Duration.TotalHours * project.Rate;
                                    sw.WriteLine($"<tr>" +
                                        $"<td>{work.Descripton}</td><td>{work.Start}</td><td>{work.End}</td><td>{work.Duration.ToString()}</td><td>{(work.Duration.TotalHours * project.Rate).ToString("n")} Kč</td>" +
                                        $"</tr>");
                                }

                            }

                            sw.WriteLine($"<tr> " +
                            $"<th>Total</th><th></th><th></th><th>{totalDuration.ToString()}</th><th>{totalPrice.ToString("n")} Kč</th>" +
                            $"</tr>");

                            sw.WriteLine("</table>");
                        }
                    }
                }
            }
        }

        private void HtmlEnd()
        {
            using (StreamWriter sw = File.AppendText())
            {
                sw.WriteLine("</body></html>");
            }
        }
    }
}