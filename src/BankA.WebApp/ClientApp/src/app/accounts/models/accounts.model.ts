
export class Account {
	Id: number;
	Description: string;
	Closed: boolean;
	FileFormat: string;
	FileFormatConfiguration: any;

	constructor(jsonObj: any) {
		this.Id = 0;
		this.Description = '';
		this.Closed = false;
		this.FileFormat = '';
		this.FileFormatConfiguration = null;

		if (jsonObj)
			this.Create(jsonObj);
	}

	public static CreateList(jsonArray: Array<any>): Array<Account> {
		return jsonArray.map(m => new Account(m));
	}

	private Create(jsonObj: any) {
		this.Id = jsonObj.Id;
		this.Description = jsonObj.Description;
		this.Closed = jsonObj.Closed;
		this.FileFormat = jsonObj.FileFormat;
		this.FileFormatConfiguration = jsonObj.FileFormatConfiguration;
	}

	public HasValidFileFormatConfiguration() {
		let hasValidFileFormatConfiguration = true;
		if (this.IsCsvFileFormat()) {
			if (!this.FileFormatConfiguration)
				hasValidFileFormatConfiguration = false;
		}
		return hasValidFileFormatConfiguration;
	}

	public IsCsvFileFormat() {
		let isCsvFileFormat = false;
		if (this.FileFormat == 'CSV') {
			isCsvFileFormat = true;
		}
		return isCsvFileFormat;
	}
}


