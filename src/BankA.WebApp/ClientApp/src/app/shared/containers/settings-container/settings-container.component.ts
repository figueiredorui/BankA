import { Component, Injector, OnInit } from '@angular/core';
import { BaseComponent } from '../../components/base/base.component';
import { environment } from '../../../../environments/environment';

@Component({
	selector: 'app-settings-container',
	templateUrl: './settings-container.component.html',
	styleUrls: ['./settings-container.component.scss']
})
export class SettingsContainerComponent extends BaseComponent implements OnInit {

	version = environment.version;
	constructor(private injector: Injector) {
		super(injector);
	}

	ngOnInit() {
		this.activatedRoute.data.subscribe((parameter: any) => {
			console.log('Settings ContainerComponent ' + parameter);
		});
	}

}