import http from "./http.client";
import { Booking } from "../models/Booking";

export class BookingService {
    async getBookingsFromDate(date: string): Promise<Booking[]> {
        const res = await http.get<Booking[]>(
            "api/Bookings/date/" + date
        );
        // @ts-ignore
        return res.data.list;
    }

    async createBooking(
        tableId: string,
        name: string,
        phone: string,
        email: string,
        date: string,
        peopleCount: number,
        startTime: string,
        endTime: string,
        isEating: boolean,
        description: string): Promise<Booking> {
        const res = await http.post("api/Bookings", {
            id: "string",
            tableId: tableId,
            name: name,
            phone: phone,
            email: email,
            date: date,
            startTime: startTime,
            endTime: endTime,
            isEating: isEating,
            description: description
        });
        return res.data;
    }

    async deleteBooking(id: string) {
        await http.delete("api/Bookings/" + id)
            .catch((res) => {
                return res.data
            })
            .then((err) => {
                console.log(err);
            });
    }
}
