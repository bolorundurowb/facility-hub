import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { IconModule, IconSetService } from '@coreui/icons-angular';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { SidebarModule, ToastModule } from '@coreui/angular';
import { AuthInterceptor, ErrorInterceptor } from './interceptors';
import { LeafletModule } from '@asymmetrik/ngx-leaflet';
import { NotificationsComponent, WidgetComponent } from './components';

@NgModule({
  declarations: [
    AppComponent,
    NotificationsComponent,
    WidgetComponent,
  ],
  imports: [
    AppRoutingModule,
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    BrowserAnimationsModule,
    HttpClientModule,
    IconModule,
    LeafletModule,
    SidebarModule,
    ToastModule,
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
    IconSetService,
  ],
  bootstrap: [ AppComponent ]
})
export class AppModule {
}
