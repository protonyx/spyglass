import { Action } from '@ngrx/store';

import { LayoutActionsUnion, LayoutActionTypes } from './layout.actions';

export interface State {
    showSidenav: boolean;
}

export const initialState: State = {
    showSidenav: true
};

export function reducer(state = initialState, action: LayoutActionsUnion): State {
    switch (action.type) {
        case LayoutActionTypes.CloseSidenav:
            return {
                ...state,
                showSidenav: false
            };

        case LayoutActionTypes.OpenSidenav:
            return {
                ...state,
                showSidenav: true
            };

        case LayoutActionTypes.ToggleSidenav:
            return {
                ...state,
                showSidenav: !state.showSidenav
            };

        default:
            return state;
    }
}

export const getShowSidenav = (state: State) => state.showSidenav;
