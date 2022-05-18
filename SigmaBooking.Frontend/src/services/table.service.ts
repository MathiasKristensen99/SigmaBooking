import http from "./http.client";
import {Table} from "../models/Table";

export class TableService {
    async createTable(
        isStatic: boolean,
        x: number,
        y: number,
        w: number,
        h: number,
        i: string,
    ): Promise<Table[]> {
        const res = await http.post<Table[]>("/Tables", {
            static: isStatic,
            x: x,
            y: y,
            w: w,
            h: h,
            i: i
        });
        return res.data;
    }

    async getAllTables(): Promise<Table[]> {
        const res = await http.get("/Tables");
        return res.data.tables
    }

    getAll() {
        return http.get("/Tables")
    }
}
