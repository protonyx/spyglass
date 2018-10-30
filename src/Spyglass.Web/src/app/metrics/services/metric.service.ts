import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Metric} from '../models/metric';

import {environment} from '../../../environments/environment';
import {MetricProvider} from '../models/metricProvider';
import {ModelPropertyMetadata} from "../models/modelPropertyMetadata";
import {FormControl, FormGroup, Validators} from "@angular/forms";

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
    let group: any = {};

    config.forEach(c => {
      group[c.propertyName] = c.isRequired ? new FormControl('', Validators.required)
                                   : new FormControl('');
    });

    return new FormGroup(group);
  }
}
