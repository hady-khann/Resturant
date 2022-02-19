import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthComponent } from './Auth/Auth.component';
import { LoginComponent } from './Auth/Login/Login.component';
import { RegisterComponent } from './Auth/Register/Register.component';

const routes: Routes = [
{path:'',component:AuthComponent,pathMatch:'full',
// children:[
//   {path:'Login',component:LoginComponent,pathMatch:'full'},
//   {path:'Register',component:RegisterComponent,pathMatch:'full'},
//   {path:'',redirectTo:'/Login',pathMatch:'full'},
// ],


}];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule { }
