import { Component } from '@angular/core';
import { RouterModule } from '@angular/router'; 
import { StoreListComponent } from './features/stores/store-list/store-list.component';
import { SalesListComponent } from './features/sales/sales-list/sales-list.component';


@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterModule, StoreListComponent, SalesListComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent {}
