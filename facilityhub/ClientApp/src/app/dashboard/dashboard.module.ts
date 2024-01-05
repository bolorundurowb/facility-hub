import {NgModule} from "@angular/core";
import { FacilitiesComponent } from "./facilities/facilities.component";
import { DashboardRoutingModule } from "./dashboard-routing.module";

@NgModule({
  declarations: [
    FacilitiesComponent
  ],
  imports: [
    DashboardRoutingModule
  ]
})
export class DashboardModule{}
