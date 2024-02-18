export class PagedListRequest {
    PageIndex: number;
    PageSize: number;
    Search: string;

    constructor() {
        this.PageIndex = 0;
        this.PageSize = 25;
        this.Search = '';
    }
}
