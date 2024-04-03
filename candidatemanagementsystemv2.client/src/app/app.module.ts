import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { CandidateFormComponent } from './components/candidate-form/candidate-form.component';
import { CandidatesListComponent } from './components/candidates-list/candidates-list.component';
import { DataReportComponent } from './components/data-report/data-report.component';
import { LoginComponent } from './components/login/login.component';
import { AuthInterceptor } from './interceptors/auth.interceptor';
import { PositionsDisplayPipe } from './pipes/positions-display.pipe';
import { SkillsDisplayPipe } from './pipes/skills-display.pipe';
import { SharedModule } from './shared/shared.module';

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
    SharedModule
  ],
  providers: [{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }
