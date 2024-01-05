import { NgModule } from "@angular/core";
import { PublicRoutingModule } from "./public-routing.module";
import { HomeComponent } from "./home/home.component";
import {
  ButtonDirective,
  ContainerComponent,
  DropdownComponent,
  DropdownItemDirective,
  DropdownMenuDirective,
  DropdownToggleDirective,
  NavbarComponent,
  NavbarNavComponent,
  NavItemComponent,
  NavLinkDirective
} from "@coreui/angular";
import { CommonModule } from "@angular/common";
import { PublicComponent } from "./public.component";

@NgModule({
  declarations: [
    HomeComponent,
    PublicComponent,
  ],
  imports: [
    CommonModule,
    PublicRoutingModule,
    NavbarComponent,
    NavbarNavComponent,
    ButtonDirective,
    NavItemComponent,
    DropdownComponent,
    DropdownMenuDirective,
    DropdownItemDirective,
    ContainerComponent,
    DropdownToggleDirective,
    NavLinkDirective,
  ]
})
export class PublicModule {
}
