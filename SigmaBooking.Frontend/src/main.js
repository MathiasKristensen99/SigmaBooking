import { createApp } from "vue";
import { createPinia } from "pinia";
import VueGridLayout from "vue-grid-layout";
import App from "./App.vue";
import router from "./router";

import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import Navbar from "./views/shared/Navbar.vue";

const app = createApp(App);
app.component("Navbar", Navbar);
app.use(createPinia());
app.use(router);
app.use(VueGridLayout);

app.mount("#app");
