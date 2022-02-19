import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule} from '@angular/router';


import { AuthRoutingModule } from './auth-routing.module';
import { AuthComponent } from './Auth/Auth.component';
import { LoginComponent } from './Auth/Login/Login.component';
import { RegisterComponent } from './Auth/Register/Register.component';


@NgModule({
  declarations: [
    AuthComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    RouterModule,
    CommonModule,
    BrowserModule,
    AuthRoutingModule,
  ]
})
export class AuthModule { }
