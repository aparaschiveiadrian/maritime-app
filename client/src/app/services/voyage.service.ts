import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VoyageRequestDto, VoyageResponseDto } from '../models/voyage';

@Injectable({
  providedIn: 'root'
})
export class VoyageService {
  private baseUrl = 'http://localhost:5047/api/Voyage';

  constructor(private http: HttpClient) {}

  getAll(): Observable<VoyageResponseDto[]> {
    return this.http.get<VoyageResponseDto[]>(this.baseUrl);
  }

  getById(id: number): Observable<VoyageResponseDto> {
    return this.http.get<VoyageResponseDto>(`${this.baseUrl}/${id}`);
  }

  create(dto: VoyageRequestDto): Observable<VoyageResponseDto> {
    return this.http.post<VoyageResponseDto>(this.baseUrl, dto);
  }

  update(id: number, dto: VoyageRequestDto): Observable<VoyageResponseDto> {
    return this.http.put<VoyageResponseDto>(`${this.baseUrl}/${id}`, dto);
  }

  delete(id: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${id}`);
  }
}
