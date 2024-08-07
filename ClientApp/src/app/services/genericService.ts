import { Injectable, Inject, InjectionToken } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OperationResult } from "../dto/OperationResult";
import {environment} from "../../environments/environment";

export const ENDPOINT = new InjectionToken<string>('endpoint');

@Injectable({
  providedIn: 'root'
})
export class GenericService<T> {
  private readonly apiUrl: string;

  constructor(private http: HttpClient, @Inject(ENDPOINT) endpoint: string) {
    this.apiUrl = `${environment.apiUrl}/${endpoint}`;
  }

  getAll(): Observable<OperationResult<T[]>> {
    return this.http.get<OperationResult<T[]>>(this.apiUrl);
  }

  getById(id: number): Observable<OperationResult<T>> {
    return this.http.get<OperationResult<T>>(`${this.apiUrl}/${id}`);
  }

  create(item: T): Observable<OperationResult<any>> {
    return this.http.post<OperationResult<any>>(this.apiUrl, item);
  }

  update(id: number, item: T): Observable<OperationResult<any>> {
    return this.http.put<OperationResult<any>>(`${this.apiUrl}/${id}`, item);
  }

  delete(id: number): Observable<OperationResult<any>> {
    return this.http.delete<OperationResult<any>>(`${this.apiUrl}/${id}`);
  }
}
