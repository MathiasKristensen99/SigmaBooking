import { createApp } from "vue";
import { createPinia } from "pinia";
import VueGridLayout from "vue-grid-layout";
import App from "./App.vue";
import router from "./router";

<<<<<<< Updated upstream
import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";
import Navbar from "./views/shared/Navbar.vue";
=======
import "bootstrap/dist/css/bootstrap.min.css";
import "bootstrap";
>>>>>>> Stashed changes

const app = createApp(App);
app.component("Navbar", Navbar);
app.use(createPinia());
app.use(router);
app.use(VueGridLayout);

app.mount("#app");
