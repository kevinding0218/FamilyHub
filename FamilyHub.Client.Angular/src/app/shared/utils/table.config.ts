export interface TableConfig<T> {
    data: Array<T>;
    rowsOnPage: number;
    filterQuery: string;
    sortBy: string;
    sortOrder: string;
}
