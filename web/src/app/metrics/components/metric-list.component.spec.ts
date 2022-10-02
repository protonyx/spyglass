import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MetricListComponent } from './metric-list.component';

describe('MetricListComponent', () => {
  let component: MetricListComponent;
  let fixture: ComponentFixture<MetricListComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MetricListComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MetricListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
