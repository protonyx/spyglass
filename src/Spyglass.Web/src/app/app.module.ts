import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import {ClarityModule} from '@clr/angular';
import { HomeComponent } from './home/home.component'
import {AppRouteModule} from './app-route.module';

import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { environment } from '../environments/environment';
import { reducers, metaReducers } from './reducers';
import { EffectsModule } from '@ngrx/effects';
import { AppEffects } from './app.effects';
import { MetricsEffects } from './metrics/effects/metrics.effects';

// Containers

// Components
import { MetricDetailsComponent } from './metrics/components/metric-details.component';

// Services
import {MetricService} from './metrics/services/metric.service';
import { NotFoundComponent } from './layout/not-found.component';
import { MetricPageComponent } from './metrics/containers/metric-page.component';
import { LayoutComponent, HeaderComponent, SideNavComponent } from './layout/layout.component';
import {RouterStateSerializer, StoreRouterConnectingModule} from '@ngrx/router-store';
import {CustomRouterStateSerializer} from './shared/utils';
import { MetricListComponent } from './metrics/components/metric-list.component';
import { MetricDetailsPageComponent } from './metrics/containers/metric-details-page.component';
import {MetricExistsGuard} from './metrics/guards/metric-exists.guard';
import { MetricEditorPageComponent } from './metrics/containers/metric-editor-page.component';
import { MetricEditorComponent } from './metrics/components/metric-editor.component';

@NgModule({
  imports: [
    AppRouteModule,
    BrowserModule,
    BrowserAnimationsModule,
    ClarityModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    StoreModule.forRoot(reducers, { metaReducers }),
    StoreRouterConnectingModule.forRoot({
      /*
        They stateKey defines the name of the state used by the router-store reducer.
        This matches the key defined in the map of reducers
      */
      stateKey: 'router',
    }),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
    EffectsModule.forRoot([AppEffects, MetricsEffects])
  ],
  declarations: [
    AppComponent,
    HomeComponent,
    MetricDetailsComponent,
    NotFoundComponent,
    MetricPageComponent,
    LayoutComponent,
    HeaderComponent,
    SideNavComponent,
    MetricListComponent,
    MetricDetailsPageComponent,
    MetricEditorPageComponent,
    MetricEditorComponent
  ],
  providers: [
    MetricService,
    MetricExistsGuard,
    { provide: RouterStateSerializer, useClass: CustomRouterStateSerializer }
  ],
  entryComponents: [ ],
  bootstrap: [AppComponent]
})
export class AppModule { }
