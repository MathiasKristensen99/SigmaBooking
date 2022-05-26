<template>
  <div class="container-fluid">
    <div class="col">
      <div class="row calendar" style="justify-content: center">
        <div class="col-3" id="kalender-div" style="text-align: center">
          <Datepicker v-model="todaysDate" :value="date" @update:modelValue="handleDate"></Datepicker>
          <br>
          <button class="btn btn-secondary me-2"
                  type="button"
                  data-bs-toggle="modal"
                  data-bs-target="#addBooking"
          >Tilføj ny reservation
          </button>
        </div>
      </div>
      <div class="row gadgetRow">
          <div class="container-fluid btnDiv">
          <h2 class="me-5">Reservationer</h2>
            <div class="col-12">
          <button class="btn btn-secondary me-2 bi bi-alarm" v-if="sortTime" type="button" @click="sortTime">
            <svg xmlns="http://www.w3.org/2000/svg" width="15" height="15" fill="currentColor" class="bi bi-alarm" viewBox="1 1 16 16">
              <path d="M8.5 5.5a.5.5 0 0 0-1 0v3.362l-1.429 2.38a.5.5 0 1 0 .858.515l1.5-2.5A.5.5 0 0 0 8.5 9V5.5z"/>
              <path d="M6.5 0a.5.5 0 0 0 0 1H7v1.07a7.001 7.001 0 0 0-3.273 12.474l-.602.602a.5.5 0 0 0 .707.708l.746-.746A6.97 6.97 0 0 0 8 16a6.97 6.97 0 0 0 3.422-.892l.746.746a.5.5 0 0 0 .707-.708l-.601-.602A7.001 7.001 0 0 0 9 2.07V1h.5a.5.5 0 0 0 0-1h-3zm1.038 3.018a6.093 6.093 0 0 1 .924 0 6 6 0 1 1-.924 0zM0 3.5c0 .753.333 1.429.86 1.887A8.035 8.035 0 0 1 4.387 1.86 2.5 2.5 0 0 0 0 3.5zM13.5 1c-.753 0-1.429.333-1.887.86a8.035 8.035 0 0 1 3.527 3.527A2.5 2.5 0 0 0 13.5 1z"/>
            </svg>
              Sorter Tid
          </button>
          <button class="btn btn-secondary me-2" type="button" @click="sortName">
            Sorter Navn
          </button>
          <button class="btn btn-secondary me-2" type="button" @click="sortBodyCount">
            Sorter Antal
          </button>
        </div>
        </div>
        <div class="row g-3">
          <div class="col-12 col-sm-6 col-md-3" v-for="(booking, index) in bookings"
          v-bind:key="index">
            <div class="card">
              <div class="card-header">
                Bord: {{ booking.table.i}}
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
                  onclick="return false;"
                  v-model="booking.isEating"
                  id="skalSpise"
                />
                <hr class="solid" />
              </div>
              <div class="card-body">
                Note: {{booking.description}}
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
                  Tlf nr. {{ booking.phone}}
                  <hr class="solid" />
                </div>
                <div class="card-body">
                  Email: {{ booking.email}}
                  <hr class="solid" />
                </div>
                <div>
                  <button class="btn btn-success change"
                          data-bs-toggle="modal"
                          data-bs-target="#updateBooking" @click="getBookingById(booking.id)">
                    Rediger</button>
                </div>
                <div>
                  <button class="btn btn-danger delete" @click="deleteBooking(booking.id)">Slet</button>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>





  <div class="modal" id="addBooking">
    <div class="modal-dialog card">
      <div class="modal-content create_background">
        <!-- Modal Header -->
        <div class="modal-header">
          <h4 class="modal-title">Tilføj ny reservation</h4>
          <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
          ></button>
        </div>
        <input type="text" class="form-control" v-model="inputName" placeholder="Navn"> <br/>
        <input type="text" class="form-control" v-model="inputPhone" placeholder="Tlf nr"> <br/>
        <select class="form-control" v-model="tableId">
          <option value="" selected disabled>Vælg bord</option>
          <option v-for="table in tables" :value="table.id.toString()" v-bind:key="inputName">{{table.i}}</option>
        </select> <br/>
        <input type="text" class="form-control" v-model="inputEmail" placeholder="Email"> <br/>
        <input type="text" class="form-control" v-model="inputStartTime" placeholder="Start tidspunkt"> <br/>
        <input type="text" class="form-control" v-model="inputEndTime" placeholder="Slut tidspunkt"> <br/>
        <div>
          Spise
          <input class="form-check-input" type="checkbox" v-model="inputIsEating">
        </div>
        <input type="number" class="form-control antal_personer" v-model="peopleCount" placeholder="Antal personer"> <br/>
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





  <div class="modal" id="updateBooking">
    <div class="modal-dialog card">
      <div class="modal-content create_background">
        <!-- Modal Header -->
        <div class="modal-header">
          <h4 class="modal-title">Rediger reservation</h4>
          <button
              type="button"
              class="btn-close"
              data-bs-dismiss="modal"
          ></button>
        </div>
        <input type="text" class="form-control" v-model="updateInputName" placeholder="Navn"> <br/>
        <input type="text" class="form-control" v-model="updateInputPhone" placeholder="Tlf nr"><br/>
        <Datepicker v-model="todaysDate" :value="date" @update:modelValue="handleUpdateDate"></Datepicker><br/>
        <select class="form-control" v-model="updateTableId">
          <option value="" selected disabled>Vælg bord</option>
          <option v-for="table in tables" :value="table.id.toString()" v-bind:key="updateInputName">{{table.i}}</option>
        </select> <br/>
        <input type="text" class="form-control" v-model="updateInputEmail" placeholder="Email"><br/>
        <input type="text" class="form-control" v-model="updateInputStartTime" placeholder="Start tidspunkt"><br/>
        <input type="text" class="form-control" v-model="updateInputEndTime" placeholder="Slut tidspunkt"><br/>
        <div>
          Spise
          <input class="form-check-input" type="checkbox" v-model="inputIsEating">
        </div>
        <input type="number" class="form-control" v-model="updatePeopleCount" placeholder="Antal personer"><br/>
        <input type="text" class="form-control" v-model="updateInputDescription" placeholder="Beskrivelse">
        <!-- Modal footer -->
        <div class="modal-footer">
          <button
              type="button"
              class="btn btn-secondary me-3"
              data-bs-dismiss="modal"
              @click="updateBooking"
          >
            Gem
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import Datepicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import {BookingStore} from "../stores/bookingStore";
import {ref} from "vue";
import {Booking} from "../models/Booking";
import {TableLayoutService} from "../services/tableLayout.service";
import {BookingService} from "../services/booking.service";
const bookingStore = BookingStore();
const tableLayoutService: TableLayoutService = new TableLayoutService();
const bookingService: BookingService = new BookingService();


const date = ref(getCurrentDate());
const todaysDate = new Date()
const inputName = ref("");
const inputEmail = ref("");
const inputPhone = ref("");
const inputDescription = ref("");
const inputIsEating = ref();
const inputStartTime = ref("");
const inputEndTime = ref("");
const tableId = ref("");
const peopleCount = ref();
const clickCount = ref(0);


const handleDate = (modelData) => {
  date.value = modelData;
  const dd = String(modelData.getDate()).padStart(2, "0");
  const mm = String(modelData.getMonth() + 1).padStart(2, "0");
  const yyyy = modelData.getFullYear();
  const datePicked = dd + mm + yyyy
  date.value = datePicked;

  bookingStore.getBookings(datePicked);
}

function sortTime(){
  console.log(clickCount.value);
  if(clickCount.value === 0){
    bookingStore.bookings.sort((a,b) =>(a.startTime < b.startTime ? -1 :1));
    clickCount.value = 1;
  }
  else{
    bookingStore.bookings.sort((a,b) =>(a.startTime > b.startTime ? -1 :1));
    clickCount.value = 0;
  }

}

function sortName() {
  if(clickCount.value === 0){
    bookingStore.bookings.sort((a, b) =>(a.name < b.name ? -1 :1));
    clickCount.value = 1;
  }
  else {
    bookingStore.bookings.sort((a, b) =>(a.name > b.name ? -1 :1));
    clickCount.value = 0;
  }
}

function sortBodyCount(){
  if(clickCount.value === 0){
    bookingStore.bookings.sort((a, b) =>(a.peopleCount < b.peopleCount ? -1 :1));
    clickCount.value = 1;
  }
  else {
    bookingStore.bookings.sort((a, b) =>(a.peopleCount > b.peopleCount ? -1 :1));
    clickCount.value = 0;
  }
}

let tables = [];
getTables();
bookingStore.getBookings(date.value);
const bookings = ref<Booking[]>(bookingStore.bookingsFromDate);

const handleUpdateDate = (modelData) => {
  date.value = modelData;
  const dd = String(modelData.getDate()).padStart(2, "0");
  const mm = String(modelData.getMonth() + 1).padStart(2, "0");
  const yyyy = modelData.getFullYear();
  const datePicked = dd  + mm  + yyyy
  updateDate.value = datePicked;
}

const updateDate = ref(getCurrentDate());
const updateInputName = ref("");
const updateInputEmail = ref("");
const updateInputPhone = ref("");
const updateInputDescription = ref("");
const updateInputIsEating = ref();
const updateInputStartTime = ref("");
const updateInputEndTime = ref("");
const updateTableId = ref("");
const updatePeopleCount = ref();

function getTables() {
  tableLayoutService.getTableLayoutByDate(date.value)
      .then(value => {
        for (const table of value.tables) {
          const newTable = {
            id: table.id,
            isStatic: table.isStatic,
            i: table.i,
            x: table.x,
            y: table.y,
            w: table.w,
            h: table.h
          };
          tables.push(newTable);
        }
      }).catch((err) => console.log(err));
}

function createBooking() {
  bookingStore.createBooking(
      tableId.value,
      inputName.value,
      inputPhone.value,
      inputEmail.value,
      date.value,
      peopleCount.value,
      inputStartTime.value,
      inputEndTime.value,
      inputIsEating.value,
      inputDescription.value);

  console.log(bookingStore.bookingsFromDate);
}

function updateBooking() {
  bookingStore.updateBooking(
      bookingStore.booking.id,
      updateTableId.value,
      updateInputName.value,
      updateInputPhone.value,
      updateInputEmail.value,
      updateDate.value,
      updatePeopleCount.value,
      updateInputStartTime.value,
      updateInputEndTime.value,
      updateInputIsEating.value,
      updateInputDescription.value
  );
}

function getCurrentDate(): string {
  let today = new Date();
  const dd = String(today.getDate()).padStart(2, "0");
  const mm = String(today.getMonth() + 1).padStart(2, "0");
  const yyyy = today.getFullYear();
  return dd  + mm  + yyyy;
}


function deleteBooking(id: string) {
  bookingStore.deleteBooking(id);
}

function getBookingById(id: string) {
  bookingStore.getBookingById(id);
  console.log(bookingStore.booking);
  updateInputName.value = bookingStore.booking.name;
  updateInputPhone.value = bookingStore.booking.phone;
  updateInputEmail.value = bookingStore.booking.email;
  updateDate.value = bookingStore.booking.date;
  updateInputStartTime.value = bookingStore.booking.startTime;
  updateInputEndTime.value = bookingStore.booking.endTime;
  updateInputIsEating.value = bookingStore.booking.isEating;
  updateTableId.value = bookingStore.booking.tableId;
  updateInputDescription.value = bookingStore.booking.description;
  updatePeopleCount.value = bookingStore.booking.peopleCount;
}

</script>

<style scoped>

.btnDiv {
  margin-left: 10px;
  margin-top: 20px;
  display: flex;
  justify-content: space-between;
}

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
.create_background{
  background-color: transparent;
}
</style>
