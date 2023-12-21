import {RouterModule, Routes} from "@angular/router";
import {NgModule} from "@angular/core";
import {LoginComponent} from "./login/login.component";

const routes: Routes = [
  {path: 'login', component: LoginComponent, pathMatch: 'full'},
  {path: '**', redirectTo: 'login'}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AuthRoutingModule {}
