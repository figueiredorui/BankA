export interface TransactionUpdateRequest {
	AccountId: number;
	TransactionId: number;
	TransactionDate: Date;
	Description: string;
	Balance: number;
	CategoryId: number | null;
	MerchantId: number | null;
	MerchantName: string | null;
}
