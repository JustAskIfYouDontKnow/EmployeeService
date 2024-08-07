import {Component, OnInit} from '@angular/core';
import {Router} from '@angular/router';
import {OperationResult} from '../../dto/OperationResult';
import {ProgrammingLanguageDto} from '../../dto/ProgrammingLanguageDto';
import {ProgrammingLanguageService} from "../../services/programingLanguageService";

@Component({
  selector: 'app-programming-language-list',
  templateUrl: './programming-language-list.component.html',
  styleUrls: ['./programming-language-list.component.css']
})
export class ProgrammingLanguageListComponent implements OnInit {
  languages: ProgrammingLanguageDto[] = [];

  constructor(private languageService: ProgrammingLanguageService, private router: Router) {
  }

  ngOnInit(): void {
    this.getLanguages();
  }

  getLanguages(): void {
    this.languageService.getAll().subscribe((response: OperationResult<ProgrammingLanguageDto[]>) => {
      if (response.status === 0) {
        this.languages = response.result;
      } else {
        console.error(response.errorMessage);
      }
    });
  }

  onEdit(id?: number | null): void {
    if (id)
      this.router.navigate(['/programming-language/edit', id]);
  }

  onDelete(id: number | null): void {
    if (id)
      this.languageService.delete(id).subscribe(() => {
        this.getLanguages();
      });
  }
}
