import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Monitor } from '../models/monitor.model';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';
import { Connection } from '../models/connection.model';

@Injectable({
  providedIn: 'root'
})
export class MonitorService {
  constructor(private http: HttpClient) {}

  getMonitors(): Observable<Monitor[]> {
    const url = `${environment.apiUrl}/api/Monitors`;

    return this.http.get<Monitor[]>(url);
  }

  getMonitor(id: string): Observable<Monitor> {
    const url = `${environment.apiUrl}/api/Monitors/${id}`;

    return this.http.get<Monitor>(url);
  }

  createMonitor(data: Monitor): Observable<Monitor> {
    const url = `${environment.apiUrl}/api/Monitors`;

    return this.http.post<Monitor>(url, data);
  }

  updateMonitor(data: Monitor): Observable<Monitor> {
    const url = `${environment.apiUrl}/api/Monitors/${data.id}`;

    return this.http.put<Monitor>(url, data);
  }

  deleteMonitor(id: string): Observable<any> {
    const url = `${environment.apiUrl}/api/Monitors/${id}`;

    return this.http.delete(url);
  }

  getConnections(): Observable<Connection[]> {
    const url = `${environment.apiUrl}/api/Connections`;

    return this.http.get<Connection[]>(url);
  }
}
