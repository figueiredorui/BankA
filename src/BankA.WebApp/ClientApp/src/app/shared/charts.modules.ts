import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChartjsModule } from '@ctrl/ngx-chartjs';
import {
	ArcElement,
	BarController,
	BarElement,
	CategoryScale,
	Chart,
	DoughnutController,
	LinearScale,
	LineController,
	LineElement,
	PieController,
	PointElement,
	PolarAreaController,
	RadarController,
	RadialLinearScale,
	Title,
	Tooltip,
	Legend,
} from 'chart.js';

@NgModule({
	imports: [
		CommonModule,
		ChartjsModule
	],
	declarations: [],
	exports: [CommonModule, ChartjsModule]
})
export class ChartsModule {
	constructor() {

		Chart.register(
			ArcElement,
			BarController,
			BarElement,
			CategoryScale,
			DoughnutController,
			LinearScale,
			LineController,
			LineElement,
			PieController,
			PointElement,
			PolarAreaController,
			RadarController,
			RadialLinearScale,
			Title,
			Tooltip,
			Legend,
		);
	}
}
