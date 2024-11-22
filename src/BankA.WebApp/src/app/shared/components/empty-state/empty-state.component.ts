import { Component, Injector } from '@angular/core';
import { BaseComponent } from '../base/base.component';

@Component({ standalone: false,
	selector: 'app-empty-state',
	templateUrl: './empty-state.component.html',
	styleUrls: ['./empty-state.component.scss']
})
export class EmptyStateComponent extends BaseComponent {
	constructor(private injector: Injector) {
		super(injector);
	}
}
