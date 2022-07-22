using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
   public interface IProjectInvoice
    {
        void AddProjectInvoice(ProjectInvoiceEntity projectInvoice);
        List<ProjectInvoiceEntity> GetProjectInvoice(int invoiceId);
    }
}
