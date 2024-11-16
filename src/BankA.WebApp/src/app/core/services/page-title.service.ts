import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';


@Injectable()
export class PageTitleService {

	title: string = '';
	private currentTitleSource = new BehaviorSubject<string>('');
	titleChanged: Observable<string> = this.currentTitleSource.asObservable();

	constructor() { }

	setTitle(title: string) {
		this.currentTitleSource.next(title);
	}

}
