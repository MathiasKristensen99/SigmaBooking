import {Table} from "./Table";

export interface TableLayout {
    id: string;
    isDefault: boolean;
    date: string;
    tables: Table[];
}