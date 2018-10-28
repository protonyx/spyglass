import {Component, Input, OnInit} from '@angular/core';
import {NavItem} from "./navItem";
import {Observable} from "rxjs";
import {select, Store} from "@ngrx/store";
import * as fromRoot from '../reducers/index';
import * as LayoutActions from './layout.actions';

@Component({
  selector: 'sg-layout',
  template: `
    <div class="main-container">
      <!--<div class="alert alert-app-level">
        ...
      </div>-->
      <sg-header></sg-header>
      
      <nav class="subnav">
        ...
      </nav>
      <div class="content-container">
        <div class="content-area">
          <router-outlet></router-outlet>
        </div>
        
        <sg-sidenav></sg-sidenav>
      </div>
    </div>
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

  @Input() appName: string;

  @Input() navItems: NavItem[];

  showSidenav$: Observable<boolean>;

  constructor(private store: Store<fromRoot.State>) {
    this.showSidenav$ = this.store.pipe(select(fromRoot.getShowSidenav));
  }

  ngOnInit() {
  }

  closeSidenav() {
    /**
     * All state updates are handled through dispatched actions in 'container'
     * components. This provides a clear, reproducible history of state
     * updates and user interaction through the life of our
     * application.
     */
    this.store.dispatch(new LayoutActions.CloseSidenav());
  }

  openSidenav() {
    this.store.dispatch(new LayoutActions.OpenSidenav());
  }

  toggleSidenav() {
    this.store.dispatch(new LayoutActions.ToggleSidenav());
  }
}

@Component({
  selector: 'sg-header',
  template: `
    <header class="header header-6">
      <div class="branding">
        
      </div>
      <div class="header-nav">
        
      </div>
      <div class="header-actions">
        <a href="javascript://" class="nav-link nav-icon">
          <clr-icon shape="cog"></clr-icon>
        </a>
      </div>
    </header>
  `
})
export class HeaderComponent {

}

@Component({
  selector: 'sg-sidenav',
  template: `
    <nav class="sidenav">
      <section class="sidenav-content">
        
      </section>
    </nav>
  `
})
export class SideNavComponent {
  @Input() navItems: any[];
}
