import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Port, PortRequestDto } from '../models/port';

@Injectable({
  providedIn: 'root'
})
export class PortService {
  private baseUrl = 'http://localhost:5047/api/Port';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Port[]> {
    return this.http.get<Port[]>(`${this.baseUrl}/country`);
  }

  getById(id: number): Observable<Port> {
    return this.http.get<Port>(`${this.baseUrl}/${id}`);
  }

  create(dto: PortRequestDto): Observable<Port> {
    return this.http.post<Port>(this.baseUrl, dto);
  }

  update(id: number, dto: PortRequestDto): Observable<Port> {
    return this.http.put<Port>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number): Observable<Port> {
    return this.http.delete<Port>(`${this.baseUrl}/${id}`);
  }
}
