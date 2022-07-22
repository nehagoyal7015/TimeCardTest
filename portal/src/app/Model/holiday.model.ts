export class HolidayModel {
    holidayId: number;
    userOptHolidayId: number;
    holidayDate: Date ;    
    holidayReason: string ;
    holidayDay: string ;
    isOpting: boolean ;
    isFloating: boolean;
    isVoided: boolean;
    holidayType: string;
    optByAnyone: boolean;
    optBy: userOptHolidays[];

}

export class OptHolidayModel {
    holidayId: number;
    userOptHolidayId: number;
    isOpting: boolean ;
    

}

export class listEmpOptHoliday {
    userId: number;
    userName: string;
    userEmail: string;
    EmpId: string;
    isOpting: boolean ;
    isVoided: boolean;
}

export class userOptHolidays {
    
    userName: string;
    EmpId: string;
    isOpting: boolean ;
    isVoided: boolean;
}

