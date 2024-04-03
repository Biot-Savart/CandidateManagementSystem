// app-routing.module.ts
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './guards/auth.guard';
import { CandidateFormComponent } from './components/candidate-form/candidate-form.component';
import { CandidatesListComponent } from './components/candidates-list/candidates-list.component';
import { DataReportComponent } from './components/data-report/data-report.component';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  { path: '', redirectTo: '/candidates', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'candidates', component: CandidatesListComponent, canActivate: [AuthGuard] },
  { path: 'candidate-add', component: CandidateFormComponent, canActivate: [AuthGuard] },
  { path: 'candidate-edit/:id', component: CandidateFormComponent, canActivate: [AuthGuard] },
  { path: 'data-report', component: DataReportComponent, canActivate: [AuthGuard] },
  { path: 'candidate-form', component: CandidateFormComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
