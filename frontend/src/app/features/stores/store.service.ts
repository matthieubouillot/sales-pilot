import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

// Le type pour 1 magasin (id peut être optionnel à la création)
export interface Store {
  id: number;
  name: string;
  city: string;
}

// Pour le format de réponse de ton backend
export interface ApiResponse<T> {
  success: boolean;
  data: T;
  message?: string | null;
}

@Injectable({
  providedIn: 'root',
})
export class StoreService {
  private apiUrl = 'http://localhost:5268/api/stores';

  constructor(private http: HttpClient) {}

  // GET tous les magasins
  getStores(): Observable<ApiResponse<Store[]>> {
    return this.http.get<ApiResponse<Store[]>>(this.apiUrl);
  }

  // POST un nouveau magasin
  createStore(store: Omit<Store, 'id'>): Observable<ApiResponse<Store>> {
    return this.http.post<ApiResponse<Store>>(this.apiUrl, store);
  }

  // GET un magasin par ID
  getStore(id: number): Observable<ApiResponse<Store>> {
    return this.http.get<ApiResponse<Store>>(`${this.apiUrl}/${id}`);
  }

  // PUT mise à jour magasin
  updateStore(id: number, data: { name: string; city: string }) {
    return this.http.put<{ success: boolean; data: Store }>(
      `${this.apiUrl}/${id}`,
      data
    );
  }

  // DELETE magasin
  deleteStore(id: number): Observable<ApiResponse<null>> {
    return this.http.delete<ApiResponse<null>>(`${this.apiUrl}/${id}`);
  }
}
