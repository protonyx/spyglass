import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Monitor } from '../../models/monitor.model';
import { Connection } from '../../models/connection.model';

@Component({
  selector: 'app-monitor-editor',
  templateUrl: './monitor-editor.component.html'
})
export class MonitorEditorComponent implements OnChanges {
  @Input() monitor: Monitor = {} as Monitor;
  @Input() connections: Array<Connection> = [];

  monitorForm = this.fb.group({
    name: ['', Validators.required],
    category: [''],
    description: [''],
    connectionId: ['', Validators.required],
    query: ['', Validators.required],
    units: ['']
  });

  get valid(): boolean {
    return this.monitorForm.valid;
  }

  get value(): Monitor {
    return this.monitorForm.value as Monitor;
  }

  constructor(private fb: FormBuilder) {}

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['monitor'] && this.monitor) {
      this.load(this.monitor);
    }
  }

  load(data: Monitor): void {
    this.monitorForm.patchValue({
      name: data.name,
      category: data.category,
      description: data.description,
      connectionId: data.connectionId,
      query: data.query,
      units: data.units
    });
  }

  handleFormSubmit(event: any): void {}
}
