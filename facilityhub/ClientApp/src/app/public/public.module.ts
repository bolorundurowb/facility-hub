import { NgModule } from "@angular/core";
import { PublicRoutingModule } from "./public-routing.module";
import { HomeComponent } from "./home/home.component";
import {
  ButtonDirective, ContainerComponent,
  DropdownComponent, DropdownItemDirective, DropdownMenuDirective,
  NavbarComponent,
  NavbarNavComponent,
  NavItemComponent
} from "@coreui/angular";

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    PublicRoutingModule,
    NavbarComponent,
    NavbarNavComponent,
    ButtonDirective,
    NavItemComponent,
    DropdownComponent,
    DropdownMenuDirective,
    DropdownItemDirective,
    ContainerComponent,
  ]
})
export class PublicModule {
}
