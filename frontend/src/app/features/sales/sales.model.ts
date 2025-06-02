// src/app/features/sales/sales.model.ts

export interface Sale {
  id: number;
  product: string;
  amount: number;
  date: string;      // ISO string: '2025-05-28T10:40:00Z'
  storeId: number;   // Id du magasin lié
  storeName?: string; // Optionnel, pour affichage rapide
}

export interface SaleCreateDto {
  product: string;
  amount: number;
  date: string;     // '2025-05-28T10:40:00Z'
  storeId: number;
}

export interface SaleUpdateDto {
  product: string;
  amount: number;
  date: string;
  storeId: number;
}