import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Sale {
  id: number;
  product: string;
  amount: number;
  date: string;
  storeId: number;
}

@Injectable({ providedIn: 'root' })
export class SalesService {
  private apiUrl = 'http://localhost:5268/api/sales';

  constructor(private http: HttpClient) {}

  getSales(): Observable<{ data: Sale[] }> {
    return this.http.get<{ data: Sale[] }>(this.apiUrl);
  }

  getSale(id: number): Observable<{ data: Sale }> {
    return this.http.get<{ data: Sale }>(`${this.apiUrl}/${id}`);
  }

  createSale(sale: Partial<Sale>): Observable<{ data: Sale }> {
    return this.http.post<{ data: Sale }>(this.apiUrl, sale);
  }

  updateSale(id: number, sale: Partial<Sale>): Observable<{ data: Sale }> {
    return this.http.put<{ data: Sale }>(`${this.apiUrl}/${id}`, sale);
  }

  deleteSale(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}