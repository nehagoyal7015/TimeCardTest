using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TimeCard.DTO;
using TimeCard.Models;

namespace TimeCard.DataAcess
{
    public class ProjectUserDataAccess: BaseDataAccess
    {
        public ProjectUserDataAccess(TimeCardAppContext dataContext) : base(dataContext)
        {
        }

        public List<Clients> GetUserProjects(int userId)
        {
            var projectInfo = (from projuser in DataContext.ProjectUsers
                               join project in DataContext.Projects on projuser.ProjectId equals project.ProjectId
                               join client in DataContext.Clients on project.ClientId equals client.ClientId
                               join projtask in DataContext.ProjectTasks on project.ProjectId equals projtask.ProjectId
                               where projuser.UserId == userId
                               select new
                               {
                                   ClientId = client.ClientId,
                                   ClientName = client.ClientName,
                                   ProjectId = project.ProjectId,
                                   ProjectName = project.Name,
                                   UserId = projuser.UserId,
                                   TaskId = projtask.TaskId,
                                   TaskName = projtask.Name,
                                   IsArchived = projtask.IsArchived,
                                   Checked = false
                               }).ToList();
            // Itrate for the client 

            var clients = new List<Clients>();
            foreach (var clientrec in projectInfo.ToList().GroupBy(s => new { s.ClientId, s.ClientName })){

                var client = new Clients() { ClientId = clientrec.Key.ClientId, ClientName = clientrec.Key.ClientName };
                client.Project = new List<Projects>();
                foreach (var prjRec in clientrec.ToList().GroupBy(s => new { s.ProjectId, s.ProjectName }))
                {
                    var project = new Projects() { ProjectId = prjRec.Key.ProjectId, ProjectName = prjRec.Key.ProjectName };
                    
                    project.ProjectTask = new List<ProjectTasks>();

                    
                    foreach (var taskrec in prjRec.Where(x => x.IsArchived == false).ToList())
                    {
                        // may be need to intiaizre ProjectTask object before add a record to it.

                        project.ProjectTask.Add(new ProjectTasks
                        {
                            TaskId = taskrec.TaskId,
                            TaskName = taskrec.TaskName
                        });
                    }
                    // may be need to intiaizre Projects object before add a record to it.
                    
                    client.Project.Add(project);
                }
                clients.Add(client);
            }
            return clients;

        }

        public List<GetRecentProj> GetRecentProjects(int userId) {

            var projInfo = new GetRecentProj();
            var projectInfo = (from projuser in DataContext.ProjectUsers
                               join project in DataContext.Projects on projuser.ProjectId equals project.ProjectId 
                          
                               orderby projuser.ModifiedOn descending 
                               where projuser.UserId == userId
                               select new GetRecentProj {
                                   ProjectId = project.ProjectId,
                                   ProjectName = project.Name,
                               }).Take(2).ToList();

            var projlist = new List<GetRecentProj>();
            foreach (var project in projectInfo)
            {
                var taskInfo = DataContext.ProjectTasks.Where(u => u.ProjectId == project.ProjectId).OrderByDescending(u => u.ModifiedOn)
                .Select(projtask => new ProjectTasks {
                    TaskId = projtask.TaskId,
                    TaskName = projtask.Name,
                    IsArchived = projtask.IsArchived
                }).ToList();

                project.ProjectTask = taskInfo;
                projlist.Add(project);
            }
            
            return projlist;
             
        }
    }
}








