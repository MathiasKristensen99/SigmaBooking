import {defineStore} from "pinia";
import {BookingService} from "../services/booking.service";
import {Booking} from "../models/Booking";

const bookingService: BookingService = new BookingService();

export const BookingStore = defineStore({
    id: "bookingStore",
    state: () => ({
        bookings: [] as Booking[],
        booking: {} as Booking,
    }),
    getters: {
        bookingsFromDate: (state) => {
            if (state.bookings) return state.bookings;
            else return null;
        },
        bookingName: (state) => {
            if (state.booking != undefined) return state.booking.name;
            else return null;
        },
    },
    actions: {
        getBookings(date: string) {
            bookingService.getBookingsFromDate(date).then(
                (bookings) => {
                    this.bookings.splice(0, this.bookings.length)
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
        deleteBooking(id: string) {
            bookingService.deleteBooking(id)
                .then((value) => {
                    this.bookings.forEach((booking, index) => {
                        if (booking.id === id) this.bookings.splice(index, 1);
                    });
                })
                .catch((err) => console.log(err));
        },
        getBookingById(id: string) {
            this.booking = this.bookings.find(value => value.id === id);
        },
        updateBooking(
            id: string,
            tableId: string,
            name: string,
            phone: string,
            email: string,
            date: string,
            peopleCount: number,
            startTime: string,
            endTime: string,
            isEating: boolean,
            description: string
        ) {
            bookingService.updateBooking(id, tableId, name, phone, email, date, peopleCount, startTime, endTime, isEating, description)
                .then((res) => {
                    this.getBookings(date)
                })
                .catch((err) => console.log(err));
        }
    },
});