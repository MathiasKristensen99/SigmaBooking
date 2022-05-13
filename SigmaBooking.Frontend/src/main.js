import { createApp } from "vue";
import { createPinia } from "pinia";
import VueGridLayout from "vue-grid-layout";
import App from "./App.vue";
import router from "./router";

import Navbar from "./views/shared/Navbar.vue";

import Datepicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";

const app = createApp(App);
app.component("Navbar", Navbar);
app.component("Datepicker", Datepicker);
app.use(createPinia());
app.use(router);
app.use(VueGridLayout);

app.mount("#app");
