import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Country, CountryRequestDto } from '../models/country';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CountryService {
  private baseUrl = 'http://localhost:5047/api/Country';

  constructor(private http: HttpClient) {}

  getAll(): Observable<Country[]> {
    return this.http.get<Country[]>(this.baseUrl);
  }

  getById(id: number): Observable<Country> {
    return this.http.get<Country>(`${this.baseUrl}/${id}`);
  }

  create(dto: CountryRequestDto): Observable<Country> {
    return this.http.post<Country>(this.baseUrl, dto);
  }

  update(id: number, dto: CountryRequestDto): Observable<Country> {
    return this.http.put<Country>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number): Observable<Country> {
    return this.http.delete<Country>(`${this.baseUrl}/${id}`);
  }
}
