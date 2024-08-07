import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericService } from './genericService';
import { DepartmentDto } from '../dto/DepartmentDto';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService extends GenericService<DepartmentDto> {
  constructor(http: HttpClient) {
    super(http, 'api/Department');
  }
}
