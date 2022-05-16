<template>
  <p class="date">{{ currentDate() }}</p>
  <div class="row">
    <button class="btn btn-secondary me-2 col" type="button" @click="addItem">
      Tilføj bord
    </button>
    <button
      class="btn btn-secondary me-2 col"
      type="button"
      data-bs-toggle="modal"
      data-bs-target="#myModal"
      @click="updateTableLayout"
    >
      Gem bordopstilling
    </button>
    <button
      class="btn btn-secondary me-2 col"
      type="button"
      @click="createLayout"
    >
      Ny Bordopstilling
    </button>
    <div class="col-2">
      <div class="form-check">
        <input class="form-check-input" type="checkbox" v-model="draggable" />
        <label class="form-check-label">Draggable</label>
      </div>
      <div class="form-check">
        <input class="form-check-input" type="checkbox" v-model="resizable" />
        <label class="form-check-label">Resizable</label>
      </div>
    </div>

    <div class="col">
      <div>
        <Datepicker v-model="date"></Datepicker>
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
      <span class="text"
        >{{ item.i }}
        <br />
        <button class="click">Click</button>
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
      date: null,
    };
  },
  mounted() {
    // this.$gridlayout.load();
    this.index = this.layout.length;
  },
  methods: {
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
        .post("https://localhost:7026/api/Tables/", {
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
        .delete("https://localhost:7026/api/Tables/" + val)
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
    createLayout() {
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
        date: this.currentDateHttpFormat().toString(),
        tables: tables,
      };
      console.log(tableLayout);
      axios
        .post("https://localhost:7026/api/TableLayouts/", {
          id: "",
          isDefault: tableLayout.isDefault,
          date: tableLayout.date,
          tables: tableLayout.tables,
        })
        .then((response) => {
          console.log(response);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    adminLogin() {},

    getLayout() {
      axios
        .get(
          "https://localhost:7026/api/TableLayouts/" +
            this.currentDateHttpFormat().toString()
        )
        .then((response) => {
          console.log(response.data);
          this.layoutDate = response.data.date;
          this.isDefault = response.data.isDefault;
          this.layoutId = response.data.id;
          console.log(this.layoutId);
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
        .put("https://localhost:7026/api/TableLayouts/" + this.layoutId, {
          id: this.layoutId,
          isDefault: this.isDefault,
          date: this.layoutDate,
          tables: this.layout,
        })
        .then((response) => {
          console.log(response.data);
          axios
            .put("https://localhost:7026/api/Tables/", this.layout)
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
      const current = new Date();
      const date = `${current.getDate()}/${
        current.getMonth() + 1
      }/${current.getFullYear()}`;
      return date;
    },
    currentDateHttpFormat() {
      let today = new Date();
      const dd = String(today.getDate()).padStart(2, "0");
      const mm = String(today.getMonth() + 1).padStart(2, "0");
      const yyyy = today.getFullYear();

      today = dd + "%2F" + mm + "%2F" + yyyy;
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
</style>
