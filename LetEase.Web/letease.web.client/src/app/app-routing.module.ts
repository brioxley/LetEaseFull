

import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
import { PropertiesComponent } from './properties/properties.component';
import { RoomsComponent } from './rooms/rooms.component';
import { ContractsComponent } from './contracts/contracts.component';
import { AuthGuard } from './guards/auth.guard';
import { EmailVerificationComponent } from './email-verification/email-verification.component';
import { CommonModule } from '@angular/common';
import { BrowserModule } from '@angular/platform-browser';



const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },
  { path: 'properties', component: PropertiesComponent, canActivate: [AuthGuard] },
  { path: 'rooms', component: RoomsComponent, canActivate: [AuthGuard] },
  { path: 'contracts', component: ContractsComponent, canActivate: [AuthGuard] },
  { path: 'verify-email', component: EmailVerificationComponent },
  { path: '**', redirectTo: '' }  // This will redirect any unknown paths to the home page
];

@NgModule(
  {
    imports: [RouterModule.forRoot(routes), BrowserModule,
      CommonModule],
  exports: [RouterModule]
})
export class AppRoutingModule { }
