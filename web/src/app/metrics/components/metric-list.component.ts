import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Metric } from '../../models/metric.model';

@Component({
  selector: 'app-metric-list',
  templateUrl: './metric-list.component.html'
})
export class MetricListComponent implements OnInit {
  @Input() metrics: Array<Metric> = [];
  @Output() edit = new EventEmitter<Metric>();
  @Output() delete = new EventEmitter<Metric>();

  constructor() {}

  ngOnInit(): void {}

  onEdit(metric: Metric): void {
    this.edit.emit(metric);
  }

  onDelete(metric: Metric): void {
    this.delete.emit(metric);
  }
}
