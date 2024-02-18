import { Component, Inject, Injector, OnInit, ViewChild } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Papa } from 'ngx-papaparse';
import { AccountService } from '../../../accounts/services/accounts.service';
import { ErrorMessage } from '../../../core/models/error-message.model';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { TransactionsService } from '../../services/transactions.service';
import { Account } from '../../../accounts/models/accounts.model';
import { FileFormatConfiguration } from '../../../accounts/models/file-format-configuration';

export interface IColumnMapping {
  ColumnId: string;
  ColumnFieldMap: string;
}

@Component({
  selector: 'app-import-file-csv',
  templateUrl: './import-file-csv.component.html',
  styleUrls: ['./import-file-csv.component.scss']
})
export class ImportFileCsvComponent extends BaseComponent implements OnInit {
  @ViewChild('fileUpload') fileUpload: any;
  public account: Account | undefined;

  public columnMapping: IColumnMapping[] = [];
  public csvDataJson: any[] = [];

  public fieldNameLookup: string[] = [];
  public fileFormatConfig = {} as FileFormatConfiguration;
  private input: any;

  constructor(private injector: Injector,
    public dialogRef: MatDialogRef<ImportFileCsvComponent>,
    @Inject(MAT_DIALOG_DATA) private data: any,
    private transactionsService: TransactionsService,
    private accountService: AccountService,
    private papa: Papa) {
    super(injector);

    this.account = data.account;
  }

  get isFileSelected(): boolean {
    return this.fileUpload?.nativeElement?.files.item(0);
  }

  public ngOnInit() {
    this.createFieldNameLookup();
  }

  public onFileSelected(event: any) {
    try {
      this.errorMessage = undefined;
      // eslint-disable-next-line @typescript-eslint/no-this-alias
      const _self = this;
      const files = event.srcElement.files;
      if (files[0].name.includes('.csv')) {

        this.input = event.target;
        const reader = new FileReader();

        reader.onload = function () {

          _self.papa.parse(event.target.files[0], {
            complete: function (results: any) {

              _self.csvDataJson = results.data;
              _self.columnMapping = [];

              const columns = Object.keys(_self.csvDataJson[0]);
              for (const i in columns) {
                const column = columns[i];

                _self.columnMapping[i] = {
                  ColumnId: column,
                  ColumnFieldMap: 'None'
                };
              }

              _self.readImportCsvDefinition();

              _self.fileFormatConfig.Delimiter = results.meta.delimiter;

            }
          });

        };
        reader.readAsText(this.input.files[0]);
      }
      else {
        throw new ErrorMessage('Error parsing File. File format is invalid.', '');
      }
    } catch (error: any) {
      this.errorMessage = error;
    }

  }

  public async import() {
    try {
      this.loading = true;
      this.errorMessage = undefined;

      await this.saveConfig();
      await this.importFile();

      this.dialogRef.close(true);

    } catch (error: any) {
      this.fileUpload.nativeElement.value = null;
      this.errorMessage = error;
    } finally {
      this.loading = false;
    }
  }

  public async saveConfig() {
    if (this.account) {
      this.createFileFormatConfiguration();
      await this.accountService.updateFileFormatConfig(this.account.Id, this.fileFormatConfig);
    }
  }

  public async importFile() {
    if (this.account) {
      const file = this.fileUpload.nativeElement.files.item(0);
      await this.transactionsService.importFile(this.account.Id, file);
    }
  }

  public cancel() {
    this.dialogRef.close(false);
  }

  private createFieldNameLookup(): void {

    this.fieldNameLookup = [];
    this.fieldNameLookup.push('None');
    this.fieldNameLookup.push('Date');
    this.fieldNameLookup.push('Type');
    this.fieldNameLookup.push('Description');
    this.fieldNameLookup.push('CreditAmount');
    this.fieldNameLookup.push('DebitAmount');
    this.fieldNameLookup.push('Amount');

  }

  private readImportCsvDefinition() {
    if (this.account?.FileFormatConfiguration) {

      this.fileFormatConfig = this.account.FileFormatConfiguration;

      if (this.fileFormatConfig.Date_ColumnIndex > -1)
        this.columnMapping[this.fileFormatConfig.Date_ColumnIndex].ColumnFieldMap = 'Date';
      if (this.fileFormatConfig.Type_ColumnIndex > -1)
        this.columnMapping[this.fileFormatConfig.Type_ColumnIndex].ColumnFieldMap = 'Type';
      if (this.fileFormatConfig.Description_ColumnIndex > -1)
        this.columnMapping[this.fileFormatConfig.Description_ColumnIndex].ColumnFieldMap = 'Description';
      if (this.fileFormatConfig.CreditAmount_ColumnIndex > -1)
        this.columnMapping[this.fileFormatConfig.CreditAmount_ColumnIndex].ColumnFieldMap = 'CreditAmount';
      if (this.fileFormatConfig.DebitAmount_ColumnIndex > -1)
        this.columnMapping[this.fileFormatConfig.DebitAmount_ColumnIndex].ColumnFieldMap = 'DebitAmount';
      if (this.fileFormatConfig.Amount_ColumnIndex > -1)
        this.columnMapping[this.fileFormatConfig.Amount_ColumnIndex].ColumnFieldMap = 'Amount';
    }
    else {
      this.fileFormatConfig = new FileFormatConfiguration();
    }
  }

  private createFileFormatConfiguration() {

    this.fileFormatConfig.Date_ColumnIndex = -1;
    this.fileFormatConfig.Type_ColumnIndex = -1;
    this.fileFormatConfig.Description_ColumnIndex = -1;
    this.fileFormatConfig.DebitAmount_ColumnIndex = -1;
    this.fileFormatConfig.CreditAmount_ColumnIndex = -1;
    this.fileFormatConfig.Amount_ColumnIndex = -1;

    for (const i in this.columnMapping) {
      if (this.columnMapping[i].ColumnFieldMap === 'Date') {
        this.fileFormatConfig.Date_ColumnIndex = Number(i);
      }
      if (this.columnMapping[i].ColumnFieldMap === 'Type') {
        this.fileFormatConfig.Type_ColumnIndex = Number(i);
      }
      if (this.columnMapping[i].ColumnFieldMap === 'Description') {
        this.fileFormatConfig.Description_ColumnIndex = Number(i);
      }
      if (this.columnMapping[i].ColumnFieldMap === 'CreditAmount') {
        this.fileFormatConfig.CreditAmount_ColumnIndex = Number(i);
      }
      if (this.columnMapping[i].ColumnFieldMap === 'DebitAmount') {
        this.fileFormatConfig.DebitAmount_ColumnIndex = Number(i);
      }

      if (this.columnMapping[i].ColumnFieldMap === 'Amount') {
        this.fileFormatConfig.Amount_ColumnIndex = Number(i);
      }
    }
  }

}


