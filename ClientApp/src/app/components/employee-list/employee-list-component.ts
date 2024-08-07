import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { EmployeeService } from '../../services/employeeService';
import {OperationResult} from "../../dto/OperationResult";
import {EmployeeDto} from "../../dto/EmployeeDto";

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list-component.html',
  styleUrls: ['./employee-list-component.css']
})
export class EmployeeListComponent implements OnInit {
  employees: EmployeeDto[] = [];

  constructor(private employeeService: EmployeeService, private router: Router) { }

  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees(): void {
    this.employeeService.getAll().subscribe((response: OperationResult<EmployeeDto[]>) => {
      console.log(response);
      if (response.status === 0) {
        this.employees = response.result;
      } else {
        console.error(response.errorMessage);
      }
    });
  }

  onEdit(id: number): void {
    this.router.navigate(['/employee/edit', id]);
  }

  onDelete(id: number): void {
    this.employeeService.delete(id).subscribe(() => {
      this.getEmployees();
    });
  }
}
