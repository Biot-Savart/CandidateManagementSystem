import { Component, OnInit } from '@angular/core';
import { ChartConfiguration, ChartData, ChartType } from 'chart.js';
import { ISkillCandidatesCount } from '../../models/skill';
import { SkillService } from '../../services/skill/skill.service';

@Component({
  selector: 'app-data-report',
  templateUrl: './data-report.component.html',
  styleUrls: ['./data-report.component.css']
})
export class DataReportComponent implements OnInit {
  reportData: ISkillCandidatesCount[] = [];
  displayedColumns: string[] = ['skillName', 'candidateCount'];

  // Pie chart configuration
  public pieChartOptions: ChartConfiguration['options'] = {
    responsive: true,
    // Additional options
  };
  public pieChartType: ChartType = 'pie';
  public pieChartLabels: string[] = []; // e.g., Skill names
  // Adjust this to match the expected ChartData type
  public pieChartData: ChartData<'pie'> = {
    labels: this.pieChartLabels,
    datasets: [{
      data: []
    }]
  };

  constructor(private skillService: SkillService) {}

  ngOnInit(): void {
    this.skillService.getSkillCandidatesCount().subscribe((data) => {
      this.reportData = data;

      // Update labels
      this.pieChartLabels = data.map(item => item.name);

      // Update datasets
      this.pieChartData = {
        labels: this.pieChartLabels, // Ensure labels are updated if they're dynamic
        datasets: [{
          data: data.map(item => item.candidateCount) // Set data points for each label
        }]
      };
    });
  }

}
