export class Timesheet {
}

export class ClientInfo {
    clientId: number;
    clientName: string;
    checked:false;
    projectData: ProjectInfoList[];
   }

export class ProjectInfoList {
    projectId: number;
    projectName: string;
    isSelected:false;
}

