<template>
  <div class="container-fluid">
    <div class="col">
      <div class="row calendar" style="justify-content: center">
        <div class="col-3" id="kalender-div" style="text-align: center">
          <Datepicker v-model="date"></Datepicker>
        </div>
      </div>

      <div class="container">
        <div>
          <h2>Reservationer</h2>
        </div>

        <div class="row">
          <div class="col-3" v-for="reservation in reservations">
            <div class="card">
              <div class="card-header">
                Bord: 1
                <hr class="solid" />
              </div>
              <div class="card-body">
                Navn:
                <hr class="solid" />
              </div>
              <div class="card-body">ankomst : slut (antal px)</div>
              <div class="card-body">
                Spise
                <input
                  class="form-check-input"
                  type="checkbox"
                  value=""
                  id="skalSpise"
                />
                <hr class="solid" />
              </div>
              <div class="card-body">
                Notes:
                <hr class="solid" />
              </div>

              <div class="card-body">
                <button
                  class="btn_visMere"
                  data-bs-toggle="collapse"
                  data-bs-target="#visMere">
                  vis mere
                </button>
              </div>
              <div id="visMere" class="collapse">
                <div class="card-body tel">
                  tel, nr. 123445677
                  <hr class="solid" />
                </div>
                <div class="card-body">
                  Email: KarlLutz69@gmail.ass
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

<script>

import Datepicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import axios from "axios";

export default {
  name: "ReservationView",
  components: { Datepicker },
  data() {
    return {
      date: null,
      reservations: ["e", "d", "l", "d", "ds"],
    };
  },

methods:{
  getCurrentDate_HttpFormat() {
    let today = new Date();
    const dd = String(today.getDate()).padStart(2, "0");
    const mm = String(today.getMonth() + 1).padStart(2, "0");
    const yyyy = today.getFullYear();
    today = dd + "%2F" + mm + "%2F" + yyyy;
    return today;
  },



    getReservations(){
      axios.get("https://localhost:7026/api/Bookings/date/" +
      this.getCurrentDate_HttpFormat().toString()
      )
          .then((response) => {
            console.log(response.data);
          })
    }


}
};
</script>
