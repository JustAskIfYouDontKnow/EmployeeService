import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { GenericService } from './genericService';
import { ProgrammingLanguageDto } from '../dto/ProgrammingLanguageDto';

@Injectable({
  providedIn: 'root'
})
export class ProgrammingLanguageService extends GenericService<ProgrammingLanguageDto> {
  constructor(http: HttpClient) {
    super(http, 'api/ProgrammingLanguage');
  }
}
