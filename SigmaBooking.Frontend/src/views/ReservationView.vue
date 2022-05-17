<template>
  <div class="container-fluid">
    <div class="col">

        <div class="row" style="justify-content: center">
          <div class="col-3" id="kalender-div" style="text-align: center">
            <h2>Kalender</h2>
            <Datepicker v-model="date"></Datepicker>
          </div>
        </div>

      <div class="container">
        <div>
          <h2>Reservationer</h2>
        </div>

        <div class="row" v-for="reservation in reservations">
          <div class="col-3">
            <div class="card">
              <div class="card-header">
                Bord: 1
                <hr class="solid">
              </div>
              <div class="card-body">
                Navn:
              </div>
              <div class="card-body">
                Efternavn:
                <hr class="solid">
              </div>
              <div class="card-body">
                ankomst : slut (antal px)
              </div>
              <div class="card-body">
                Spise
                <input class="form-check-input" type="checkbox" value="" id="skalSpise">
                <hr class="solid">
              </div>
              <div class="card-body">
                Notes:
                <hr class="solid">
              </div>


              <div class="card-body">
                <button class="btn_visMere" data-bs-toggle="collapse" data-bs-target="#visMere">
                  vis mere
                </button>
              </div>
              <div id="visMere" class="collapse">
                <div class="card-body">
                  tel, nr.
                  123445677
                  <hr class="solid">
                </div>
                <div class="card-body">
                  Email:
                  KarlLutz69@gmail.ass
                  <hr class="solid">
                </div>
                  <div>
                    <button class="btn btn-success change">
                      bearbejd
                    </button>
                  </div>
                <div>
                  <button class="btn btn-danger delete">
                    slet
                  </button>
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
.kalender-div{
  text-align: center;
}

.card{
  text-align: center;
  border-radius: 20px;
  border: 2px solid #181818;
}
.card-header{
  -moz-border-radius-topleft: 20px;
  -moz-border-radius-topright: 20px;
  border-bottom: 0px;
  background-color: transparent;
}
.card-body{
  margin-top: -35px;
}
.btn_visMere{
  background-color: transparent;
  border: 0px;
  text-decoration: underline;
}
.delete{
  margin-bottom: 10px;
}
.change{
  margin-top: -35px;
}

</style>

<script>
import Datepicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
//v-for="reservation in reservations"
export default {
  name: "ReservationView",
  components: { Datepicker },
  data() {
    return {
      date: null,
      reservations: [
          "manni das mammut",
          "Rød hår er sekset",
          "Hækser knepper bedst",
          "I like turtles"
      ]
    };
  },
};



</script>
