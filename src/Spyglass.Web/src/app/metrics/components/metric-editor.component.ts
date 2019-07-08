import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } frr, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

import { Metric } from '../models/metric';
import { MetricProvider } from '../models/metricProvider';
import { MetricService } from '../services/metric.service';

@Component({
  selector: 'sg-metric-editor',
  templateUrl: './metric-editor.component.html',
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
export class MetricEditorComponent implements OnInit, OnChanges, OnDestroy {
  @Input() providers: MetricProvider[];
  @Input() metric: Metric;
  @Output() save = new EventEmitter<Metric>();
  @Output() cancel = new EventEmitter();

  currentProvider: MetricProvider;

  metricForm = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    providerType: ['', Validators.required]
  });

  providerForm = this.fb.group({});

  providerTypeSubscription: Subscription;

  constructor(
    private fb: FormBuilder,
    private metricService: MetricService
  ) { }

  ngOnInit() {
    this.providerTypeSubscription = this.metricForm.get('providerType')
      .valueChanges.subscribe(val => {
        this.currentProvider = this.providers.find(t => t.name === val);
        this.providerForm = this.buildProviderForm(val);
    });
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['metric']) {
      this.metricForm.setValue({
        name: this.metric.name,
        description: this.metric.description,
        providerType: this.metric.providerType
      });
      this.currentProvider = this.providers.find(t => t.name === this.metric.providerType);
      this.providerForm = this.buildProviderForm(this.metric.providerType);
      this.providerForm.setValue(this.metric.provider);
    }
  }

  ngOnDestroy(): void {
    this.providerTypeSubscription.unsubscribe();
  }

  buildProviderForm(providerType: string) {
    const providerConfig = this.providers.find(t => t.name === providerType);
    if (!providerConfig) {
      return;
    }

    return this.metricService.toFormGroup(providerConfig.properties);
  }

  handleFormSubmit(event: any) {
    const metric = {...this.metric, ...this.metricForm.value};
    metric.provider = this.providerForm.value;

    this.save.emit(metric);
  }

  handleCancel(event: any) {
    this.cancel.emit();
  }
}
