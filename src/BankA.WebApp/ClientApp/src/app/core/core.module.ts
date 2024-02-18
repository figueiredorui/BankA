import { CommonModule } from '@angular/common';
import { DEFAULT_CURRENCY_CODE, ErrorHandler, NgModule, Optional, SkipSelf } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { ErrorHandlerService } from './services/error-handling.service';
import { PageTitleService } from './services/page-title.service';

@NgModule({
	imports: [CommonModule, HttpClientModule],
	providers: [
		ErrorHandlerService,
		PageTitleService,
		{ provide: ErrorHandler, useClass: ErrorHandlerService },
		{provide: DEFAULT_CURRENCY_CODE, useValue: ''}
	],
	declarations: [],
	exports: [CommonModule, HttpClientModule]
})

export class CoreModule {
	constructor(@Optional() @SkipSelf() parentModule: CoreModule) {
		console.debug('CoreModule');
		if (parentModule) {
			throw new Error('CoreModule has already been loaded. Import Core modules in the AppModule only.');
		}
	}
}
