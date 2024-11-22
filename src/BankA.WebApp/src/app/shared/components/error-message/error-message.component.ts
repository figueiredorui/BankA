import { Component, Input, OnChanges } from '@angular/core';

//ToDo
//https://github.com/Angular-Material-Dev/course-md-ng-my-app/tree/main/src/app/ui/alert

@Component({ standalone: false,
	selector: 'app-error-message',
	templateUrl: './error-message.component.html',
	styleUrls: ['./error-message.component.scss']
})
export class ErrorMessageComponent implements OnChanges {

	@Input() errorMessage: string | any;

	errorMessageClean: string | undefined;

	constructor(
	) { }

	ngOnChanges(): void {

		if (this.errorMessage) {
			if (this.errorMessage.Message) {
				this.errorMessageClean = this.errorMessage.Message;
			}
			else if (this.errorMessage.detail) {
				this.errorMessageClean = this.errorMessage.detail;
			}
			else if (this.errorMessage.Detail) {
				this.errorMessageClean = this.errorMessage.Detail;
			}
			else if (this.errorMessage.message) {
				this.errorMessageClean = this.errorMessage.message;
			}
			else {
				this.errorMessageClean = this.errorMessage;
			}
		}
	}

	close() {
		this.errorMessage = null;
	}
}
