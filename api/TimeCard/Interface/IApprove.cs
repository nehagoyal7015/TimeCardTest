using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeCard.DTO;

namespace TimeCard.Interface
{
    public interface IApprove
    {
        List<AppReport> SearchReport(AppSearchDto data);

        List<ApproveReport> EmployeeDetail(ApproveSearchDto data, int TaskId);

        List<ApprovalClientProjectInfo> AppClientProjectDetail(int clientId, int userId);

  
        List<taskProject> TaskListProject(int ProjeectId);

        List<EmpDetailApp> AppEmployeeEnteredTaskList(int taskId, int userId);

        List<Clients1> GetRecentApprovals(int userId);

        /* List<AppClientProjectInfo> ApproveclientProjectDetail(int clientId, int userId);*/
        /*List<AppEmployeeProjectDetails> AppEmployeeProjectInfo(int taskId);*/
    }
}
