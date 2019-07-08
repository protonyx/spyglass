import { inject, TestBed } from '@angular/core/testing';
import { provideMockActions } from '@ngrx/effects/testing';
import { Observable } from 'rxjs';

import { MetricsEffects } from './metrics.effects';

describe('MetricsService', () => {
  const actions$: Observable<any>;
  const effects: MetricsEffects;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        MetricsEffects,
        provideMockActions(() => actions$)
      ]
    });

    effects = TestBed.get(MetricsEffects);
  });

  it('should be created', () => {
    expect(effects).toBeTruthy();
  });
});
