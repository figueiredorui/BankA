export interface TransactionList {
	Id: number;
	AccountId: number;
	TransactionDate: string;
	Description: string;
	Balance: number;
	RunningBalance: number;
	CategoryId: number;
	CategoryParentId: number;
	CategoryName: string;
	MerchantName: string;
}
