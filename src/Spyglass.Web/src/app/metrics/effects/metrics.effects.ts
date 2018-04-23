import { Injectable } from '@angular/core';
import { Actions, Effect, ofType } from '@ngrx/effects';
import {Action} from '@ngrx/store';
import {Observable} from 'rxjs';
import 'rxjs/add/observable/of';
import {
  LoadMetricGroups,
  LoadSuccessful,
  LoadFailure,
  MetricGroupsActionTypes,
  SelectMetricGroup
} from '../actions/metricGroup.actions';
import {map, switchMap, catchError} from 'rxjs/operators';
import {MetricGroupService} from '../../services/metric-group.service';
import {MetricGroup} from '../models/metricGroup';


@Injectable()
export class MetricsEffects {
  @Effect()
  load$: Observable<Action> = this.actions$.pipe(
    ofType<LoadMetricGroups>(MetricGroupsActionTypes.Load),
    switchMap(action => {
      return this.metricGroupService.getGroups().pipe(
        map((groups: MetricGroup[]) => new LoadSuccessful(groups)),
        catchError(error => Observable.of(new LoadFailure(error)))
      );
    })
  );

  constructor(private actions$: Actions,
              private metricGroupService: MetricGroupService) {}
}
