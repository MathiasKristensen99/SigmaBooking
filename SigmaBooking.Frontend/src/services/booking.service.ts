import http from "./http.client";
import { Booking } from "../models/Booking";
import httpClient from "./http.client";

export class BookingService {
    async getBookingsFromDate(date: string): Promise<Booking[]> {
        const res = await http.get(
            "api/Bookings/date/" + date
        );
        return res.data.list;
    }
}
