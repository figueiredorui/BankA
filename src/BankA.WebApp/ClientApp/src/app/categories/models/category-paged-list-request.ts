import { PagedListRequest } from '../../shared/models/paged-list-request.model';


export class CategoryPagedListRequest extends PagedListRequest {
	CategoryType: string | undefined = undefined;
}
