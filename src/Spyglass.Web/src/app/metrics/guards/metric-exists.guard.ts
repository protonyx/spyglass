import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import * as fromMetrics from '../reducers';
import {select, Store} from "@ngrx/store";
import {filter, take, map, switchMap} from 'rxjs/operators';

@Injectable()
export class MetricExistsGuard implements CanActivate {

  constructor(private store: Store<fromMetrics.State>) {}

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

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean> {
    return this.waitForLoading().pipe(
      switchMap(() => this.hasMetricInStore(route.params['id']))
    );
  }
}
