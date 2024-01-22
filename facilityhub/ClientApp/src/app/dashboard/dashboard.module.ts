import { NgModule } from '@angular/core';
import { FacilitiesComponent } from './facilities/facilities.component';
import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import {
  AlertComponent,
  BadgeComponent,
  ButtonCloseDirective,
  ButtonDirective,
  ButtonGroupComponent,
  CalloutComponent,
  CardBodyComponent,
  CardComponent,
  CardHeaderComponent,
  CardImgDirective,
  CardTextDirective,
  CardTitleDirective,
  ColComponent,
  ColDirective,
  ContainerComponent,
  FormControlDirective,
  FormDirective,
  FormLabelDirective,
  FormSelectDirective,
  FormTextDirective,
  GutterDirective,
  HeaderComponent, InputGroupComponent,
  ModalBodyComponent,
  ModalComponent,
  ModalFooterComponent,
  ModalHeaderComponent,
  ModalTitleDirective,
  NavItemComponent,
  PopoverDirective,
  RowComponent,
  SidebarBrandComponent,
  SidebarComponent,
  SidebarNavComponent,
  SidebarTogglerComponent,
  SpinnerComponent,
  TableDirective,
} from '@coreui/angular';
import { CommonModule } from '@angular/common';
import { IconComponent, IconDirective } from '@coreui/icons-angular';
import { HomeComponent } from './home/home.component';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { FormsModule } from '@angular/forms';
import { FacilityDetailsComponent } from './facility-details/facility-details.component';
import { NgxFilesizeModule } from 'ngx-filesize';
import { TruncateModule } from '@yellowspot/ng-truncate';
import { IssuesComponent } from './issues/issues.component';

@NgModule({
  declarations: [
    DashboardComponent,
    FacilitiesComponent,
    FacilityDetailsComponent,
    HomeComponent,
    IssuesComponent
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
    TableDirective,
    BadgeComponent,
    PopoverDirective,
    ButtonGroupComponent,
    NgxFilesizeModule,
    TruncateModule,
    FormSelectDirective,
    InputGroupComponent,
  ],
})
export class DashboardModule {
}
