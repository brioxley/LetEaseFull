import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { PropertiesComponent } from './properties/properties.component';
import { HomeComponent } from './home/home.component';
import { RoomsComponent } from './rooms/rooms.component';
import { ContractsComponent } from './contracts/contracts.component';

// Import the AuthInterceptor
import { AuthInterceptor } from './interceptors/auth.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    LoginComponent,
    RegisterComponent,
    PropertiesComponent,
    HomeComponent,
    RoomsComponent,
    ContractsComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    RouterModule,
    HttpClientModule,
    AppRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }],
  bootstrap: [AppComponent]
})
export class AppModule { }

//import { BrowserModule } from '@angular/platform-browser';
//import { NgModule } from '@angular/core';
//import { FormsModule } from '@angular/forms';
//import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
//import { RouterModule } from '@angular/router';
//import { AppRoutingModule } from './app-routing.module';
//import { AppComponent } from './app.component';
//import { HomeComponent } from './home/home.component';
//import { PropertiesComponent } from './properties/properties.component';
//import { RoomsComponent } from './rooms/rooms.component';
//import { ContractsComponent } from './contracts/contracts.component';
//import { LoginComponent } from './login/login.component';
//import { RegisterComponent } from './register/register.component';
//import { NavbarComponent } from './navbar/navbar.component';
//import { AuthInterceptor } from './interceptors/auth.interceptor';
//import { DashboardComponent } from './dashboard/dashboard.component';


//@NgModule({
//  declarations: [
//    BrowserModule,
//    AppRoutingModule,
//    AppComponent,
//    HomeComponent,
//    PropertiesComponent,
//    RoomsComponent,
//    ContractsComponent,
//    LoginComponent,
//    RegisterComponent,
//    NavbarComponent,
//    DashboardComponent,
//    FormsModule
//  ],
//  imports: [
//    BrowserModule,
//    HttpClientModule,
//    FormsModule,
//    AppRoutingModule
//  ],
//  providers: [
//    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true }
//  ],
//  bootstrap: [AppComponent]
//})
//export class AppModule { }
