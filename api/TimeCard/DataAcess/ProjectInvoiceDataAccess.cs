using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class ProjectInvoiceDataAccess : BaseDataAccess 
    {
        public ProjectInvoiceDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }

        public void AddProjectInvoice(ProjectInvoiceEntity projectInvoice)
        { 
               var InvoiceInfo = new ProjectInvoice();

               InvoiceInfo.ProjectId = DataContext.Projects.Where(p => p.ProjectId == p.ProjectId).Select(s => s.ProjectId).FirstOrDefault();
               InvoiceInfo.InvoiceYear = projectInvoice.InvoiceYear;
               InvoiceInfo.InvoiceDate = projectInvoice.InvoiceMonth;
               InvoiceInfo.TotalBillableHours = projectInvoice.TotalBillableHours;
               InvoiceInfo.TotalNonBillableHours = projectInvoice.TotalNonBillableHours;
               InvoiceInfo.Comment = projectInvoice.Comment;
               InvoiceInfo.CreatedBy = 1;
               InvoiceInfo.CreatedOn = DateTime.Now;
               InvoiceInfo.ModifiedBy = 1;
               InvoiceInfo.ModifiedOn = DateTime.Now;

                DataContext.ProjectInvoices.Add(InvoiceInfo);
                DataContext.SaveChanges();

        }
        public List<ProjectInvoiceEntity> GetProjectInvoice(int invoiceId)
        {


            List<ProjectInvoiceEntity> projectInvoice = (from p in DataContext.ProjectInvoices
                                                   where p.InvoiceId == invoiceId
                                                         select new ProjectInvoiceEntity
                                                      {
                                                         
                                                          InvoiceMonth = p.InvoiceMonth,
                                                          InvoiceYear = p.InvoiceYear,
                                                          InvoiceDate = p.InvoiceDate,
                                                          TotalBillableHours = p.TotalBillableHours,
                                                          TotalNonBillableHours = p.TotalNonBillableHours,
                                                          Comment = p.Comment,
                                                   }).ToList();

            return projectInvoice;

        }
    }
}
