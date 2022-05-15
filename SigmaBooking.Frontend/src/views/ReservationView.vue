<template>
  <div class="col-3">
    <div>
      <h2>Kalender</h2>
      <Datepicker v-model="date"></Datepicker>
    </div>
    <div>
      <h2>Reservations</h2>
    </div>
  </div>
</template>

<style scoped></style>

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
