import {NgModule} from '@angular/core';
import {BrowserModule} from '@angular/platform-browser';
import {AppRoutingModule} from './app-routing.module';
import {AppComponent} from './app.component';
import {HttpClientModule} from "@angular/common/http";
import {EmployeeService} from "./services/employeeService";
import {FormsModule} from "@angular/forms";
import {DepartmentService} from "./services/departmentService";
import {ProgrammingLanguageService} from "./services/programingLanguageService";
import { DepartmentListComponent } from './components/department-list/department-list.component';
import { DepartmentFormComponent } from './components/department-form/department-form.component';
import {EmployeeFormComponent} from "./components/employee-form/employee-form-component";
import {EmployeeListComponent} from "./components/employee-list/employee-list-component";
import { ProgrammingLanguageFormComponent } from './components/programming-language-form/programming-language-form.component';
import { ProgrammingLanguageListComponent } from './components/programming-language-list/programming-language-list.component';

@NgModule({
  declarations: [
    AppComponent,
    EmployeeFormComponent,
    EmployeeListComponent,
    DepartmentListComponent,
    DepartmentFormComponent,
    ProgrammingLanguageFormComponent,
    ProgrammingLanguageListComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
  ],
  providers: [EmployeeService, DepartmentService, ProgrammingLanguageService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
