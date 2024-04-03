import { Component, OnInit } from '@angular/core';
import { ISkillCandidatesCount } from '../models/skill';
import { SkillService } from '../services/skill/skill.service';

@Component({
  selector: 'app-data-report',
  templateUrl: './data-report.component.html',
  styleUrls: ['./data-report.component.css']
})
export class DataReportComponent implements OnInit {
  reportData: ISkillCandidatesCount[] = [];
  displayedColumns: string[] = ['skillName', 'candidateCount'];

  constructor(private skillService: SkillService) {}

  ngOnInit(): void {
    this.skillService.getSkillCandidatesCount().subscribe((data) => {
      this.reportData = data;
    });
  }

}
