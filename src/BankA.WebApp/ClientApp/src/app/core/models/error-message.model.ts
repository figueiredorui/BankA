export class ErrorMessage {
    Message: string;
    Details: string;

    constructor(message: string, details: string) {
        this.Message = message;
        this.Details = details;
    }
}