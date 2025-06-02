import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { StoreService, Store } from '../store.service';

@Component({
  selector: 'app-store-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './store-list.component.html',
  styleUrls: ['./store-list.component.scss'],
})
export class StoreListComponent implements OnInit {
  loading = false;
  error: string | null = null;
  stores: Store[] = [];
  newStore = { name: '', city: '' };

  // Pour l’édition
  editStoreId: number | null = null;
  editStoreData = { name: '', city: '' };

  constructor(private storeService: StoreService) {}

  ngOnInit(): void {
    this.fetchStores();
  }

  fetchStores(): void {
    this.loading = true;
    this.storeService.getStores().subscribe({
      next: (response) => {
        this.stores = response.data;
        this.loading = false;
      },
      error: () => {
        this.error = 'Erreur lors du chargement des magasins';
        this.loading = false;
      },
    });
  }

  createStore(): void {
    this.loading = true;
    this.storeService.createStore(this.newStore).subscribe({
      next: (res) => {
        this.stores.push(res.data);
        this.newStore = { name: '', city: '' };
        this.loading = false;
      },
      error: () => {
        this.error = "Erreur lors de l'ajout";
        this.loading = false;
      },
    });
  }

  // -- Edition
  // Active l'édition et préremplit les champs
  editStore(store: Store): void {
    this.editStoreId = store.id;
    this.editStoreData = { name: store.name, city: store.city };
  }

  // Annule l'édition
  cancelEdit(): void {
    this.editStoreId = null;
    this.editStoreData = { name: '', city: '' };
  }

  // Envoie la maj au backend
  updateStore(id: number) {
    this.loading = true;
    this.storeService.updateStore(id, this.editStoreData).subscribe({
      next: (res: any) => {
        // Si l’API retourne { data: {...} }
        if (res.data) {
          const index = this.stores.findIndex((s) => s.id === id);
          if (index !== -1) {
            this.stores[index] = res.data;
          }
        }
        // sinon fallback :
        else if (res.id && res.name && res.city) {
          const index = this.stores.findIndex((s) => s.id === id);
          if (index !== -1) {
            this.stores[index] = res;
          }
        }
        this.editStoreId = null;
        this.editStoreData = { name: '', city: '' };
        this.loading = false;
      },
      error: () => {
        this.error = 'Erreur lors de la modification';
        this.loading = false;
      },
    });
  }

  deleteStore(id: number): void {
    this.loading = true;
    this.storeService.deleteStore(id).subscribe({
      next: () => {
        this.stores = this.stores.filter((s) => s.id !== id);
        this.loading = false;
      },
      error: () => {
        this.error = 'Erreur lors de la suppression';
        this.loading = false;
      },
    });
  }
}
