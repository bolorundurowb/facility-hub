import {NgModule} from "@angular/core";
import {LoginComponent} from "./login/login.component";
import {AuthRoutingModule} from "./auth-routing.module";
import {RegisterComponent} from "./register/register.component";
import { FormsModule } from "@angular/forms";
import {
  AlertComponent,
  ButtonDirective,
  FormControlDirective,
  FormDirective,
  FormLabelDirective,
  FormTextDirective
} from '@coreui/angular';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    AuthRoutingModule,
    ButtonDirective,
    FormsModule,
    FormControlDirective,
    FormLabelDirective,
    FormTextDirective,
    FormDirective,
    AlertComponent
  ]
})
export class AuthModule {}
