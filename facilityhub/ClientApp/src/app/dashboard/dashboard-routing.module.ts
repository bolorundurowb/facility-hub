import { RouterModule, Routes } from "@angular/router";
import { FacilitiesComponent } from "./facilities/facilities.component";
import { NgModule } from "@angular/core";
import { HomeComponent } from "./home/home.component";

const routes: Routes = [
  { path: 'facilities', component: FacilitiesComponent,  },
  { path: 'home', component: HomeComponent, pathMatch: 'full' },
  { path: '**', redirectTo: 'home' }
]

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class DashboardRoutingModule {
}
