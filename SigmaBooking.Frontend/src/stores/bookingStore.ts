import { defineStore } from "pinia";
import {BookingService} from "../services/booking.service";
import {Booking} from "../models/Booking";

const bookingService: BookingService = new BookingService();

export const BookingStore = defineStore({
    id: "bookingStore",
    state: () => ({
        bookings: {} as Booking[],
    }),
    getters: {
        bookingsFromDate: (state) => {
            if (state.bookings != undefined) return state.bookings;
            else return null;
        },
    },
    actions: {
        getBookings(date: string) {
            bookingService.getBookingsFromDate(date).then(
                (bookings) => (this.bookings = bookings)
            ).catch((err) => console.log(err));
        },
    },
});