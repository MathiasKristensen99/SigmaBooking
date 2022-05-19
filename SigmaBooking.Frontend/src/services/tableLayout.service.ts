import http from "./http.client";
import {TableLayout} from "../models/TableLayout";

export class TableLayoutService {
    async getTableLayoutByDate(date: string): Promise<TableLayout> {
        const res = await http.get<TableLayout>("api/TableLayouts/" + date);
        return res.data;
    }
}