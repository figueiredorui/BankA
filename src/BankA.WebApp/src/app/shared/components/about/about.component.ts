import { Component } from '@angular/core';
import { environment } from '../../../../environments/environment';

@Component({ standalone: false,
  selector: 'app-about',
	templateUrl: './about.component.html',
	styleUrls: ['./about.component.scss'],
})
export class AboutComponent {
	version = environment.version;
}
