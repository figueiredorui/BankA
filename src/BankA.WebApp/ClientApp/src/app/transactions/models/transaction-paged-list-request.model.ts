import { PagedListRequest } from '../../shared/models/paged-list-request.model';


export class TransactionPagedListRequest extends PagedListRequest {
	AccountId: number | undefined = undefined;
	Category: string | undefined = undefined;
	CategoryId: number | undefined = undefined;
	MerchantId: number | undefined = undefined;
	MerchantName: string | undefined = undefined;
	CategoryType: string | undefined = undefined;
	Period: number | undefined = undefined;
	Uncategorized: boolean | undefined = undefined;
}
