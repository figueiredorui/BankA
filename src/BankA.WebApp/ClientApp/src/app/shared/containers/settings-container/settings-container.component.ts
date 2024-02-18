import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from '../../components/base/base.component';

@Component({
	selector: 'app-settings-container',
	templateUrl: './settings-container.component.html',
	styleUrls: ['./settings-container.component.scss']
})
export class SettingsContainerComponent extends BaseComponent implements OnInit {

	constructor(private injector: Injector) {
		super(injector);
	}

	ngOnInit() {
		this.activatedRoute.data.subscribe((parameter: any) => {
			console.log('Settings ContainerComponent ' + parameter);
		});
	}

}