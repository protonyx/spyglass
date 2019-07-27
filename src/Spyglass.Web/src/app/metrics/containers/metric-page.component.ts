import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as MetricActions from '../actions/metrics.actions';
import { Metric } from '../models/metric';
import * as fromMetrics from '../reducers';

@Component({
    selector: 'sg-metric-page',
    template: `
        <h1>Metrics</h1>
        <div>
            <button class="btn btn-primary" (click)="handleCreateMetric()">
                <clr-icon shape="plus"></clr-icon>
                Create Metric
            </button>
            <button class="btn btn-secondary" (click)="refreshMetrics()">
                <clr-icon shape="refresh"></clr-icon>
                Refresh
            </button>
        </div>
        <sg-metric-list [metrics]="metrics$ | async"></sg-metric-list>
    `
})
export class MetricPageComponent implements OnInit {
    metrics$: Observable<Metric[]>;
    loading$: Observable<boolean>;

    constructor(private store: Store<fromMetrics.State>, private router: Router, private route: ActivatedRoute) {
        this.metrics$ = store.pipe(select(fromMetrics.getAllMetrics));
        this.loading$ = store.pipe(select(fromMetrics.getMetricsLoading));
    }

    handleCreateMetric(): void {
        this.router.navigate(['new'], {
            relativeTo: this.route
        });
    }

    ngOnInit(): void {
        this.store.dispatch(new MetricActions.LoadProviders());
        this.refreshMetrics();
    }

    refreshMetrics(): void {
        this.store.dispatch(new MetricActions.LoadMetrics());
    }
}
