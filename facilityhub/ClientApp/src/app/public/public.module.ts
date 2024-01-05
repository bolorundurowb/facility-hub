import {NgModule} from "@angular/core";
import { PublicRoutingModule } from "./public-routing.module";
import { HomeComponent } from "./home/home.component";

@NgModule({
  declarations: [
    HomeComponent
  ],
  imports: [
    PublicRoutingModule
  ]
})
export class PublicModule {}
