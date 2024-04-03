import { Component, OnInit } from '@angular/core';
import { UntypedFormArray, UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ICandidatePosition, IPosition } from '../../models/position';
import { ISkill } from '../../models/skill';
import { CandidateService } from '../../services/candidate/candidate.service';
import { PositionService } from '../../services/position/position.service';

@Component({
  selector: 'app-candidate-form',
  templateUrl: './candidate-form.component.html',
  styleUrls: ['./candidate-form.component.css'],
})
export class CandidateFormComponent implements OnInit {
  candidateForm: UntypedFormGroup = this.formBuilder.group({
    name: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phone: ['', [Validators.required, Validators.pattern(/^\+?\d{10,15}$/)]], // Example regex for phone numbers
    experience: [0, [Validators.required, Validators.min(0)]],
    skills: this.formBuilder.array([]), // Start with an empty array for skills
    positions: [[]]
  });
  candidateId: number | null = null;
  positions: IPosition[] = []; // To store fetched positions
  selectedPositions: number[] = []; // To store selected position IDs

  constructor(
    private formBuilder: UntypedFormBuilder,
    private candidateService: CandidateService,
    private positionService: PositionService,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.candidateId = Number(params.get('id'));
      if (this.candidateId) {
        this.loadCandidate(this.candidateId);
      }
    });

    // Fetch positions
    this.positionService.getPositions().subscribe((positions) => {
      this.positions = positions;
      // Initialize the form array for positions
      this.positions.forEach(() => this.positionsFormArray.value.push(new UntypedFormControl(false)));
    });
  }

  get positionsFormArray(): UntypedFormArray {
    return this.candidateForm.get('positions') as UntypedFormArray;
  }

  // Getter for easy access to the skills FormArray
  get skills(): UntypedFormArray {
    return this.candidateForm.get('skills') as UntypedFormArray;
  }

  // Method to add a new skill FormControl to the FormArray
  addSkill(skill?: ISkill): void {
    const skillGroup = this.formBuilder.group({
      name: [skill?.name || '', Validators.required],
      yearsOfExperience: [skill?.yearsOfExperience || 1, Validators.required],
      skillId: [skill?.skillId || 0]
      // Include other skill properties as needed
    });
    this.skills.push(skillGroup);
  }

  // Method to remove a skill FormControl from the FormArray
  removeSkill(index: number): void {
    this.skills.removeAt(index);
  }

  onSubmit(): void {
    if (this.candidateForm.invalid) {
      return; // Form is invalid, do not submit
    }

    

    if (this.candidateId) {
      //update
      const skills: ISkill[] = this.candidateForm.value.skills.map((s: ISkill) => {
        return {
          skillId: s.skillId,
          name: s.name,
          yearsOfExperience: s.yearsOfExperience,
          candidateId: this.candidateId,
        }
      });

      const candidatePositions: ICandidatePosition[] = this.candidateForm.value.positions.map((positionId: number) => {
        return {
          positionId,
          candidateId: this.candidateId,
        }
      });

      this.candidateService.updateCandidate({
        candidateId: this.candidateId,
        name: this.candidateForm.value.name,
        email: this.candidateForm.value.email,
        phone: this.candidateForm.value.phone,
        experience: this.candidateForm.value.experience,
        skills,
        candidatePositions
      }).subscribe({
        next: (candidate) => {
          console.log('Candidate updated', candidate);
          this.router.navigate(['/candidates']);
        },
        error: (error) => {
          console.error('There was an error!', error);
          // Handle error, e.g., show an error message
        }
      });
    }
    else {
      //create
      const skills: ISkill[] = this.candidateForm.value.skills.map((s: ISkill) => {
        return {
          name: s.name,
          yearsOfExperience: 1
        }
      });

      const candidatePositions: ICandidatePosition[] = this.candidateForm.value.positions.map((positionId: number) => {
        return {
          positionId,
          candidateId: 0,
        }
      });

      this.candidateService.addCandidate({
        name: this.candidateForm.value.name,
        email: this.candidateForm.value.email,
        phone: this.candidateForm.value.phone,
        experience: this.candidateForm.value.experience,
        skills,
        candidatePositions
      }).subscribe({
        next: (candidate) => {
          console.log('Candidate added', candidate);
          this.router.navigate(['/candidates']);
        },
        error: (error) => {
          console.error('There was an error!', error);
          // Handle error, e.g., show an error message
        }
      });
    }
    
  }

  loadCandidate(candidateId: number): void {
    this.candidateService.getCandidateById(candidateId).subscribe(candidate => {
      this.candidateForm.patchValue(candidate);
      // Clear existing skills FormArray
      while (this.skills.length !== 0) {
        this.skills.removeAt(0);
      }

      candidate.skills?.forEach(skill => {
        this.addSkill(skill);
      });

      this.candidateForm.get('positions')?.setValue(candidate.candidatePositions?.map(p => p.positionId));
    });
  }
}
