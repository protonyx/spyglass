import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MonitorEditorComponent } from './monitor-editor.component';

describe('MetricEditorComponent', () => {
  let component: MonitorEditorComponent;
  let fixture: ComponentFixture<MonitorEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [MonitorEditorComponent]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MonitorEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
