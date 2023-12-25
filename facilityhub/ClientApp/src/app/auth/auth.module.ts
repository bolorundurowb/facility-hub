import {NgModule} from "@angular/core";
import {LoginComponent} from "./login/login.component";
import {AuthRoutingModule} from "./auth-routing.module";
import {NglButtonsModule, NglInputModule} from "ng-lightning";
import {RegisterComponent} from "./register/register.component";

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    AuthRoutingModule,
    NglInputModule,
    NglButtonsModule
  ]
})
export class AuthModule {}
