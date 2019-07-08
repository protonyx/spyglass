import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Metric } from '../models/metric';
import { MetricProvider } from '../models/metricProvider';
import { ModelPropertyMetadata } from '../models/modelPropertyMetadata';

@Injectable()
export class MetricService {

  constructor(private http: HttpClient) { }

  getMetrics(): Observable<Metric[]> {
    const url = `${environment.apiUrl}/api/Metric`;

    return this.http.get<Metric[]>(url);
  }

  getMetric(id: string): Observable<Metric> {
    const url = `${environment.apiUrl}/api/Metric/${id}`;

    return this.http.get<Metric>(url);
  }

  createMetric(data: Metric): Observable<Metric> {
    const url = `${environment.apiUrl}/api/Metric`;

    return this.http.post<Metric>(url, data);
  }

  updateMetric(data: Metric): Observable<Metric> {
    const url = `${environment.apiUrl}/api/Metric/${data.id}`;

    return this.http.put<Metric>(url, data);
  }

  deleteMetric(id: string): Observable<any> {
    const url = `${environment.apiUrl}/api/Metric/${id}`;

    return this.http.delete(url);
  }

  getProviderMetadata(): Observable<MetricProvider[]> {
    const url = `${environment.apiUrl}/api/Provider`;

    return this.http.get<MetricProvider[]>(url);
  }

  toFormGroup(config: ModelPropertyMetadata[]) {
    const group: any = {};

    config.forEach(c => {
      group[c.propertyName] = c.isRequired ? new FormControl('', Validators.required)
                                   : new FormControl('');
    });

    return new FormGroup(group);
  }
}
