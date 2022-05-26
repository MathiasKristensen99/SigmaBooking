<template>
  <div class="row gadgetRow">
    <div class="col-12 col-xl-3 col-lg-6">
      <div class="container-fluid btnDiv">
        <button class="btn btn-secondary" type="button" @click="addItem">
          Tilføj bord
        </button>
        <button
          class="btn btn-secondary"
          type="button"
          data-bs-toggle="modal"
          data-bs-target="#myModal"
          @click="updateTableLayout"
        >
          Gem bordopstilling
        </button>

        <button
          class="btn btn-secondary"
          type="button"
          data-bs-toggle="modal"
          data-bs-target="#loginModal"
          @click="createLayout"
        >
          Ny Standard
        </button>

        <div class="modal" id="loginModal">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h4 class="modal-title">Enter Credentials</h4>

                <div class="modal-body">
                  <input
                    v-model="credentials"
                    class="inputField"
                    type="text"
                    id="credentials"
                    form="credentials"
                  />
                </div>

                <button
                  type="submit"
                  class="submitCred"
                  data-bs-dismiss="modal"
                  @click="getCredentials()"
                >
                  Submit
                </button>
              </div>

              <div class="modal-footer">
                <button
                  type="button"
                  class="btn btn-danger"
                  data-bs-dismiss="modal"
                >
                  Close
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>

    <div class="col-12 col-md-6">
      <div>
        <Datepicker
          v-model="date"
          :value="date"
          @update:modelValue="handleDate"
        ></Datepicker>
      </div>
    </div>
  </div>

  <grid-layout
    v-model:layout="layout"
    :col-num="colNum"
    :row-height="40"
    :is-draggable="draggable"
    :is-resizable="resizable"
    :responsive="false"
    :vertical-compact="false"
    :prevent-collision="true"
    :use-css-transforms="true"
  >
    <grid-item
      v-for="item in layout"
      :static="item.static"
      :x="item.x"
      :y="item.y"
      :w="item.w"
      :h="item.h"
      :i="item.i"
    >
      <span class="text">
        <button class="click">{{ item.i }}</button>
      </span>
      <span class="remove" @click="removeItem(item.id)">x</span>
    </grid-item>
  </grid-layout>
  <div class="modal" id="myModal">
    <div class="modal-dialog">
      <div class="modal-content">
        <!-- Modal Header -->
        <div class="modal-header">
          <h4 class="modal-title">Bordopstilling gemt</h4>
          <button
            type="button"
            class="btn-close"
            data-bs-dismiss="modal"
          ></button>
        </div>
        <!-- Modal footer -->
        <div class="modal-footer">
          <button
            type="button"
            class="btn btn-secondary me-3"
            data-bs-dismiss="modal"
          >
            Luk
          </button>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { GridLayout, GridItem } from "vue-grid-layout";
import axios from "axios";
import Datepicker from "@vuepic/vue-datepicker";
import "@vuepic/vue-datepicker/dist/main.css";
import { ref } from "vue";

export default {
  components: {
    GridLayout,
    GridItem,
    Datepicker,
  },
  data() {
    return {
      layout: [],
      draggable: true,
      resizable: true,
      colNum: 50,
      index: 0,
      layoutId: "",
      layoutDate: "",
      isDefault: true,
      date: new Date(),
      credentials: "",
      loggedInId: "",
    };
  },
  mounted() {
    // this.$gridlayout.load();
    this.index = this.layout.length;
  },
  methods: {
    selectNewDate() {
      const datepicker = ref(null);
      if (datepicker.value) {
        console.log(datepicker.value.toString());
      }
    },
    addItem: function () {
      const item = {
        x: 0,
        y: 0,
        w: 3,
        h: 3,
        i: (this.layout.length + 1).toString(),
        static: false,
      };
      axios
        .post("https://sigmabooking.azurewebsites.net/api/Tables/", {
          x: item.x,
          y: item.y,
          w: item.w,
          h: item.h,
          i: item.i,
          id: "string",
        })
        .then((response) => {
          this.layout.push(response.data);
          this.updateTableLayout();
        })
        .catch((error) => {
          console.log(error);
        });
    },
    removeItem: function (val) {
      axios
        .delete("https://sigmabooking.azurewebsites.net/api/Tables/" + val)
        .then((response) => {
          console.log(response.data);
          const index = this.layout.map((item) => item.id).indexOf(val);
          this.layout.splice(index, 1);
          this.updateTableLayout();
        })
        .catch((error) => {
          console.log(error);
        });
    },

    getCredentials: function () {
      axios
        .get(
          "https://sigmabooking.azurewebsites.net/api/credentials/" +
            this.credentials
        )
        .then((response) => {
          if (response.data.credentials === this.credentials) {
            this.loggedInId = response.data.id;
            console.log(this.loggedInId);
            return true;
          }
          return false;
        });
    },

    createLayout() {
      if (!this.loggedInId === "") {
        let tables = [];
        for (const item of this.layout) {
          const table = {
            id: "string",
            x: item.x,
            y: item.y,
            w: item.w,
            h: item.h,
            i: item.i,
            static: item.static,
          };
          tables.push(table);
        }
        const tableLayout = {
          isDefault: false,
          date: this.currentDate().toString(),
          tables: tables,
        };
        console.log(tableLayout);
        axios
          .post("https://sigmabooking.azurewebsites.net/api/TableLayouts/", {
            id: "",
            isDefault: tableLayout.isDefault,
            date: tableLayout.date,
            tables: tableLayout.tables,
          })
          .then((response) => {
            console.log(response);
            this.layout = [];
            this.getLayout();
          })
          .catch((error) => {
            console.log(error);
          });
      }
    },

    getLayout() {
      axios
        .get(
          "https://sigmabooking.azurewebsites.net/api/TableLayouts/" +
            this.currentDate().toString()
        )
        .then((response) => {
          console.log(response.data);
          this.layoutDate = response.data.date;
          this.isDefault = response.data.isDefault;
          this.layoutId = response.data.id;
          for (const responseElement of response.data.tables) {
            this.layout.push(responseElement);
          }
        })
        .catch((error) => {
          console.log(error);
        });
    },

    updateTableLayout() {
      axios
        .put(
          "https://sigmabooking.azurewebsites.net/api/TableLayouts/" +
            this.layoutId,
          {
            id: this.layoutId,
            isDefault: this.isDefault,
            date: this.layoutDate,
            tables: this.layout,
          }
        )
        .then((response) => {
          console.log(response.data);
          axios
            .put(
              "https://sigmabooking.azurewebsites.net/api/Tables/",
              this.layout
            )
            .then((response) => {
              console.log(response.data);
            })
            .catch((error) => {
              console.log(error);
            });
        })
        .catch((error) => {
          console.log(error);
        });
    },
    currentDate() {
      let today = new Date();
      const dd = String(today.getDate()).padStart(2, "0");
      const mm = String(today.getMonth() + 1).padStart(2, "0");
      const yyyy = today.getFullYear();

      today = dd + mm + yyyy;
      return today;
    },
  },
  created() {
    this.getLayout();
  },
  isLoggedIn() {},

  enableButtons() {
    const button = document.querySelectorAll("button");
    if (this.isLoggedIn() === 1) {
      button.disabled = false;
    } else return (button.disabled = true);
  },
};

</script>

<style>
/*************************************/

.btnDiv {
  margin-left: 10px;
  display: flex;
  justify-content: space-between;
}

.date {
  text-align: center;
}

.btn-primary {
  background-color: black;
}

.remove {
  position: absolute;
  right: 2px;
  top: 0;
  cursor: pointer;
}

.click {
  position: relative;
  cursor: pointer;
}

.vue-grid-layout {
  background: #eee;
}

.vue-grid-item:not(.vue-grid-placeholder) {
  background: #ccc;
  border: 1px solid black;
}

.vue-grid-item .resizing {
  opacity: 0.9;
}

.vue-grid-item .static {
  background: #cce;
}

.vue-grid-item .text {
  font-size: 24px;
  text-align: center;
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  margin: auto;
  height: 100%;
  width: 100%;
}

.vue-grid-item .no-drag {
  height: 100%;
  width: 100%;
}

.vue-grid-item .minMax {
  font-size: 12px;
}

.vue-grid-item .add {
  cursor: pointer;
}

.vue-draggable-handle {
  position: absolute;
  width: 20px;
  height: 20px;
  top: 0;
  left: 0;
  background: url("data:image/svg+xml;utf8,<svg xmlns='http://www.w3.org/2000/svg' width='10' height='10'><circle cx='5' cy='5' r='5' fill='#999999'/></svg>")
    no-repeat;
  background-position: bottom right;
  padding: 0 8px 8px 0;
  background-repeat: no-repeat;
  background-origin: content-box;
  box-sizing: border-box;
  cursor: pointer;
}

.gadgetRow {
  justify-content: center;
}
.buttonContainer {
  justify-content: space-between;
}
</style>
