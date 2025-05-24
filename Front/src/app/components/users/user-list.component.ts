import { Component, OnInit } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialog } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { environment } from '../../../environments/environment';

interface User {
  id: number;
  fullName: string;
  birth: string;
  email: string;
  mainAddress: string;
}

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [
    CommonModule,
    MatTableModule,
    MatPaginatorModule,
    MatButtonModule,
    MatDialogModule,
    MatIconModule,
    HttpClientModule
  ],
  template: `
    <div class="container">
      <div class="header">
        <h1>Administrador de Usuarios</h1>
        <button mat-raised-button color="primary" (click)="onNewUser()">
          Nuevo Usuario
        </button>
      </div>

      <mat-table [dataSource]="users">
        <ng-container matColumnDef="fullName">
          <mat-header-cell *matHeaderCellDef>Nombre Completo</mat-header-cell>
          <mat-cell *matCellDef="let user">{{user.fullName}}</mat-cell>
        </ng-container>

        <ng-container matColumnDef="birth">
          <mat-header-cell *matHeaderCellDef>Fecha Nacimiento</mat-header-cell>
          <mat-cell *matCellDef="let user">{{user.birth | date:'dd/MM/yyyy'}}</mat-cell>
        </ng-container>

        <ng-container matColumnDef="email">
          <mat-header-cell *matHeaderCellDef>Email</mat-header-cell>
          <mat-cell *matCellDef="let user">{{user.email}}</mat-cell>
        </ng-container>

        <ng-container matColumnDef="mainAddress">
          <mat-header-cell *matHeaderCellDef>Dirección Principal</mat-header-cell>
          <mat-cell *matCellDef="let user">{{user.mainAddress}}</mat-cell>
        </ng-container>

        <ng-container matColumnDef="actions">
          <mat-header-cell *matHeaderCellDef>&nbsp;</mat-header-cell>
          <mat-cell *matCellDef="let user">
            <button mat-icon-button color="primary" (click)="onEditUser(user.id)" matTooltip="Editar">
              <mat-icon>edit</mat-icon>
            </button>
            <button mat-icon-button color="warn" (click)="onDeleteUser(user.id)" matTooltip="Eliminar">
              <mat-icon>delete</mat-icon>
            </button>
          </mat-cell>
        </ng-container>

        <mat-header-row *matHeaderRowDef="displayedColumns"></mat-header-row>
        <mat-row *matRowDef="let row; columns: displayedColumns;"></mat-row>
      </mat-table>

      <mat-paginator
        [pageSize]="10"
        [pageSizeOptions]="[10]"
        [length]="users.length">
      </mat-paginator>
    </div>
  `,
  styles: [`
    .container {
      padding: 20px;
    }
    .header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      margin-bottom: 20px;
    }
    mat-table {
      width: 100%;
    }
    .mat-column-actions {
      width: 120px;
      text-align: center;
    }
  `]
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  displayedColumns: string[] = ['fullName', 'birth', 'email', 'mainAddress', 'actions'];

  constructor(
    private dialog: MatDialog,
    private http: HttpClient
  ) {}

  ngOnInit() {
    this.fetchUsers();
  }

  fetchUsers() {
    this.http.get<User[]>(environment.API_GetUsers)
      .subscribe({
        next: (data) => this.users = data,
        error: (error) => console.error('Error fetching users:', error)
      });
  }

  onNewUser() {
    console.log('Clicked Nuevo Usuario');
  }

  onEditUser(userId: number) {
    console.log('Clicked editar', userId);
  }

  onDeleteUser(userId: number) {
    const dialogRef = this.dialog.open(DeleteConfirmationDialog);

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log('Clicked eliminar', userId);
      }
    });
  }
}

@Component({
  selector: 'delete-confirmation-dialog',
  standalone: true,
  imports: [
    MatDialogModule,
    MatButtonModule
  ],
  template: `
    <h2 mat-dialog-title>Confirmar Eliminación</h2>
    <mat-dialog-content>
      ¿Está seguro que desea eliminar este usuario?
    </mat-dialog-content>
    <mat-dialog-actions>
      <button mat-button [mat-dialog-close]="false">Cancelar</button>
      <button mat-button color="warn" [mat-dialog-close]="true">Eliminar</button>
    </mat-dialog-actions>
  `
})
export class DeleteConfirmationDialog {} 