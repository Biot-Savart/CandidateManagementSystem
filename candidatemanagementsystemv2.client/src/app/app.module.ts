import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
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
import { MatMenuModule } from '@angular/material/menu'


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
    MatSelectModule, MatListModule, FlexLayoutModule, MatMenuModule

  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
