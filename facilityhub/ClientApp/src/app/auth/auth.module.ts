import {NgModule} from "@angular/core";
import {LoginComponent} from "./login/login.component";
import {RouterModule} from "@angular/router";

@NgModule({
  declarations: [
    LoginComponent
  ],
  imports: [
    RouterModule.forChild([
      {path: '*'},
      {path: 'login', component: LoginComponent, pathMatch: 'full'}
    ])
  ]
})
export class AuthModule {}
