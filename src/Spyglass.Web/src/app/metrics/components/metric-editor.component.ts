import {Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges} from '@angular/core';
import {FormBuilder, Validators} from '@angular/forms';
import {Metric} from '../models/metric';
import {MetricProvider} from '../models/metricProvider';

@Component({
  selector: 'sg-metric-editor',
  template: `
    <form clrForm 
          class="metric-form"
          [formGroup]="metricForm"
          (submit)="handleFormSubmit($event)">
      <clr-input-container>
        <label for="name">Name</label>
        <input clrInput
               required
               formControlName="name"
               placeholder="Name">
      </clr-input-container>
      
      <clr-input-container>
        <label for="description">Description</label>
        <textarea clrInput
                  formControlName="description"
                  placeholder="Description"></textarea>
      </clr-input-container>
      
      <clr-select-container>
        <label for="metricType">Metric Type</label>
        
        <select clrSelect
                formControlName="providerType">
          <option *ngFor="let provider of providers" [value]="provider.name">{{ provider.name }}</option>
        </select>
      </clr-select-container>

      <div *ngIf="currentProvider">
        <ng-container *ngFor="let prop of currentProvider.properties">
          <clr-input-container>
            <label>{{prod.name}}</label>
            <input clrInput
                   [name]="prop.name"
                   [(ngModel)]="metric.provider[prop.name]"
                   [placeholder]="prop.name">
          </clr-input-container>
        </ng-container>
      </div>

      <button type="submit" 
              class="btn btn-primary"
              [disabled]="!metricForm.valid">Save</button>
    </form>
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
export class MetricEditorComponent implements OnInit, OnChanges {
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

  handleFormSubmit(event: any) {
    this.save.emit(this.metricForm.value);
  }

  ngOnChanges(changes: SimpleChanges): void {
    console.log(changes);
  }

}
