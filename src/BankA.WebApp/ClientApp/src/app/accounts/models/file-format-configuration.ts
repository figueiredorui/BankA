

export class FileFormatConfiguration {

	HasHeaders: boolean;
	Delimiter: string;
	Date_ColumnIndex: number;
	Type_ColumnIndex: number;
	Description_ColumnIndex: number;
	DebitAmount_ColumnIndex: number;
	CreditAmount_ColumnIndex: number;
	Amount_ColumnIndex: number;

	constructor() {
		this.HasHeaders = false;
		this.Delimiter = '';
		this.Date_ColumnIndex = 0;
		this.Type_ColumnIndex = -1;
		this.Description_ColumnIndex = -1;
		this.DebitAmount_ColumnIndex = -1;
		this.CreditAmount_ColumnIndex = -1;
		this.Amount_ColumnIndex = -1;
	}


}
