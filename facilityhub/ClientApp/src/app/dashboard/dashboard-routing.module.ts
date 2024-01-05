import { RouterModule, Routes } from "@angular/router";
import { FacilitiesComponent } from "./facilities/facilities.component";
import { NgModule } from "@angular/core";

const routes: Routes = [
  { path: 'facilities', component: FacilitiesComponent, pathMatch: 'full' },
  { path: '**', redirectTo: 'facilities' }
]

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class DashboardRoutingModule {
}
