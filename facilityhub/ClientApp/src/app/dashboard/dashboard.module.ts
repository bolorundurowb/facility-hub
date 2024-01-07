import { NgModule } from '@angular/core';
import { FacilitiesComponent } from './facilities/facilities.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import {
  ButtonDirective, CalloutComponent,
  CardBodyComponent,
  CardComponent, CardHeaderComponent, CardImgDirective, CardTextDirective, CardTitleDirective,
  ColComponent, ColDirective,
  ContainerComponent, GutterDirective,
  HeaderComponent,
  NavItemComponent, RowComponent,
  SidebarBrandComponent,
  SidebarComponent,
  SidebarNavComponent,
  SidebarTogglerComponent
} from '@coreui/angular';
import { CommonModule } from '@angular/common';
import { IconComponent, IconDirective } from '@coreui/icons-angular';
import { HomeComponent } from './home/home.component';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';

@NgModule({
  declarations: [
    DashboardComponent,
    FacilitiesComponent,
    HomeComponent,
  ],
  imports: [
    CommonModule,
    LeafletModule,
    DashboardRoutingModule,
    SidebarComponent,
    SidebarBrandComponent,
    SidebarNavComponent,
    SidebarTogglerComponent,
    NavItemComponent,
    IconComponent,
    IconDirective,
    HeaderComponent,
    ContainerComponent,
    RowComponent,
    ColComponent,
    CardComponent,
    CardBodyComponent,
    CardHeaderComponent,
    CardTextDirective,
    ButtonDirective,
    CardImgDirective,
    GutterDirective,
    CardTitleDirective,
    ColDirective,
    CalloutComponent,
  ],
})
export class DashboardModule {
}
