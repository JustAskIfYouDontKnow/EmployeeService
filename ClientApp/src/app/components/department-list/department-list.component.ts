import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { DepartmentService } from '../../services/departmentService';
import { OperationResult } from '../../dto/OperationResult';
import { DepartmentDto } from '../../dto/DepartmentDto';

@Component({
  selector: 'app-department-list',
  templateUrl: './department-list.component.html',
  styleUrls: ['./department-list.component.css']
})
export class DepartmentListComponent implements OnInit {
  departments: DepartmentDto[] = [];

  constructor(private departmentService: DepartmentService, private router: Router) { }

  ngOnInit(): void {
    this.getDepartments();
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

  onEdit(id?: number | null): void {
    if(id)
    this.router.navigate(['/department/edit', id]);
  }

  onDelete(id?: number | null): void {
    if(id)
    this.departmentService.delete(id).subscribe(() => {
      this.getDepartments();
    });
  }
}
