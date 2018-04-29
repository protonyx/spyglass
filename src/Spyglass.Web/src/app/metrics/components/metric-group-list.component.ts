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
      <a mat-list-item (click)="createGroup.emit()">
        <mat-icon matLine>add</mat-icon>
        <h3 matLine>Create group</h3>
      </a>
    </mat-nav-list>
  `
})
export class MetricGroupListComponent {
  @Input() groups: MetricGroup[];
  @Output() createGroup: EventEmitter<any>;

  constructor() {
    this.createGroup = new EventEmitter<any>();
  }
}
