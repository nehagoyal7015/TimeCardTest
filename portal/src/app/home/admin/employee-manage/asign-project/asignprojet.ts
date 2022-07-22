export class Clients {
    clientId: number;
    clientName: string;
    total: number;
    asignTotal: number;
    projectdata: Projects[];
}

export class Projects {
    projectId: number;
    projectName: string;
    isSelected: boolean;
}

export class addProject{
 projectId : number;
 userId : number;
}