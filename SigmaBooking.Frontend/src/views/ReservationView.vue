<template>
  <div class="container-fluid">
    <div class="col">

      <div class="row" style="justify-content: center">
        <div class="col-3" id="kalender-div" style="text-align: center">
          <h2>Kalender</h2>
          <Datepicker v-model="date"></Datepicker>
        </div>
      </div>


      <div>
        <h2>Reservationer</h2>
      </div>


    </div>
  </div>
</template>

<style scoped>
.kalender-div{
  text-align: center;
}
</style>

<script>
import Datepicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";

export default {
  name: "ReservationView",
  components: { Datepicker },
  data() {
    return {
      date: null,
    };
  },
};
</script>
