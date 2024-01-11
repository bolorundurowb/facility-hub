import { RouterModule, Routes } from '@angular/router';
import { FacilitiesComponent } from './facilities/facilities.component';
import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { FacilityDetailsComponent } from './facility-details/facility-details.component';

const routes: Routes = [
  {
    path: 'facilities',
    component: FacilitiesComponent,
  },
  {
    path: 'facilities/:facilitiesId',
    component: FacilityDetailsComponent,
  },
  {
    path: 'home',
    component: HomeComponent,
    pathMatch: 'full'
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];

@NgModule({
  imports: [ RouterModule.forChild(routes) ],
  exports: [ RouterModule ]
})
export class DashboardRoutingModule {
}
