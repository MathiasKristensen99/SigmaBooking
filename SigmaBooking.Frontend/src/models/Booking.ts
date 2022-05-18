export interface Booking {
    id: string;
    tableId: string;
    name: string;
    phone: string;
    email: string;
    date: string;
    peopleCount: number;
    startTime: string;
    endTime: string;
    isEating: boolean;
    description: string;
}