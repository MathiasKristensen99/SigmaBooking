import { defineStore } from "pinia";
import {BookingService} from "../services/booking.service";
import {Booking} from "../models/Booking";

const bookingService: BookingService = new BookingService();

export const BookingStore = defineStore({
    id: "bookingStore",
    state: () => ({
        bookings: [] as Booking[],
    }),
    getters: {
        bookingsFromDate: (state) => {
            if (state.bookings) return state.bookings;
            else return null;
        },
    },
    actions: {
        getBookings(date: string) {
            bookingService.getBookingsFromDate(date).then(
                (bookings) => {
                    for (const item of bookings) {
                        this.bookings.push(item);
                    }
                }
            ).catch((err) => console.log(err));
        },
        createBooking(
            tableId: string,
            name: string,
            phone: string,
            email: string,
            date: string,
            peopleCount: number,
            startTime: string,
            endTime: string,
            isEating: boolean,
            description: string) {
            bookingService.createBooking(tableId, name, phone, email, date, peopleCount, startTime, endTime, isEating, description)
                .then((booking => (this.bookings.push(booking)))).catch((err) => console.log(err));
        },
    },
});