import {Component, OnInit, Input, Output, EventEmitter, OnChanges, SimpleChanges, OnDestroy} from '@angular/core';
import {FormBuilder, FormGroup, Validators} from '@angular/forms';
import {Metric} from '../models/metric';
import {MetricProvider} from '../models/metricProvider';
import {Subscription} from 'rxjs';
import {MetricService} from '../services/metric.service';

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

  public currentProvider: MetricProvider;

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
        console.log(val);
        this.currentProvider = this.providers.find(t => t.name === val);
        console.log(this.currentProvider);
        this.providerForm = this.buildProviderForm(val);
        console.log(this.providerForm);
    })
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['metric']) {
      this.metricForm.setValue(this.metric);
    }
  }

  ngOnDestroy(): void {
    this.providerTypeSubscription.unsubscribe();
  }

  buildProviderForm(providerType: string) {
    const providerConfig = this.providers.find(t => t.name === providerType);
    return this.metricService.toFormGroup(providerConfig.properties);
  }

  handleFormSubmit(event: any) {
    let metric = Object.assign(new Metric(), this.metricForm.value);
    metric.provider = this.providerForm.value;

    this.save.emit(metric);
  }
}
