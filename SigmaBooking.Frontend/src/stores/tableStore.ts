import { defineStore } from "pinia";
import {TableService} from "../services/table.service";

const tableService: TableService = new TableService();

export const TableStore = defineStore({
    id: "tableStore",
    state: () => ({

    }),
    getters: {

    },
    actions: {
        
    }
})