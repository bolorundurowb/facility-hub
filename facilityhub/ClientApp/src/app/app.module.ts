import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IconModule, IconSetService } from '@coreui/icons-angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SidebarModule } from "@coreui/angular";

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    AppRoutingModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    IconModule,
    SidebarModule
  ],
  providers: [ IconSetService ],
  bootstrap: [ AppComponent ]
})
export class AppModule {
}
