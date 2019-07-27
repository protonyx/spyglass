import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { HomeComponent } from './home/home.component';
import { NotFoundComponent } from './layout/not-found.component';
import { MetricDetailsPageComponent } from './metrics/containers/metric-details-page.component';
import { MetricEditorPageComponent } from './metrics/containers/metric-editor-page.component';
import { MetricPageComponent } from './metrics/containers/metric-page.component';
import { MetricExistsGuard } from './metrics/guards/metric-exists.guard';

const appRouteModule: Routes = [
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
    imports: [RouterModule.forRoot(appRouteModule)]
})
export class AppRouteModule {}
