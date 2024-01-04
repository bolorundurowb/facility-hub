import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthComponent } from './auth/auth.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IconModule, IconSetService } from '@coreui/icons-angular';
import {
  ButtonDirective,
  ContainerComponent, DropdownComponent, DropdownItemDirective, DropdownMenuDirective, DropdownToggleDirective,
  NavbarBrandDirective,
  NavbarComponent,
  NavbarNavComponent,
  NavItemComponent, NavLinkDirective
} from "@coreui/angular";

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    HomeComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule.withServerTransition({appId: 'ng-cli-universal'}),
    BrowserAnimationsModule,
    HttpClientModule,
    IconModule,
    NavItemComponent,
    NavbarNavComponent,
    NavbarComponent,
    ContainerComponent,
    NavbarBrandDirective,
    NavLinkDirective,
    ButtonDirective,
    DropdownComponent,
    DropdownToggleDirective,
    DropdownMenuDirective,
    DropdownItemDirective,
  ],
  providers: [IconSetService],
  bootstrap: [AppComponent]
})
export class AppModule {
}
