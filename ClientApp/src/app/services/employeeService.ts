import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericService } from './genericService';
import { EmployeeDto } from '../dto/EmployeeDto';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService extends GenericService<EmployeeDto> {
  constructor(http: HttpClient) {
    super(http, 'api/Employee');
  }
}
