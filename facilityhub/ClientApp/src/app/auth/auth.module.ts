import {NgModule} from "@angular/core";
import {LoginComponent} from "./login/login.component";
import {AuthRoutingModule} from "./auth-routing.module";
import {NglButtonsModule, NglInputModule} from "ng-lightning";
import {RegisterComponent} from "./register/register.component";
import { FormsModule } from "@angular/forms";

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    AuthRoutingModule,
    NglInputModule,
    NglButtonsModule,
    FormsModule
  ]
})
export class AuthModule {}
