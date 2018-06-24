import {RouterModule, Routes} from '@angular/router';
import {NgModule} from '@angular/core';
import {HomeComponent} from './home/home.component';
import {NotFoundComponent} from './core/components/not-found.component';
import {MetricGroupDetailsPageComponent} from "./metrics/containers/metric-group-details-page.component";
import {GroupExistsGuard} from "./metrics/guards/group-exists.guard";
import {MetricPageComponent} from "./metrics/containers/metric-page.component";
import {MetricExistsGuard} from './metrics/guards/metric-exists.guard';
import {MetricDetailsPageComponent} from './metrics/containers/metric-details-page.component';
import {MetricEditorPageComponent} from './metrics/containers/metric-editor-page.component';

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
        path: 'new',
        component: MetricEditorPageComponent
      },
      {
        path: ':id/edit',
        component: MetricEditorPageComponent,
        canActivate: [MetricExistsGuard]
      },
      {
        path: ':id',
        component: MetricDetailsPageComponent,
        canActivate: [MetricExistsGuard]
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
