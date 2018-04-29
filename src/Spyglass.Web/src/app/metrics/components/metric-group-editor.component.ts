import {Component, Input, OnInit} from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import {MetricGroup} from "../models/metricGroup";

@Component({
  selector: 'sg-metric-group-editor',
  template: `
    <form [formGroup]="groupForm">
      <mat-form-field class="full-width">
        <input matInput placeholder="Name" formControlName="name" [(ngModel)]="group.name">
      </mat-form-field>
      <mat-form-field class="full-width">
        <input matInput placeholder="Description" formControlName="description" [(ngModel)]="group.description">
      </mat-form-field>
    </form>
  `,
  styles: [`
  .full-width {
    width: 100%;
  }
  `]
})
export class MetricGroupEditorComponent implements OnInit {
  @Input() group: MetricGroup;
  groupForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.groupForm = this.fb.group({
      name: ['', Validators.required],
      description: ''
    })
  }

  ngOnInit() {
    if (this.group.name) {
      this.groupForm.setValue({
        name: this.group.name
      });
    }
  }

}
