import { Component, Input } from '@angular/core';

@Component({ standalone: false,
	selector: 'app-loader',
	templateUrl: './loader.component.html',
	styleUrls: ['./loader.component.scss'],
	
})
export class LoaderComponent {
	@Input()
	show: boolean = false;

	constructor() {}
}
