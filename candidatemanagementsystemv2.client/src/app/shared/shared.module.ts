import { HttpClientModule} from '@angular/common/http';
import { NgModule } from '@angular/core';
import { FlexLayoutModule } from '@angular/flex-layout';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatListModule } from '@angular/material/list';
import { MatMenuModule } from '@angular/material/menu';
import { MatSelectModule } from '@angular/material/select';
import { MatTableModule } from '@angular/material/table';
import { MatToolbarModule } from '@angular/material/toolbar';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgChartsModule } from 'ng2-charts';
import { AppRoutingModule } from '../app-routing.module';

const modules = [
  BrowserModule, HttpClientModule, BrowserAnimationsModule, AppRoutingModule,
  MatTableModule, MatButtonModule, MatToolbarModule, MatIconModule, FormsModule, MatCardModule, MatFormFieldModule, MatInputModule, ReactiveFormsModule,
  MatSelectModule, MatListModule, FlexLayoutModule, MatMenuModule, NgChartsModule
]

@NgModule({
  declarations: [],
  imports: [...modules],
  exports: [...modules]
})
export class SharedModule { }
