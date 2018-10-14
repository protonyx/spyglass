import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {Metric} from '../models/metric';

import {environment} from '../../../environments/environment';
import {MetricGroup} from '../models/metricGroup';
import {MetricProvider} from '../models/metricProvider';

@Injectable()
export class MetricService {

  constructor(private http: HttpClient) { }

  getGroups(): Observable<MetricGroup[]> {
    const url = `${environment.apiUrl}/api/MetricGroup`;

    return this.http.get<MetricGroup[]>(url);
  }

  getGroup(id: string): Observable<MetricGroup> {
    const url = `${environment.apiUrl}/api/MetricGroup/${id}`;

    return this.http.get<MetricGroup>(url);
  }

  createGroup(data: MetricGroup): Observable<MetricGroup> {
    const url = `${environment.apiUrl}/api/MetricGroup`;

    return this.http.post<MetricGroup>(url, data);
  }

  deleteGroup(id: string): Observable<any> {
    const url = `${environment.apiUrl}/api/MetricGroup/${id}`;

    return this.http.delete(url);
  }

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
}
