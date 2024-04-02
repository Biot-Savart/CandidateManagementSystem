import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatLegacyButtonModule as MatButtonModule } from '@angular/material/legacy-button';
import { MatLegacyCardModule as MatCardModule } from '@angular/material/legacy-card';
import { MatLegacyFormFieldModule as MatFormFieldModule } from '@angular/material/legacy-form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatLegacyInputModule as MatInputModule } from '@angular/material/legacy-input';
import { MatLegacyListModule as MatListModule } from '@angular/material/legacy-list';
import { MatLegacySelectModule as MatSelectModule } from '@angular/material/legacy-select';
import { MatLegacyTableModule as MatTableModule } from '@angular/material/legacy-table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from '../app/guards/auth.guard';
import { AppComponent } from './app.component';
import { CandidateFormComponent } from './candidate-form/candidate-form.component';
import { CandidatesListComponent } from './candidates-list/candidates-list.component';
import { DataReportComponent } from './data-report/data-report.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { LoginComponent } from './login/login.component';
import { PositionsDisplayPipe } from './pipes/positions-display.pipe';
import { SkillsDisplayPipe } from './pipes/skills-display.pipe';
import { FlexLayoutModule } from '@angular/flex-layout';


// Define routes
const routes: Routes = [
  { path: '', redirectTo: '/login', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'candidates', component: CandidatesListComponent, canActivate: [AuthGuard] },
  { path: 'candidate-add', component: CandidateFormComponent, canActivate: [AuthGuard] },
  { path: 'candidate-edit/:id', component: CandidateFormComponent, canActivate: [AuthGuard] }, // Assuming :id is used for editing
  { path: 'data-report', component: DataReportComponent, canActivate: [AuthGuard] },
  { path: 'candidate-form', component: CandidateFormComponent, canActivate: [AuthGuard] }
];

@NgModule({
  declarations: [
    AppComponent,
    CandidatesListComponent,
    CandidateFormComponent,
    DataReportComponent,
    LoginComponent,
    SkillsDisplayPipe,
    PositionsDisplayPipe
  ],
  imports: [
    BrowserModule, HttpClientModule, BrowserAnimationsModule, RouterModule, RouterModule.forRoot(routes),
    MatTableModule, MatButtonModule, MatToolbarModule, MatIconModule, FormsModule, MatCardModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule,
    MatSelectModule, MatListModule, FlexLayoutModule 

  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
