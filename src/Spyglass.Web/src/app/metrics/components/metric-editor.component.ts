import {Component, OnInit, Input, Output, EventEmitter} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
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
        <form class="metric-form" [formGroup]="metricForm">
          <mat-form-field class="full-width">
            <input matInput
                   formControlName="name"
                   placeholder="Name">
          </mat-form-field>
          <mat-form-field class="full-width">
          <textarea matInput
                    formControlName="description"
                    placeholder="Description"></textarea>
          </mat-form-field>
          <mat-form-field class="full-width">
            <mat-select formControlName="description"
                        placeholder="Metric Type">
              <mat-option *ngFor="let provider of providers"
                          [value]="provider.name">{{ provider.name }}</mat-option>
            </mat-select>
          </mat-form-field>
          <!-- Dynamic Provider Properties -->
          <div *ngIf="currentProvider">
            {{ currentProvider | json }}
            <ng-container *ngFor="let prop of currentProvider.properties">
              <mat-form-field class="full-width">
                <input matInput
                       [name]="prop.name"
                       [(ngModel)]="metric.provider[prop.name]"
                       [placeholder]="prop.name">
              </mat-form-field>
            </ng-container>
          </div>
        </form>
      </mat-card-content>
      
      <mat-card-actions>
        <button (click)="save.emit(metric)" class="btn btn-outline-primary">Save</button>
      </mat-card-actions>
    </mat-card>
  `,
  styles: [
    `
    .metric-form {
      min-width: 150px;
      max-width: 500px;
      width: 100%;
    }
      
    .full-width {
      width: 100%;
    }
    `
  ]
})
export class MetricEditorComponent implements OnInit {
  @Input() providers: MetricProvider[];
  @Input() metric: Metric;
  @Output() save = new EventEmitter<Metric>();

  public currentProvider: MetricProvider;

  metricForm = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    providerType: ['', Validators.required]
  });

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
  }



}
