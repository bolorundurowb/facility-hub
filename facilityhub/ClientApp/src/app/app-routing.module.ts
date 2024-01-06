import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { AuthComponent } from "./auth/auth.component";
import { DashboardComponent } from "./dashboard/dashboard.component";
import { PublicComponent } from "./public/public.component";

const routes: Routes = [
  {
    path: '',
    component: PublicComponent,
    loadChildren: () => import('./public/public.module').then(x => x.PublicModule)
  },
  {
    path: 'auth',
    component: AuthComponent,
    loadChildren: () => import('./auth/auth.module').then(x => x.AuthModule)
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    loadChildren: () => import('./dashboard/dashboard.module').then(x => x.DashboardModule)
  },
]

@NgModule({
  imports: [ RouterModule.forRoot(routes) ],
  exports: [ RouterModule ],
})
export class AppRoutingModule {
}
