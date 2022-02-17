import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Metric } from '../models/metric.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Connection } from '../models/connection.model';

@Injectable({
  providedIn: 'root'
})
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

  getConnections(): Observable<Connection[]> {
    const url = `${environment.apiUrl}/api/Connection`;

    return this.http.get<Connection[]>(url);
  }
}
