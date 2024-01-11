import { NgModule } from '@angular/core';
import { FacilitiesComponent } from './facilities/facilities.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import {
  AlertComponent,
  ButtonCloseDirective,
  ButtonDirective, CalloutComponent,
  CardBodyComponent,
  CardComponent, CardHeaderComponent, CardImgDirective, CardTextDirective, CardTitleDirective,
  ColComponent, ColDirective,
  ContainerComponent, FormControlDirective, FormDirective, FormLabelDirective, FormTextDirective, GutterDirective,
  HeaderComponent, ModalBodyComponent, ModalComponent, ModalFooterComponent, ModalHeaderComponent, ModalTitleDirective,
  NavItemComponent, RowComponent,
  SidebarBrandComponent,
  SidebarComponent,
  SidebarNavComponent,
  SidebarTogglerComponent, SpinnerComponent
} from '@coreui/angular';
import { CommonModule } from '@angular/common';
import { IconComponent, IconDirective } from '@coreui/icons-angular';
import { HomeComponent } from './home/home.component';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { FormsModule } from '@angular/forms';
import { FacilityDetailsComponent } from './facility-details/facility-details.component';

@NgModule({
  declarations: [
    DashboardComponent,
    FacilitiesComponent,
    FacilityDetailsComponent,
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
    ModalComponent,
    ModalHeaderComponent,
    ButtonCloseDirective,
    ModalTitleDirective,
    ModalFooterComponent,
    ModalBodyComponent,
    FormDirective,
    FormControlDirective,
    FormLabelDirective,
    FormsModule,
    FormTextDirective,
    AlertComponent,
    SpinnerComponent,
  ],
})
export class DashboardModule {
}
