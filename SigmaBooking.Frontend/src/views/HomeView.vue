<template>
  <br />
  <div>
    <div class="layoutJSON">
      Displayed as <code>[x, y, w, h]</code>:
      <div class="columns">
        <div class="layoutItem" v-for="item in layout" :key="item.i">
          <b>{{ item.i }}</b
          >: [{{ item.x }}, {{ item.y }}, {{ item.w }}, {{ item.h }}]
        </div>
      </div>
    </div>
    <button @click="addItem">Tilføj bord</button>
    <button @click="updateLayout">Gem bordopstilling</button>
    <input type="checkbox" v-model="draggable" /> Draggable
    <input type="checkbox" v-model="resizable" /> Resizable
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
  </div>
</template>

<script>
import { GridLayout, GridItem } from "vue-grid-layout";
import axios from "axios";

export default {
  components: {
    GridLayout,
    GridItem,
  },
  data() {
    return {
      layout: [],
      draggable: true,
      resizable: true,
      colNum: 40,
      index: 0,
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
        })
        .catch((error) => {
          console.log(error);
        });
    },
    getLayout() {
      axios
        .get("https://localhost:7026/api/Tables")
        .then((response) => {
          for (const responseElement of response.data) {
            this.layout.push(responseElement);
          }
          //this.layout.push(response.data);
          console.log(this.layout);
        })
        .catch((error) => {
          console.log(error);
        });
    },
    updateLayout() {
      console.log(this.layout);
      axios
        .put("https://localhost:7026/api/Tables/", this.layout)
        .then((response) => {
          console.log(response.data);
        })
        .catch((error) => {
          console.log(error);
        });
    },
  },
  created() {
    this.getLayout();
  },
};
</script>

<style>
.layoutJSON {
  background: #ddd;
  border: 1px solid black;
  margin-top: 10px;
  padding: 10px;
}

.columns {
  -moz-columns: 120px;
  -webkit-columns: 120px;
  columns: 120px;
}

/*************************************/

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
