import { Component, EventEmitter, Input, OnInit, Output, ViewEncapsulation } from '@angular/core';
import { select, Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import * as fromRoot from '../reducers/index';

import * as LayoutActions from './layout.actions';
import { NavItem } from './navItem';

@Component({
    selector: 'sg-layout',
    template: `
        <clr-main-container class="main-container">
            <!--<div class="alert alert-app-level">
        ...
      </div>-->
            <sg-header [appName]="appName"> </sg-header>

            <div class="content-container">
                <sg-sidenav
                    [navItems]="navItems"
                    [showSidenav]="showSidenav$ | async"
                    (sidenavCollapsed)="$event ? closeSidenav() : openSidenav()"
                ></sg-sidenav>

                <div class="content-area">
                    <router-outlet></router-outlet>
                </div>
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

    ngOnInit() {}

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
            <button class="header-hamburger-trigger" type="button">
                <span></span>
            </button>
            <div class="branding">
                <a href="#">
                    <span class="clr-icon brand-logo">
                        <clr-icon shape="search"></clr-icon>
                    </span>
                    <span class="title">{{ appName }}</span>
                </a>
            </div>
            <div class="header-nav"></div>
            <div class="header-actions">
                <a href="javascript://" class="nav-link nav-icon">
                    <clr-icon shape="cog"></clr-icon>
                </a>
            </div>
        </clr-header>
    `,
    styles: [
        `
            .brand-logo {
                margin-right: 11px;
                height: 36px;
                width: 36px;
            }
        `
    ],
    encapsulation: ViewEncapsulation.None
})
export class HeaderComponent {
    @Input() appName: string;
}

@Component({
    selector: 'sg-sidenav',
    template: `
        <clr-vertical-nav
            [clrVerticalNavCollapsible]="true"
            [clrVerticalNavCollapsed]="!showSidenav"
            (clrVerticalNavCollapsedChanged)="sidenavCollapsed.emit($event)"
        >
            <a clrVerticalNavLink *ngFor="let item of navItems" [routerLink]="item.route" routerLinkActive="active">
                {{ item.name }}
            </a>
        </clr-vertical-nav>
    `
})
export class SideNavComponent {
    @Input() navItems: NavItem[];

    @Input() showSidenav: boolean;

    @Output() readonly sidenavCollapsed = new EventEmitter<boolean>();
}
