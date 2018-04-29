import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {HomeComponent} from './home/home.component';
import {NotFoundComponent} from './core/components/not-found.component';
import {MetricGroupDetailsPageComponent} from "./metrics/containers/metric-group-details-page.component";
import {GroupExistsGuard} from "./metrics/guards/group-exists.guard";
import {MetricPageComponent} from "./metrics/containers/metric-page.component";

const appRoutes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: 'metrics',
    children: [
      { path: '', component: MetricPageComponent },
      {
        path: ':id',
        component: MetricGroupDetailsPageComponent,
        canActivate: [GroupExistsGuard]
      }
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
