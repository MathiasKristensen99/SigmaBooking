<template>
  <div>Kalender</div>
  <Datepicker v-model="date"></Datepicker>
  <div>Reservations</div>
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
