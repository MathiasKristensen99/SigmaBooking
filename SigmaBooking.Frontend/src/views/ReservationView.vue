<template>
  <div class="container-fluid">
    <div class="col">
      <div class="row calendar" style="justify-content: center">
        <div class="col-3" id="kalender-div" style="text-align: center">
          <Datepicker v-model="date"></Datepicker>
          <br>
          <button class="btn btn-secondary me-2"
                  type="button"
                  data-bs-toggle="modal"
                  data-bs-target="#addBooking"
          >Tilføj ny reservation
          </button>
        </div>
      </div>

      <div class="container-fluid">
        <div>
          <h2>Reservationer</h2>
        </div>

        <div class="row g-3">
          <div class="col-12 col-md-6 col-lg-3" v-for="(booking, index) in bookings"
          v-bind:key="index">
            <div class="card">
              <div class="card-header">
                Bord: {{ booking.table.i }}
                <hr class="solid" />
              </div>
              <div class="card-body">
                Navn: {{booking.name}}
                <hr class="solid" />
              </div>
              <div class="card-body">{{ booking.startTime}} - {{booking.endTime}} (px: {{booking.peopleCount}})</div>
              <div class="card-body">
                Spise
                <input
                  class="form-check-input"
                  type="checkbox"
                  v-model="booking.isEating"
                  id="skalSpise"
                />
                <hr class="solid" />
              </div>
              <div class="card-body">
                Notes: {{booking.description}}
                <hr class="solid" />
              </div>

              <div class="card-body">
                <button
                    id="btn_visMere"
                    :href="'#collapse' + index"
                  class="btn_visMere"
                  data-bs-toggle="collapse"
                  :data-target="'#collapse' + index"
                >
                  vis mere
                </button>
              </div>
              <div :id="'collapse' + index" class="collapse">
                <div class="card-body tel">
                  Tlf nr. {{ booking.phone }}
                  <hr class="solid" />
                </div>
                <div class="card-body">
                  Email: {{ booking.email }}
                  <hr class="solid" />
                </div>
                <div>
                  <button class="btn btn-success change">Rediger</button>
                </div>
                <div>
                  <button class="btn btn-danger delete">Slet</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
  <div class="modal" id="addBooking">
    <div class="modal-dialog">
      <div class="modal-content">
        <!-- Modal Header -->
        <div class="modal-header">
          <h4 class="modal-title">Tilføj ny reservation</h4>
          <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
          ></button>
        </div>
        <input type="text" class="form-control" v-model="tableId" placeholder="Bord"> <br/>
        <input type="text" class="form-control" v-model="inputName" placeholder="Navn"> <br/>
        <input type="text" class="form-control" v-model="inputPhone" placeholder="Tlf nr"> <br/>
        <input type="text" class="form-control" v-model="inputEmail" placeholder="Email"> <br/>
        <input type="text" class="form-control" v-model="inputStartTime" placeholder="Start tidspunkt"> <br/>
        <input type="text" class="form-control" v-model="inputEndTime" placeholder="Slut tidspunkt"> <br/>
        <div class="form-check">
          <input class="form-check-input" type="checkbox" v-model="inputIsEating" />
          <label class="form-check-label">Spise</label>
        </div> <br/>
        <input type="number" class="form-control" v-model="peopleCount" placeholder="Antal personer"> <br/>
        <input type="text" class="form-control" v-model="inputDescription" placeholder="Beskrivelse">
        <!-- Modal footer -->
        <div class="modal-footer">
          <button
              type="button"
              class="btn btn-secondary me-3"
              data-bs-dismiss="modal"
              @click="createBooking"
          >
            Gem
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<style scoped>
.kalender-div {
  text-align: center;
}
.calendar {
  margin-top: 110px;
}

.card {
  text-align: center;
  border-radius: 20px;
  border: 2px solid #181818;
}
.card-header {
  -moz-border-radius-topleft: 20px;
  -moz-border-radius-topright: 20px;
  border-bottom: 0px;
  background-color: transparent;
}
.card-body {
  margin-top: -35px;
}
.btn_visMere {
  background-color: transparent;
  border: 0px;
  text-decoration: underline;
}
.delete {
  margin-bottom: 10px;
}
.change {
  margin-top: -35px;
}
.tel {
  margin-top: -25px;
}

</style>

<script setup lang="ts">
import Datepicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
//import axios from "axios";
import {BookingStore} from "../stores/bookingStore";
import {ref} from "vue";
import {BookingService} from "../services/booking.service";
import {Booking} from "../models/Booking";
const bookingStore = BookingStore();
const bookingService: BookingService = new BookingService();
bookingStore.getBookings(getCurrentDate_HttpFormat());
console.log(bookingStore.bookingsFromDate)
const bookings = ref<Booking[]>(bookingStore.bookingsFromDate);

const date = new Date();
const inputName = ref("");
const inputEmail = ref("");
const inputPhone = ref("");
const inputDescription = ref("");
const inputIsEating = ref();
const inputStartTime = ref("");
const inputEndTime = ref("");
const tableId = ref("");
const peopleCount = ref();

function getBookings() {
  bookingService.getBookingsFromDate(getCurrentDate_HttpFormat()).then(value =>
      value.map((booking) => {
        bookings.value.push(booking)
      })
    );
}

function createBooking() {
  bookingStore.createBooking(
      tableId.value,
      inputName.value,
      inputPhone.value,
      inputEmail.value,
      getCurrentDate_HttpFormat(),
      peopleCount.value,
      inputStartTime.value,
      inputEndTime.value,
      inputIsEating.value,
      inputDescription.value);

  console.log(bookingStore.bookingsFromDate);
}

function getCurrentDate_HttpFormat(): string {
  let today = new Date();
  const dd = String(today.getDate()).padStart(2, "0");
  const mm = String(today.getMonth() + 1).padStart(2, "0");
  const yyyy = today.getFullYear();
  return dd + "%2F" + mm + "%2F" + yyyy;
}




/*
export default {

  name: "ReservationView",
  components: { Datepicker },
  data() {
    return {
      date: null,
      reservations: ["e", "d", "l", "d", "ds"],
    };
  },

  methods: {
    getCurrentDate_HttpFormat() {
      let today = new Date();
      const dd = String(today.getDate()).padStart(2, "0");
      const mm = String(today.getMonth() + 1).padStart(2, "0");
      const yyyy = today.getFullYear();
      today = dd + "%2F" + mm + "%2F" + yyyy;
      return today;
    },

    getReservations() {
      axios
        .get(
          "https://localhost:7026/api/Bookings/date/" +
            this.getCurrentDate_HttpFormat().toString()
        )
        .then((response) => {
          console.log(response.data);
        });
    },
  },
  */
</script>
