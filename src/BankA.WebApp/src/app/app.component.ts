import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from './shared/components/base/base.component';

@Component({
	selector: 'app-root',
	templateUrl: './app.component.html',
	styles: []
})
export class AppComponent extends BaseComponent implements OnInit {
	constructor(private injector: Injector) {
		super(injector);
	}
	ngOnInit() {
		console.log('AppComponent');
	}
}
