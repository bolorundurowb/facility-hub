import { Component } from '@angular/core';
import { INavData } from '@coreui/angular';

@Component({
  selector: 'fh-dashboard',
  template: `
    <c-sidebar visible>
      <c-sidebar-brand routerLink="/">
        <img class="brand-logo" src="/assets/logo/white-transparent.svg" style="margin-left: -3rem;"/>
        <span class="align-middle ms-2">Facility Hub</span>
      </c-sidebar-brand>
      <c-sidebar-nav [navItems]="sidebarLinks" dropdownMode="close"></c-sidebar-nav>
      <c-sidebar-toggler></c-sidebar-toggler>
    </c-sidebar>

    <div class="sidebar-content">
      <router-outlet></router-outlet>
    </div>
  `,
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
  sidebarLinks: INavData[] = [
    {
      name: 'Home',
      url: [ 'home' ]
    },
    {
      name: 'Facilities',
      url: [ 'facilities' ]
    }
  ];
}
