export class Leave 
{
    leaveId : number; 
    leaveType : string;
    fromDate : Date;
    toDate  : Date;
    reason : string;
    cancelledDate : Date;
    total: number;
    isCancelled : boolean; 
    userId: number;
    userName: string;
    Unplanned: string;
    Holiday: string;
    Planned: string
    Floating: string;
    Total:number;
}

export class UpcomingLeaves
{
    userId: number;
    userName: string;
    fromDate : Date;
    toDate  : Date;
    reason : string;
    leaveType : string;
    EmpId: string;
    isOpting: boolean ;
    isVoided: boolean;
    isFloating: boolean;
    holidayId: number;
    holidayDate: Date ;  
    holidayReason: string ;
}

export class userCountList {
    plannedtotal: string;
    unplannedtotal: string ;    
    holidaytotal: string ;
    floatingtotal: string ;
    totalCount: boolean;
  }
