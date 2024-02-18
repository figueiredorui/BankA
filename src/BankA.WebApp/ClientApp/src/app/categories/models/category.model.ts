
export interface Category {
	Id: number;
	CategoryName: string;
	CategoryFullName: string;
	ParentId: number;
	ParentCategoryName: string;
	CategoryType: string;
	IsSystem: boolean;
}
