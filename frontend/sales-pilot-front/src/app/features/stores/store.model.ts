export interface Store {
  id: number;
  name: string;
  city: string;
}

export interface StoreCreateDto {
  name: string;
  city: string;
}

export interface StoreUpdateDto {
  name: string;
  city: string;
}