import {NgModule} from "@angular/core";
import { FacilitiesComponent } from "./facilities/facilities.component";
import { DashboardRoutingModule } from "./dashboard-routing.module";
import { DashboardComponent } from "./dashboard.component";

@NgModule({
  declarations: [
    FacilitiesComponent,
    DashboardComponent
  ],
  imports: [
    DashboardRoutingModule
  ]
})
export class DashboardModule{}
