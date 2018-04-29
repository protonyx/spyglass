import {Component, Inject, OnInit} from '@angular/core';
import {MetricGroup} from "../models/metricGroup";
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material";


@Component({
  selector: 'sg-metric-group-editor-dialog',
  template: `
    <h1 mat-dialog-title>Group</h1>
    <div mat-dialog-content>
      <sg-metric-group-editor [group]="data.group">
        
      </sg-metric-group-editor>
    </div>
    <div mat-dialog-actions>
      <button mat-button (click)="onNoClick()">Close</button>
      <button mat-button [mat-dialog-close]="data.group" cdkFocusInitial>Save</button>
    </div>
  `,
  styles: []
})
export class MetricGroupEditorDialogComponent implements OnInit {
  constructor(public dialogRef: MatDialogRef<MetricGroupEditorDialogComponent>,
              @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    console.log(this.data);
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}
