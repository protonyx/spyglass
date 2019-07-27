import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from '../../../environments/environment';
import { Metric } from '../models/metric';
import { MetricValue } from '../models/metric-value';
import { MetricProvider } from '../models/metricProvider';

@Injectable()
export class MetricService {
    constructor(private http: HttpClient) {}

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

    testProvider(providerName: string, providerOptions: any): Observable<MetricValue[]> {
        const url = `${environment.apiUrl}/api/Provider/${providerName}/Test`;

        return this.http.post<MetricValue[]>(url, providerOptions);
    }
}
