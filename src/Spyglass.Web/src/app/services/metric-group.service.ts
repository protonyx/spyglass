import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Observable} from 'rxjs';

import { MetricGroup } from '../metrics/models/metricGroup';
import {environment} from '../../environments/environment';

@Injectable()
export class MetricGroupService {
  constructor(private http: HttpClient) {

  }

  getGroups(): Observable<MetricGroup[]> {
    const url = `${environment.apiUrl}/api/MetricGroup`;

    return this.http.get<MetricGroup[]>(url);
  }

  createGroup(data: MetricGroup): Observable<MetricGroup> {
    const url = `${environment.apiUrl}/api/MetricGroup`;

    return this.http.post<MetricGroup>(url, data);
  }

  deleteGroup(id: string): Observable<any> {
    const url = `${environment.apiUrl}/api/MetricGroup/${id}`;

    return this.http.delete(url);
  }
}
