import { TestBed, inject } from '@angular/core/testing';

import { GroupExistsGuard } from './group-exists.guard';

describe('GroupExistsGuard', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [GroupExistsGuard]
    });
  });

  it('should be created', inject([GroupExistsGuard], (service: GroupExistsGuard) => {
    expect(service).toBeTruthy();
  }));
});
