import { Component, OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Actions, ofType } from '@ngrx/effects';
import { select, Store } from '@ngrx/store';
import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import * as MetricActions from '../actions/metrics.actions';
import { Metric } from '../models/metric';
import { MetricProvider } from '../models/metricProvider';
import * as fromMetrics from '../reducers';

@Component({
    selector: 'sg-metric-editor-page',
    template: `
        <a [routerLink]="['/metrics']">
            <clr-icon shape="caret" dir="left"></clr-icon>
            Return to Metrics
        </a>
        <h1>Metric</h1>
        <div *ngIf="providersLoading$ | async">
            <span class="spinner">Loading...</span>
        </div>
        <sg-metric-editor
            [metric]="metric$ | async"
            [providers]="providers$ | async"
            (save)="handleSave($event)"
            (cancel)="handleCancel()"
        >
        </sg-metric-editor>
    `
})
export class MetricEditorPageComponent implements OnDestroy {
    paramsSubscription: Subscription;
    actionsSubscription: Subscription;

    providersLoading$: Observable<boolean>;
    providers$: Observable<MetricProvider[]>;
    metric$: Observable<Metric>;

    constructor(
        private store: Store<fromMetrics.State>,
        private router: Router,
        private route: ActivatedRoute,
        private actions$: Actions
    ) {
        this.paramsSubscription = route.params
            .pipe(
                map(params => {
                    if (params.id) {
                        return new MetricActions.SelectMetric(params.id);
                    } else {
                        return new MetricActions.SelectNewMetric();
                    }
                })
            )
            .subscribe(store);
        this.actionsSubscription = actions$
            .pipe(ofType<MetricActions.SaveMetricSuccessful>(MetricActions.MetricActionTypes.SaveMetricSuccessful))
            .subscribe(a => {
                this.router.navigate(['..'], {
                    relativeTo: this.route
                });
            });
        this.providersLoading$ = store.pipe(select(fromMetrics.getProvidersLoading));
        this.providers$ = store.pipe(select(fromMetrics.getProviders));
        this.metric$ = store.pipe(select(fromMetrics.getSelectedMetric));
    }

    ngOnDestroy(): void {
        this.paramsSubscription.unsubscribe();
        this.actionsSubscription.unsubscribe();
    }

    handleSave(metric: Metric): void {
        this.store.dispatch(new MetricActions.SaveMetric(metric));
    }

    handleCancel(): void {
        this.router.navigate(['..'], {
            relativeTo: this.route
        });
    }
}
