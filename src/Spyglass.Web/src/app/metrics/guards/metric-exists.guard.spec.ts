import { TestBed, async, inject } from '@angular/core/testing';

import { MetricExistsGuard } from './metric-exists.guard';

describe('MetricExistsGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MetricExistsGuard]
    });
  });

  it('should ...', inject([MetricExistsGuard], (guard: MetricExistsGuard) => {
    expect(guard).toBeTruthy();
  }));
});
