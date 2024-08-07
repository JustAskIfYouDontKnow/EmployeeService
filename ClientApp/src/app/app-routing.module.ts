import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {EmployeeListComponent} from "./components/employee-list/employee-list-component";
import {EmployeeFormComponent} from "./components/employee-form/employee-form-component";
import {DepartmentFormComponent} from "./components/department-form/department-form.component";
import {DepartmentListComponent} from "./components/department-list/department-list.component";
import {
  ProgrammingLanguageListComponent
} from "./components/programming-language-list/programming-language-list.component";
import {
  ProgrammingLanguageFormComponent
} from "./components/programming-language-form/programming-language-form.component";

const routes: Routes = [
  { path: '', redirectTo: 'employee', pathMatch: 'full' },
  { path: 'employee', component: EmployeeListComponent },
  { path: 'employee/add', component: EmployeeFormComponent },
  { path: 'employee/edit/:id', component: EmployeeFormComponent },
  { path: 'department', component: DepartmentListComponent },
  { path: 'department/add', component: DepartmentFormComponent },
  { path: 'department/edit/:id', component: DepartmentFormComponent },
  { path: 'programming-language', component: ProgrammingLanguageListComponent },
  { path: 'programming-language/add', component: ProgrammingLanguageFormComponent },
  { path: 'programming-language/edit/:id', component: ProgrammingLanguageFormComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
