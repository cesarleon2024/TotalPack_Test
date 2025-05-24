import { Routes } from '@angular/router';
import { UserListComponent, DeleteConfirmationDialog } from './components/users/user-list.component';

export const routes: Routes = [
  { path: '', redirectTo: '/users', pathMatch: 'full' },
  { path: 'users', component: UserListComponent }
];
