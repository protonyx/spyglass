import { Component, EventEmitter, Input, OnChanges, OnDestroy, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';

import { Metric } from '../models/metric';
import { MetricValue } from '../models/metric-value';
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
    @Output() readonly save = new EventEmitter<Metric>();
    @Output() readonly cancel = new EventEmitter();

    currentProvider: MetricProvider;

    testResults: MetricValue[];

    metricForm = this.fb.group({
        name: ['', Validators.required],
        description: [''],
        providerType: ['', Validators.required],
        provider: this.fb.group({})
    });

    get provider(): FormGroup {
        return this.metricForm.get('provider') as FormGroup;
    }

    providerTypeSubscription: Subscription;

    constructor(private fb: FormBuilder, private metricService: MetricService) {}

    ngOnInit(): void {
        this.providerTypeSubscription = this.metricForm.get('providerType').valueChanges.subscribe(val => {
            if (!this.currentProvider || this.currentProvider.name !== val) {
                this.currentProvider = this.providers.find(t => t.name === val);
                this.updateProviderForm(val);
            }
        });
    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['metric'] && this.metric) {
            this.load(this.metric);
        }
    }

    ngOnDestroy(): void {
        this.providerTypeSubscription.unsubscribe();
    }

    load(data: Metric): void {
        this.metricForm.patchValue({
            name: data.name,
            description: data.description,
            providerType: data.providerType
        });

        if (!this.currentProvider || this.currentProvider.name !== data.providerType) {
            this.currentProvider = this.providers.find(t => t.name === this.metric.providerType);
            this.updateProviderForm(data.providerType);
        }

        this.provider.patchValue(data.provider);
    }

    updateProviderForm(providerType: string): void {
        if (!providerType) {
            return;
        }

        const fg = this.buildProviderForm(providerType);
        this.metricForm.setControl('provider', fg);
    }

    buildProviderForm(providerType: string): FormGroup {
        const providerConfig = this.providers.find(t => t.name === providerType);
        if (!providerConfig) {
            return;
        }

        const group: any = {};

        providerConfig.properties.forEach(c => {
            group[c.propertyName] = c.isRequired ? new FormControl('', Validators.required) : new FormControl('');
        });

        return new FormGroup(group);
    }

    handleFormSubmit(event: any): void {
        const metric = { ...this.metric, ...this.metricForm.value };

        this.save.emit(metric);
    }

    handleTestMetric(): void {
        this.metricService.testProvider(this.currentProvider.name, this.provider.value).subscribe(data => {
            this.testResults = data;
        });
    }

    handleCancel(event: any): void {
        this.cancel.emit();
    }
}
