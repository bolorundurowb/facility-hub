import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { AuthComponent } from './auth/auth.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IconModule, IconSetService } from '@coreui/icons-angular';
import { DashboardComponent } from "./dashboard/dashboard.component";
import { PublicComponent } from "./public/public.component";

@NgModule({
  declarations: [
    AppComponent,
    AuthComponent,
    DashboardComponent,
    PublicComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    IconModule,
  ],
  providers: [ IconSetService ],
  bootstrap: [ AppComponent ]
})
export class AppModule {
}
