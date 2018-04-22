import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs/Observable';
import {Metric} from '../metrics/models/metric';

import {environment} from '../../environments/environment';
import {MetricGroup} from '../metrics/models/metricGroup';

@Injectable()
export class MetricService {

  constructor(private http: HttpClient) { }

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
}
