using System.Collections.Generic;

namespace TimeCard.DTO
{
    public class ProjectUserEntity
    {

        
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public bool IsActive { get; set; }
        public proj Project { get; set; }
        // public int DomainIdOfProj { get; set; }
        // public string ProjectNoteTitle { get; set; }
        // public int ProjectNoteId { get; set; }
        // public string ProjectNote { get; set; }

        // public int TaskId { get; set; }
        // public string TaskName { get; set; }
        // public string TaskDescription { get; set; }

        // public int ProjectId { get; set; }
        // public string ProjectName { get; set; }
        // public string ProjectDescription { get; set; }
        // public bool ProjectIsActive { get; set; }
        // public int DomainIdOfProj { get; set; }
        // public string ProjectNoteTitle { get; set; }
        // public int ProjectNoteId { get; set; }
        // public string ProjectNote { get; set; }

        // public int TaskId { get; set; }
        // public string TaskName { get; set; }
        // public string TaskDescription { get; set; }

    }

    public class proj {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ProjectDescription { get; set; }
        public bool ProjectIsActive { get; set; }
    }
    public class Clients
    {
        //internal object project;

        public int ClientId { get; set; }
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public string ClientName { get; set; }
        public List<Projects> Project { get; set; }
    }

    public class Projects
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public bool Checked { get; set; }
        public List<ProjectTasks> ProjectTask { get; set; }
    }

    public class ProjectTasks
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public bool? IsArchived { get; set; }
    }



    public class GetRecentProj
    {
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public List<ProjectTasks> ProjectTask { get; set; }
    }
}
