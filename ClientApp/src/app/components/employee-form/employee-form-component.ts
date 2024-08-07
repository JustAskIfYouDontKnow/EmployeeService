import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { EmployeeService } from "../../services/employeeService";
import { EmployeeDto } from "../../dto/EmployeeDto";
import { DepartmentDto } from "../../dto/DepartmentDto";
import { ProgrammingLanguageDto } from "../../dto/ProgrammingLanguageDto";
import { OperationResult } from "../../dto/OperationResult";
import { DepartmentService } from "../../services/departmentService";
import {ProgrammingLanguageService} from "../../services/programingLanguageService";

@Component({
  selector: 'app-employee-form',
  templateUrl: './employee-form-component.html',
  styleUrls: ['./employee-form-component.css']
})
export class EmployeeFormComponent implements OnInit {
  employee: EmployeeDto = {
    id: 0,
    name: '',
    surname: '',
    age: 0,
    gender: 0,
    department: { id: 0, name: '', floor: 0 },
    programmingLanguage: { id: 0, name: '' }
  };

  departments: DepartmentDto[] = [];
  programmingLanguages: ProgrammingLanguageDto[] = [];
  isEditing: boolean = false;
  title: string = "";

  constructor(
    private employeeService: EmployeeService,
    private programmingLanguageService: ProgrammingLanguageService,
    private departmentService: DepartmentService,
    private router: Router,
    private route: ActivatedRoute) {}

  ngOnInit(): void {
    this.getDepartments();
    this.getProgrammingLanguages();

    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditing = true;
      this.getEmployeeById(+id);
      this.title = "Employee data changes";
    } else {
      this.title = "Adding an employee";
    }
  }

  getDepartments(): void {
    this.departmentService.getAll().subscribe((response: OperationResult<DepartmentDto[]>) => {
      if (response.status === 0) {
        this.departments = response.result;
      } else {
        console.error(response.errorMessage);
      }
    });
  }

  getProgrammingLanguages(): void {
    this.programmingLanguageService.getAll().subscribe((response: OperationResult<ProgrammingLanguageDto[]>) => {
      if (response.status === 0) {
        this.programmingLanguages = response.result;
      } else {
        console.error(response.errorMessage);
      }
    });
  }

  getEmployeeById(id: number): void {
    this.employeeService.getById(id).subscribe((response: OperationResult<EmployeeDto>) => {
      if (response.status === 0) {
        this.employee = response.result;
        if (this.employee.programmingLanguage.id == null) {
          this.employee.programmingLanguage.id = 0;
        }
        if (this.employee.department.id == null) {
          this.employee.department.id = 0;
        }
      } else {
        console.error(response.errorMessage);
      }
    });
  }

  onSubmit(): void {
    if (this.employee.name && this.employee.surname) {
      this.employee.gender = Number(this.employee.gender);
      if (this.employee.programmingLanguage.id == 0) {
        this.employee.programmingLanguage.id = null;
      }
      if (this.employee.department.id == 0) {
        this.employee.department.id = null;
      }
      if (this.isEditing) {
        this.employeeService.update(this.employee.id, this.employee).subscribe(() => {
          this.router.navigate(['/employee']);
        });
      } else {
        this.employeeService.create(this.employee).subscribe(() => {
          this.router.navigate(['/employee']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/employee']);
  }
}
