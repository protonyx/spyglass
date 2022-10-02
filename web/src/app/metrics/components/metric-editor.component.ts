import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Metric } from '../../models/metric.model';
import { Connection } from '../../models/connection.model';

@Component({
  selector: 'app-metric-editor',
  templateUrl: './metric-editor.component.html'
})
export class MetricEditorComponent implements OnChanges {
  @Input() metric: Metric = {} as Metric;
  @Input() connections: Array<Connection> = [];

  metricForm = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    connectionId: ['', Validators.required],
    query: ['', Validators.required],
    units: ['']
  });

  get valid(): boolean {
    return this.metricForm.valid;
  }

  get value(): Metric {
    return this.metricForm.value as Metric;
  }

  constructor(private fb: FormBuilder) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['metric'] && this.metric) {
      this.load(this.metric);
    }
  }

  load(data: Metric): void {
    this.metricForm.patchValue({
      name: data.name,
      description: data.description,
      connectionId: data.connectionId,
      query: data.query,
      units: data.units
    });
  }

  handleFormSubmit(event: any): void {}
}
