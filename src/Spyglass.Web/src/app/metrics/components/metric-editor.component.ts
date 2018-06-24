import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import {Metric} from '../models/metric';
import {MetricProvider} from '../models/metricProvider';

@Component({
  selector: 'sg-metric-editor',
  template: `
    <mat-card>
      <mat-card-title>
        Metric
      </mat-card-title>
      
      <mat-card-content>
        <mat-form-field>
          <input matInput
                 [(ngModel)]="metric.name"
                 placeholder="Name">
        </mat-form-field>
        <mat-form-field>
        <textarea matInput
                  [(ngModel)]="metric.description"
                  placeholder="Description"></textarea>
        </mat-form-field>
        <mat-form-field>
          <mat-select [(ngModel)]="currentProvider"
                      placeholder="Metric Type">
            <mat-option *ngFor="let provider of providers"
                        [value]="provider.name">{{ provider.name }}</mat-option>
          </mat-select>
        </mat-form-field>
      </mat-card-content>
      
      <mat-card-actions>
        <button (click)="save.emit(metric)" class="btn btn-outline-primary">Save</button>
      </mat-card-actions>
    </mat-card>
  `
})
export class MetricEditorComponent implements OnInit {
  @Input() providers: MetricProvider[];
  @Input() metric: Metric;
  @Output() save = new EventEmitter<Metric>();

  public currentProvider: MetricProvider;

  constructor() { }

  ngOnInit() {
  }

}
