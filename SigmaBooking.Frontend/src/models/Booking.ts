import {Table} from "./Table";

export interface Booking {
    id: string;
    table: Table;
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