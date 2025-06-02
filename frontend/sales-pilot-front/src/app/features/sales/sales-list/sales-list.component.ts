import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SalesService, Sale } from '../sales.service';
import { StoreService, Store } from '../../stores/store.service'; 

@Component({
  selector: 'app-sales-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './sales-list.component.html',
  styleUrls: ['./sales-list.component.scss'],
})
export class SalesListComponent implements OnInit {
  sales: Sale[] = [];
  stores: Store[] = []; 
  loading = false;
  error: string | null = null;
  newSale: Partial<Sale> = { product: '', amount: 0, date: '', storeId: undefined };
  editSaleId?: number;
  editSaleData: Partial<Sale> = {};

  constructor(
    private salesService: SalesService,
    private storeService: StoreService 
  ) {}

  ngOnInit(): void {
    this.fetchSales();
    this.fetchStores(); 
  }

  fetchSales() {
    this.loading = true;
    this.salesService.getSales().subscribe({
      next: res => { this.sales = res.data; this.loading = false; },
      error: () => { this.error = 'Erreur lors du chargement des ventes'; this.loading = false; }
    });
  }

  fetchStores() {
    this.storeService.getStores().subscribe({
      next: res => { this.stores = res.data; },
      error: () => { this.error = "Erreur lors du chargement des magasins"; }
    });
  }

  createSale() {
    if (!this.newSale.product || !this.newSale.amount || !this.newSale.date || !this.newSale.storeId) return;
    this.loading = true;
    this.salesService.createSale(this.newSale).subscribe({
      next: res => {
        this.sales.push(res.data);
        this.newSale = { product: '', amount: 0, date: '', storeId: undefined };
        this.loading = false;
      },
      error: () => { this.error = "Erreur lors de l'ajout"; this.loading = false; }
    });
  }

  editSale(sale: Sale) {
    this.editSaleId = sale.id;
    this.editSaleData = { ...sale };
  }

  updateSale(id: number) {
    if (!this.editSaleData.product || !this.editSaleData.amount || !this.editSaleData.date || !this.editSaleData.storeId) return;
    this.loading = true;
    this.salesService.updateSale(id, this.editSaleData).subscribe({
      next: () => {
        const i = this.sales.findIndex(s => s.id === id);
        if (i > -1) this.sales[i] = { ...this.sales[i], ...this.editSaleData };
        this.editSaleId = undefined;
        this.loading = false;
      },
      error: () => { this.error = "Erreur lors de la modification"; this.loading = false; }
    });
  }

  deleteSale(id: number) {
    this.loading = true;
    this.salesService.deleteSale(id).subscribe({
      next: () => {
        this.sales = this.sales.filter(s => s.id !== id);
        this.loading = false;
      },
      error: () => { this.error = "Erreur lors de la suppression"; this.loading = false; }
    });
  }

  cancelEdit() {
    this.editSaleId = undefined;
    this.editSaleData = {};
  }

  // Optionnel : pour afficher le nom du magasin dans la liste
  getStoreName(storeId: number | undefined) {
    return this.stores.find(s => s.id === storeId)?.name || 'Magasin inconnu';
  }
  getStoreCity(storeId: number | undefined) {
    return this.stores.find(s => s.id === storeId)?.city || '';
  }
}