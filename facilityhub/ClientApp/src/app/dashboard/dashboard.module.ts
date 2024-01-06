import {NgModule} from "@angular/core";
import { FacilitiesComponent } from "./facilities/facilities.component";
import { DashboardRoutingModule } from "./dashboard-routing.module";
import { DashboardComponent } from "./dashboard.component";
import {
  NavItemComponent,
  SidebarBrandComponent,
  SidebarComponent,
  SidebarNavComponent,
  SidebarTogglerComponent
} from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { IconComponent, IconDirective } from "@coreui/icons-angular";
import { HomeComponent } from "./home/home.component";

@NgModule({
  declarations: [
    DashboardComponent,
    FacilitiesComponent,
    HomeComponent,
  ],
  imports: [
    CommonModule,
    DashboardRoutingModule,
    SidebarComponent,
    SidebarBrandComponent,
    SidebarNavComponent,
    SidebarTogglerComponent,
    NavItemComponent,
    IconComponent,
    IconDirective,
  ],
})
export class DashboardModule{}
