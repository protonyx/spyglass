import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Metric } from '../../models/metric.model';

@Component({
  selector: 'app-metric-list',
  templateUrl: './metric-list.component.html'
})
export class MetricListComponent implements OnInit {
  @Input() metrics: Array<Metric> | null = [];
  @Output() add = new EventEmitter<Metric>();

  constructor() {}

  ngOnInit(): void {}
}
