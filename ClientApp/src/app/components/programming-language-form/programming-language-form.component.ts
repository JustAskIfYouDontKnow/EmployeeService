import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { ProgrammingLanguageDto } from '../../dto/ProgrammingLanguageDto';
import { OperationResult } from '../../dto/OperationResult';
import {ProgrammingLanguageService} from "../../services/programingLanguageService";

@Component({
  selector: 'app-programming-language-form',
  templateUrl: './programming-language-form.component.html',
  styleUrls: ['./programming-language-form.component.css']
})
export class ProgrammingLanguageFormComponent implements OnInit {
  language: ProgrammingLanguageDto = {
    id: 0,
    name: ''
  };
  isEditing: boolean = false;
  title: string = "";

  constructor(
    private languageService: ProgrammingLanguageService,
    private router: Router,
    private route: ActivatedRoute) {}

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditing = true;
      this.getLanguageById(+id);
      this.title = "Programming Language data changes";
    } else {
      this.title = "Adding a programming language";
    }
  }

  getLanguageById(id: number): void {
    this.languageService.getById(id).subscribe((response: OperationResult<ProgrammingLanguageDto>) => {
      if (response.status === 0) {
        this.language = response.result;
      } else {
        console.error(response.errorMessage);
      }
    });
  }

  onSubmit(): void {
    if(this.language.name != '')
    {
      if (this.isEditing)
      {
        if(this.language.id)
          this.languageService.update(this.language.id, this.language).subscribe(() => {
            this.router.navigate(['/programming-language']);
          });
      } else {
        this.languageService.create(this.language).subscribe(() => {
          this.router.navigate(['/programming-language']);
        });
      }
    }
  }

  onCancel(): void {
    this.router.navigate(['/programming-language']);
  }
}
