import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

import { Context } from '../models/context';

@Injectable()
export class ContextService {
  constructor(private http: HttpClient) {

  }

  getContexts() : Observable<Context> {
    const url = `api/Context`;

    return this.http.get<Context>(url);
  }
}
