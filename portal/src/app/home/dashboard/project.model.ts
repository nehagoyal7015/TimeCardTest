import { Timestamp } from "rxjs/internal/operators/timestamp";

export class Clients {
    clientId: number
    clientName: string;
    project: Projects[];
}

export class Projects {
    projectId: number;
    projectName: string;
    checked :false;
    projectTask: ProjectTasks[];
}

export class ProjectTasks {
    taskId: number;
    taskName: string;
    isArchived : boolean;
}
export class TimeSheetEntity {
      timeSheetId: number;
      projectId: number;
      projectName: string;
      taskId: number;
      taskName: string;
      billableHours: number;
      billableHoursNote: string;
      nonBillableHours: number;
      nonBillableHoursNote: string;
      isBilled: boolean;
}

export class TimeSheetInpt {
    timeSheetId: number;
    projectId: number;
    taskId: number;
    requestDate: string;
    billableHours: number;
    billableHoursNote: string;
    nonBillableHours: number;
    nonBillableHoursNote: string;
    isBilled: boolean;
}



