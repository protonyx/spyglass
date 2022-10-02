import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Monitor } from '../../models/monitor.model';

@Component({
  selector: 'app-monitor-list',
  templateUrl: './monitor-list.component.html'
})
export class MonitorListComponent implements OnInit {
  @Input() monitors: Array<Monitor> = [];
  @Output() edit = new EventEmitter<Monitor>();
  @Output() delete = new EventEmitter<Monitor>();

  constructor() {}

  ngOnInit(): void {}

  onEdit(monitor: Monitor): void {
    this.edit.emit(monitor);
  }

  onDelete(monitor: Monitor): void {
    this.delete.emit(monitor);
  }
}
