import { createAction, props } from '@ngrx/store';
import { Connection } from '../../models/connection.model';

export const retrievedConnectionList = createAction(
  '[API] Retrieved Connections Success',
  props<{ connections: Array<Connection> }>()
);
