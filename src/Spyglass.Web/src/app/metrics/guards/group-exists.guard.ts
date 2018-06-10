import { Injectable } from '@angular/core';
import {ActivatedRouteSnapshot, CanActivate, RouterStateSnapshot} from "@angular/router";
import {Observable} from "rxjs";
import {filter, take, map, switchMap} from 'rxjs/operators';
import * as fromMetrics from '../reducers';
import {select, Store} from "@ngrx/store";

@Injectable()
export class GroupExistsGuard implements CanActivate {

  constructor(private store: Store<fromMetrics.State>) { }

  waitForLoading(): Observable<boolean> {
    return this.store.pipe(
      select(fromMetrics.getGroupsLoading),
      filter(loaded => loaded),
      take(1)
    );
  }

  hasGroupInStore(id: string): Observable<boolean> {
    return this.store.pipe(
      select(fromMetrics.getGroupEntities),
      map(entities => !!entities[id]),
      take(1)
    )
  }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> {
    return this.waitForLoading().pipe(
      switchMap(() => this.hasGroupInStore(route.params['id']))
    );
  }

}
