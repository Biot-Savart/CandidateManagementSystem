import { Component, OnInit } from '@angular/core';
import { ICandidate } from '../../models/candidate';
import { CandidateService } from '../../services/candidate/candidate.service';

@Component({
  selector: 'app-candidates-list',
  templateUrl: './candidates-list.component.html',
  styleUrls: ['./candidates-list.component.css']
})
export class CandidatesListComponent implements OnInit {

  candidates: ICandidate[] = [];
  displayedColumns: string[] = ['name', 'email', 'phone', 'experience', 'skills','positions', 'actions'];

  constructor(private candidateService: CandidateService) { }

  ngOnInit(): void {
    this.candidateService.getCandidates().subscribe((candidates) => {
      this.candidates = candidates;
    });
  }

  deleteCandidate(id: number): void {
    this.candidateService.deleteCandidate(id).subscribe(() => {
      this.candidates = this.candidates.filter(candidate => candidate.candidateId !== id);
    });
  }

}
