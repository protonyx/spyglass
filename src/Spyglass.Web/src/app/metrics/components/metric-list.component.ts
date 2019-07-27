import { Component, Input } from '@angular/core';

import { Metric } from '../models/metric';

@Component({
    selector: 'sg-metric-list',
    template: `
        <clr-datagrid>
            <clr-dg-column [clrDgField]="'name'">Name</clr-dg-column>
            <clr-dg-column [clrDgField]="'description'">Description</clr-dg-column>
            <clr-dg-column [clrDgField]="'providerType'">Type</clr-dg-column>

            <clr-dg-row *ngFor="let metric of metrics">
                <clr-dg-cell
                    ><a [routerLink]="['/metrics', metric.id]">{{ metric.name }}</a></clr-dg-cell
                >
                <clr-dg-cell>{{ metric.description }}</clr-dg-cell>
                <clr-dg-cell>{{ metric.providerType }}</clr-dg-cell>
            </clr-dg-row>
        </clr-datagrid>
    `,
    styles: []
})
export class MetricListComponent {
    @Input() metrics: Metric[];
}
