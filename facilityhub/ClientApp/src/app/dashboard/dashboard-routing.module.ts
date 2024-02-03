import { RouterModule, Routes } from '@angular/router';
import { FacilitiesComponent } from './facilities/facilities.component';
import { NgModule } from '@angular/core';
import { HomeComponent } from './home/home.component';
import { FacilityDetailsComponent } from './facility-details/facility-details.component';
import { IssuesComponent } from './issues/issues.component';
import { ProfileComponent } from './profile/profile.component';

const routes: Routes = [
  {
    path: 'home',
    component: HomeComponent,
    pathMatch: 'full'
  },
  {
    path: 'facilities',
    component: FacilitiesComponent,
  },
  {
    path: 'facilities/:facilityId',
    component: FacilityDetailsComponent,
  },
  {
    path: 'issues',
    component: IssuesComponent,
  },
  {
    path: 'profile',
    component: ProfileComponent,
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
