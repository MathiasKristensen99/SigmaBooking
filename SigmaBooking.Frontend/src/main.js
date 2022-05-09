import { createApp } from "vue";
import { createPinia } from "pinia";
import VueGridLayout from "vue-grid-layout";
import App from "./App.vue";
import router from "./router";
import { BootstrapVue, IconsPlugin } from "bootstrap-vue";

import "bootstrap/dist/css/bootstrap.css";
import "bootstrap-vue/dist/bootstrap-vue.css";

const app = createApp(App);

app.use(BootstrapVue);
app.use(IconsPlugin);
app.use(createPinia());
app.use(router);
app.use(VueGridLayout);

app.mount("#app");
