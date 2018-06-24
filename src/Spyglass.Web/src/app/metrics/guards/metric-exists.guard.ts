import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable, of } from 'rxjs';
import {filter, take, map, switchMap, tap, catchError} from 'rxjs/operators';
import { LoadMetricsSuccessful } from '../actions/metrics.actions';
import * as fromMetrics from '../reducers';
import {select, Store} from "@ngrx/store";
import {MetricService} from '../services/metric.service';

@Injectable()
export class MetricExistsGuard implements CanActivate {

  constructor(
    private store: Store<fromMetrics.State>,
    private metricService: MetricService) {}

  waitForLoading(): Observable<boolean> {
    return this.store.pipe(
      select(fromMetrics.getMetricsLoading),
      filter(loading => !loading),
      take(1)
    );
  }

  hasMetricInStore(id: string): Observable<boolean> {
    return this.store.pipe(
      select(fromMetrics.getMetricEntities),
      map(entities => !!entities[id]),
      take(1)
    )
  }

  hasMetricInApi(id: string): Observable<boolean> {
    return this.metricService.getMetric(id).pipe(
      map(entity => new LoadMetricsSuccessful([entity])),
      tap((action: LoadMetricsSuccessful) => this.store.dispatch(action)),
      map(entity => !!entity),
      catchError(() => {
        return of(false);
      })
    )
  }

  hasMetric(id: string): Observable<boolean> {
    return this.hasMetricInStore(id).pipe(
      switchMap((inStore) => {
        if (inStore) {
          return of(inStore);
        }

        return this.hasMetricInApi(id);
      })
    )
  }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.waitForLoading().pipe(
      switchMap(() => this.hasMetric(route.params['id']))
    );
  }
}
