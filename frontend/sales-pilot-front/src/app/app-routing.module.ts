import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StoreListComponent } from './features/stores/store-list/store-list.component';
import { SalesListComponent } from './features/sales/sales-list/sales-list.component'; // 👈 Importe le composant Sales

export const routes: Routes = [
  { path: 'stores', component: StoreListComponent },
  { path: 'sales', component: SalesListComponent }, 
  { path: '', redirectTo: '/stores', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}