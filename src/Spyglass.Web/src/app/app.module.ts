import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

import { ClarityModule } from '@clr/angular';
import {
  MatAutocompleteModule,
  // MatBadgeModule,
  // MatBottomSheetModule,
  MatButtonModule,
  MatButtonToggleModule,
  MatCardModule,
  MatCheckboxModule,
  MatChipsModule,
  MatDatepickerModule,
  MatDialogModule,
  MatDividerModule,
  MatExpansionModule,
  MatFormFieldModule,
  MatGridListModule,
  MatIconModule,
  MatInputModule,
  MatListModule,
  MatMenuModule,
  MatNativeDateModule,
  MatPaginatorModule,
  MatProgressBarModule,
  MatProgressSpinnerModule,
  MatRadioModule,
  MatRippleModule,
  MatSelectModule,
  MatSidenavModule,
  MatSliderModule,
  MatSlideToggleModule,
  MatSnackBarModule,
  MatSortModule,
  MatStepperModule,
  MatTableModule,
  MatTabsModule,
  MatToolbarModule,
  MatTooltipModule,
  // MatTreeModule
} from '@angular/material';
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
import { MetricGroupListPageComponent } from './metrics/containers/metric-group-list-page.component';
import { MetricGroupDetailsPageComponent } from './metrics/containers/metric-group-details-page.component';

// Components
import { MetricGroupListComponent } from './metrics/components/metric-group-list.component';
import { MetricGroupDetailsComponent } from './metrics/components/metric-group-details.component';
import { MetricDetailsComponent } from './metrics/components/metric-details.component';

// Services
import {MetricService} from './metrics/services/metric.service';
import { NotFoundComponent } from './layout/not-found.component';
import { MetricPageComponent } from './metrics/containers/metric-page.component';
import { LayoutComponent } from './layout/layout.component';
import { MetricGroupEditorComponent } from './metrics/components/metric-group-editor.component';
import { MetricGroupEditorDialogComponent } from './metrics/containers/metric-group-editor-dialog.component';
import {RouterStateSerializer, StoreRouterConnectingModule} from '@ngrx/router-store';
import {CustomRouterStateSerializer} from './shared/utils';
import { MetricListComponent } from './metrics/components/metric-list.component';
import { MetricDetailsPageComponent } from './metrics/containers/metric-details-page.component';
import {GroupExistsGuard} from './metrics/guards/group-exists.guard';
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
    MatCardModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
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
    MetricGroupListPageComponent,
    MetricGroupDetailsPageComponent,
    MetricGroupListComponent,
    MetricGroupDetailsComponent,
    MetricDetailsComponent,
    NotFoundComponent,
    MetricPageComponent,
    LayoutComponent,
    MetricGroupEditorComponent,
    MetricGroupEditorDialogComponent,
    MetricListComponent,
    MetricDetailsPageComponent,
    MetricEditorPageComponent,
    MetricEditorComponent
  ],
  providers: [
    MetricService,
    GroupExistsGuard,
    MetricExistsGuard,
    { provide: RouterStateSerializer, useClass: CustomRouterStateSerializer }
  ],
  entryComponents: [
    MetricGroupEditorDialogComponent
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
