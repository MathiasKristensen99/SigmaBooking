import {defineStore} from "pinia";
import {TableLayoutService} from "../services/tableLayout.service";
import {TableLayout} from "../models/TableLayout";

const tableLayoutService: TableLayoutService = new TableLayoutService();

export const TableLayoutStore = defineStore({
    id: "tableLayoutStore",
    state: () => ({
        todaysTableLayout: {} as TableLayout
    }),
    getters: {
        tables: (state) => {
            if (state.todaysTableLayout.tables) return state.todaysTableLayout.tables;
            else return null;
        },
        date: (state) => {
            if (state.todaysTableLayout.date != undefined) return state.todaysTableLayout.date;
            else return null;
        }
    },
    actions: {
        getTableLayout(date: string) {
            tableLayoutService
                .getTableLayoutByDate(date)
                .then((layout) => (this.todaysTableLayout = layout))
                .catch((err) => console.log(err));
        },
    },
});