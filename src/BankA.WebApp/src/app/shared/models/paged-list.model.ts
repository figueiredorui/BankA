export class PagedList<T> {
    PageIndex: number;
    PageCount: number;
    PageSize: number;
    RecordCount: number;
    Data: Array<T>;

    constructor(jsonObj: any, data: Array<T>) {
        this.PageIndex = jsonObj.PageIndex;
        this.PageCount = jsonObj.PageCount;
        this.PageSize = jsonObj.PageSize;
        this.RecordCount = jsonObj.RecordCount;
        this.Data = data;
    }
}


