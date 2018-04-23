import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {HomeComponent} from './home/home.component';
import {MetricGroupListPageComponent} from './metrics/containers/metric-group-list-page.component';
import {NotFoundComponent} from './core/components/not-found.component';
import {MetricGroupDetailsPageComponent} from "./metrics/containers/metric-group-details-page.component";
import {GroupExistsGuard} from "./metrics/guards/group-exists.guard";

const appRoutes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'groups',
    children: [
      {
        path: ':id',
        component: MetricGroupDetailsPageComponent,
        canActivate: [GroupExistsGuard]
      },
      { path: '', component: MetricGroupListPageComponent }
    ]
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [
    RouterModule.forRoot(appRoutes)
  ]
})
export class AppRouteModule { }
