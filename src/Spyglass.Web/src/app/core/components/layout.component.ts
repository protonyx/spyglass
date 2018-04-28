import {Component, Input, OnInit} from '@angular/core';
import {NavItem} from "../models/navItem";

@Component({
  selector: 'sg-layout',
  template: `
    <ng-container>
      <!-- Primary Toolbar -->
      <mat-toolbar class="sg-header mat-elevation-z6" color="primary">
        <button mat-icon-button
                class="sg-toolbar-icon"
                (click)="sidenav.toggle()">
          <mat-icon>menu</mat-icon>
        </button>

        <span>Spyglass</span>
        <span class="sg-fill-remaining"></span>
      </mat-toolbar>

      <mat-sidenav-container class="sg-container">
        <mat-sidenav #sidenav
                     mode="side"
                     [opened]="true"
                     [fixedInViewport]="false"
                     role="navigation"
                     class="sg-sidenav">
          <!-- sidenav content -->
          <mat-nav-list>
            <a mat-list-item
               *ngFor="let navItem of navItems"
               [routerLink]="[navItem.route]">
              <mat-icon matListIcon>{{ navItem.icon }}</mat-icon>
              <span matLine>{{ navItem.name }}</span>
            </a>
          </mat-nav-list>
        </mat-sidenav>

        <mat-sidenav-content>
          <!-- main content -->
          <router-outlet></router-outlet>
        </mat-sidenav-content>
      </mat-sidenav-container>
    </ng-container>
  `,
  styles: [`
    .sg-header {
      position: fixed;
      top: 0;
      left: 0;
      right: 0;

      .sg-toolbar-icon {
        padding: 24px 16px;
      }
  
      .sg-fill-remaining {
        flex: 1 1 auto;
      }
    }

    .sg-sidenav {
      display: flex;
      width: 200px;
    }

    .sg-container {
      position: absolute;
      top: 60px;
      bottom: 60px;
      left: 0;
      right: 0;
    }

    .sg-footer {
      position: fixed;
      bottom: 0;
      left: 0;
      right: 0;
    }
    
    mat-toolbar {
      z-index: 1000;
    }
    
    mat-sidenav-container {
      background: rgba(0, 0, 0, 0.03);
    }
  `]
})
export class LayoutComponent implements OnInit {
  @Input() navItems: NavItem[];

  constructor() { }

  ngOnInit() {
  }

}
