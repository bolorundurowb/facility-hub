import { Component } from '@angular/core';
import { INavData } from '@coreui/angular';

@Component({
  selector: 'fh-dashboard',
  template: `
    <c-sidebar visible [narrow]="isCompact">
      <c-sidebar-brand routerLink="/">
        <img class="brand-logo" src="/assets/logo/white-transparent.svg" style="margin-left: -3rem;"/>
        <span class="align-middle ms-2">Facility Hub</span>
      </c-sidebar-brand>
      <c-sidebar-nav [navItems]="sidebarLinks" dropdownMode="close"></c-sidebar-nav>
      <c-sidebar-toggler (click)="toggleSidebar()"></c-sidebar-toggler>
    </c-sidebar>

    <div class="sidebar-content">
      <router-outlet></router-outlet>
    </div>
  `,
  styles: `
    .sidebar-content {
      margin-left: 16rem;
      padding: 2rem;
      height: 100vh;
    }
  `
})
export class DashboardComponent {
  sidebarLinks: INavData[] = [
    {
      name: 'Dashboard',
      url: '/dashboard/home',
      iconComponent: { name: 'cil-chart-line' }
    },
    {
      name: 'Facilities',
      url: '/dashboard/facilities',
      iconComponent: { name: 'cil-bank' }
    },
    {
      name: 'Issues',
      url: '/dashboard/issues',
      iconComponent: { name: 'cil-bell-exclamation' }
    },
    {
      name: 'Profile',
      url: '/dashboard/profile',
      iconComponent: { name: 'cil-user' }
    },
  ];

  isCompact = false;

  toggleSidebar() {
    this.isCompact = !this.isCompact;
  }
}
