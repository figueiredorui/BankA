import { ErrorHandler, Injectable } from '@angular/core';

@Injectable()
export class ErrorHandlerService extends ErrorHandler {

    constructor() {
        super();
    }

    override handleError(error: Error) {
        console.error(error);
        super.handleError(error);
    }
}