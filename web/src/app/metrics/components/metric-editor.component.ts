import { Component, Input, Output, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Metric } from '../../models/metric.model';

@Component({
  selector: 'app-metric-editor',
  templateUrl: './metric-editor.component.html'
})
export class MetricEditorComponent implements OnInit {
  @Input() metric: Metric | null = null;
  @Input() connections: Array<any> = [];

  metricForm = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    query: [''],
    units: ['']
  });

  get valid(): boolean {
    return this.metricForm.valid;
  }

  get value(): Metric {
    return this.metricForm.value as Metric;
  }

  constructor(private fb: FormBuilder) {}

  ngOnInit(): void {}
}
