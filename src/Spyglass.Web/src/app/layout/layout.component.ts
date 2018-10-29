import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {NavItem} from "./navItem";
import {Observable} from "rxjs";
import {select, Store} from "@ngrx/store";
import * as fromRoot from '../reducers/index';
import * as LayoutActions from './layout.actions';

@Component({
  selector: 'sg-layout',
  template: `
    <clr-main-container class="main-container">
      <!--<div class="alert alert-app-level">
        ...
      </div>-->
      <sg-header
        [appName]="appName">
      </sg-header>
      
      <div class="content-container">
        <div class="content-area">
          <router-outlet></router-outlet>
        </div>
        
        <sg-sidenav></sg-sidenav>
      </div>
    </clr-main-container>
  `
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
    <clr-header class="header header-6">
      <button class="header-hamburger-trigger" type="button"><span></span></button>
      <div class="branding">
        <a href="#">
          <span class="clr-icon brand-logo">
            <clr-icon shape="search"></clr-icon>
          </span>
          <span class="title">{{appName}}</span>
        </a>
      </div>
      <div class="header-nav">
        
      </div>
      <div class="header-actions">
        <a href="javascript://" class="nav-link nav-icon">
          <clr-icon shape="cog"></clr-icon>
        </a>
      </div>
    </clr-header>
  `,
  styles: [`
    .brand-logo {
      margin-right: 11px;
      height: 36px;
      width: 36px;
    }
  `],
  encapsulation: ViewEncapsulation.None
})
export class HeaderComponent {
  @Input() appName: string;
}

@Component({
  selector: 'sg-sidenav',
  template: `
    <nav class="sidenav">
      <section class="sidenav-content">
        <section class="nav-group">
          <input id="navgroup1" type="checkbox">
          <label for="navgroup1">Metrics</label>
          <ul class="nav-list">
            <li><a routerLink="/metrics" routerLinkActive="active" class="nav-link">Metrics</a></li>
          </ul>
        </section>
      </section>
    </nav>
  `
})
export class SideNavComponent {
  @Input() navItems: any[];
}
