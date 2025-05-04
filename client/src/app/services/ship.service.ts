import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Ship, ShipRequestDto } from '../models/ship';

@Injectable({
  providedIn: 'root'
})
export class ShipService {
  private baseUrl = 'http://localhost:5047/api/Ship';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Ship[]> {
    return this.http.get<Ship[]>(this.baseUrl);
  }

  getById(id: number): Observable<Ship> {
    return this.http.get<Ship>(`${this.baseUrl}/${id}`);
  }

  create(dto: ShipRequestDto): Observable<Ship> {
    return this.http.post<Ship>(this.baseUrl, dto);
  }

  update(id: number, dto: ShipRequestDto): Observable<Ship> {
    return this.http.put<Ship>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number): Observable<Ship> {
    return this.http.delete<Ship>(`${this.baseUrl}/${id}`);
  }
}
