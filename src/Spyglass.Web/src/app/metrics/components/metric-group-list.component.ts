import {Component, EventEmitter, Input, Output} from '@angular/core';
import {MetricGroup} from '../models/metricGroup';

@Component({
  selector: 'sg-group-list',
  template: `
    <mat-nav-list>
      <a mat-list-item *ngFor="let group of groups"
         [routerLink]="['/group', group.id]">
        <h3 matLine>{{ group.name }}</h3>
        <p matLine>{{ group.description }}</p>
        <mat-divider inset></mat-divider>
      </a>
    </mat-nav-list>
  `
})
export class MetricGroupListComponent {
  @Input() groups: MetricGroup[];

  constructor() { }
}
