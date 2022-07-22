using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DataAcess;
using TimeCard.DTO;
using TimeCard.Interface;

namespace TimeCard.Manager
{
    public class ProjectInvoiceManager : BaseManager, IProjectInvoice
    {

        private readonly ProjectInvoiceDataAccess _projectInvoiceDataAccess;
        public ProjectInvoiceManager(ProjectInvoiceDataAccess dataAccess)
        {
            _projectInvoiceDataAccess = dataAccess;
        }
        public void AddProjectInvoice(ProjectInvoiceEntity projectInvoice)
        {
            _projectInvoiceDataAccess.AddProjectInvoice(projectInvoice);
            _projectInvoiceDataAccess.Commit();
        }
        public List<ProjectInvoiceEntity> GetProjectInvoice(int invoiceId)
        {
            return _projectInvoiceDataAccess.GetProjectInvoice(invoiceId);
        }
    }
}
