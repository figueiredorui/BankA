
export interface Transaction {

	Id: number;
	AccountId: number;
	AccountDescription: string;
	TransactionDate: Date;
	TransactionType: string;
	Description: string;
	DebitAmount: number;
	CreditAmount: number;
	Category: TransactionCategory;
	Balance: number;
	RunningBalance: number;
	Merchant: TransactionMerchant;
}

export interface TransactionMerchant {

	Id: number;
	MerchantName: string;
}

export interface TransactionCategory {

	Id: number;
	CategoryFullName: string;
	CategoryTyoe: string;

}

