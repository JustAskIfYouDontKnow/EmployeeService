import { DepartmentDto } from "./DepartmentDto";
import { ProgrammingLanguageDto } from "./ProgrammingLanguageDto";

export interface EmployeeDto {
  id: number;
  name: string;
  surname: string;
  age: number;
  gender: number;
  department: DepartmentDto;
  programmingLanguage: ProgrammingLanguageDto;
}
