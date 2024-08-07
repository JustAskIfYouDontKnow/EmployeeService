import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { DepartmentService } from '../../services/departmentService';
import { DepartmentDto } from '../../dto/DepartmentDto';
import { OperationResult } from '../../dto/OperationResult';

@Component({
  selector: 'app-department-form',
  templateUrl: './department-form.component.html',
  styleUrls: ['./department-form.component.css']
})
export class DepartmentFormComponent implements OnInit {
  department: DepartmentDto = {
    id: 0,
    name: '',
    floor: 0
  };
  isEditing: boolean = false;
  title: string = "";

  constructor(
    private departmentService: DepartmentService,
    private router: Router,
    private route: ActivatedRoute) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditing = true;
      this.getDepartmentById(+id);
      this.title = "Department data changes";
    } else {
      this.title = "Adding a department";
    }
  }

  getDepartmentById(id: number): void {
    this.departmentService.getById(id).subscribe((response: OperationResult<DepartmentDto>) => {
      if (response.status === 0) {
        this.department = response.result;
      } else {
        console.error(response.errorMessage);
      }
    });
  }

  onSubmit(): void {
    if(this.department.name != '')
    {
      if (this.isEditing) {
        if (this.department.id)
          this.departmentService.update(this.department.id, this.department).subscribe(() => {
            this.router.navigate(['/department']);
          });
      } else {
        this.departmentService.create(this.department).subscribe(() => {
          this.router.navigate(['/department']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/department']);
  }
}
