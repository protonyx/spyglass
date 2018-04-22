import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

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
import {AppRouteModule} from './app.routes';

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
import {MetricGroupService} from './services/metric-group.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MetricGroupListPageComponent,
    MetricGroupDetailsPageComponent,
    MetricGroupListComponent,
    MetricGroupDetailsComponent,
    MetricDetailsComponent
  ],
  imports: [
    AppRouteModule,
    BrowserModule,
    BrowserAnimationsModule,
    RouterModule,
    FormsModule,
    HttpClientModule,
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatMenuModule,
    MatIconModule,
    MatButtonModule,
    StoreModule.forRoot(reducers, { metaReducers }),
    !environment.production ? StoreDevtoolsModule.instrument() : [],
    EffectsModule.forRoot([AppEffects, MetricsEffects])
  ],
  providers: [MetricGroupService],
  bootstrap: [AppComponent]
})
export class AppModule { }
