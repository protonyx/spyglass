import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule } from '@angular/router';
import { ClarityModule } from '@clr/angular';
import { EffectsModule } from '@ngrx/effects';
import { RouterStateSerializer, StoreRouterConnectingModule, DefaultRouterStateSerializer } from '@ngrx/router-store';
import { StoreModule } from '@ngrx/store';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';

import { environment } from '../environments/environment';

import { AppRouteModule } from './app-route.module';
import { AppComponent } from './app.component';
import { AppEffects } from './app.effects';
import { HomeComponent } from './home/home.component';
import { HeaderComponent, LayoutComponent, SideNavComponent } from './layout/layout.component';
import { NotFoundComponent } from './layout/not-found.component';
import { MetricDetailsComponent } from './metrics/components/metric-details.component';
import { MetricEditorComponent } from './metrics/components/metric-editor.component';
import { MetricListComponent } from './metrics/components/metric-list.component';
import { MetricDetailsPageComponent } from './metrics/containers/metric-details-page.component';
import { MetricEditorPageComponent } from './metrics/containers/metric-editor-page.component';
import { MetricPageComponent } from './metrics/containers/metric-page.component';
import { MetricsEffects } from './metrics/effects/metrics.effects';
import { MetricExistsGuard } from './metrics/guards/metric-exists.guard';
import { MetricService } from './metrics/services/metric.service';
import { metaReducers, reducers } from './reducers';
import { CustomRouterStateSerializer } from './shared/utils';

// Containers

// Components

// Services

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
            serializer: DefaultRouterStateSerializer,
            /*
        They stateKey defines the name of the state used by the router-store reducer.
        This matches the key defined in the map of reducers
      */
            stateKey: 'router'
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
    entryComponents: [],
    bootstrap: [AppComponent]
})
export class AppModule {}
