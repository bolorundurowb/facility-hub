import {AfterViewInit, Component, HostListener, ViewChild} from '@angular/core';
import {INavData, SidebarComponent} from '@coreui/angular';

@Component({
  selector: 'fh-dashboard',
  template: `
    <c-navbar colorScheme="dark" expand="lg" class="bg-dark text-light mobile-nav">
      <c-container fluid>
        <c-navbar-brand routerLink="/">
          <img class="brand-logo
          " src="/assets/logo/white-transparent.svg"/>
          <span class="align-middle ms-2">Facility Hub</span>
        </c-navbar-brand>
        <button [cNavbarToggler]="collapseRef"></button>
        <div #collapseRef="cCollapse" navbar cCollapse>
          <c-navbar-nav class="me-auto mb-2 mb-lg-0">
            <c-nav-item *ngFor="let r of sidebarLinks" class="nav-item">
              <a cNavLink active [routerLink]="r.url" routerLinkActive="active">
                <svg cIcon [name]="r.iconComponent.name" size="lg" [title]="r.name"></svg>
                <span class="mx-2">{{r.name}}</span>
              </a>
            </c-nav-item>
          </c-navbar-nav>
        </div>
      </c-container>
    </c-navbar>
    <c-sidebar #sidebar visible id="sidebar">
      <c-sidebar-brand routerLink="/">
        <img class="brand-logo" src="/assets/logo/white-transparent.svg" style="margin-left: -3rem;"/>
        <span class="align-middle ms-2">Facility Hub</span>
      </c-sidebar-brand>
      <c-sidebar-nav [navItems]="sidebarLinks" dropdownMode="close"></c-sidebar-nav>
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

    .mobile-nav {
      .nav-item {
        margin-bottom: 0.5rem;

        a {
          text-decoration: none;
          color: white;

          &.active {
            filter: brightness(70%);
          }
        }
      }
    }

    @media only screen and (max-width: 991px) {
      .sidebar-content {
        margin-left: 0;
        padding: 2rem 1rem !important;
      }
    }

    @media only screen and (min-width: 992px) {
      .mobile-nav {
        display: none;
      }
    }
  `
})
export class DashboardComponent implements AfterViewInit {
  sidebarLinks: INavData[] = [
    {
      name: 'Dashboard',
      url: '/dashboard/home',
      iconComponent: {name: 'cil-chart-line'}
    },
    {
      name: 'Facilities',
      url: '/dashboard/facilities',
      iconComponent: {name: 'cil-bank'}
    },
    {
      name: 'Issues',
      url: '/dashboard/issues',
      iconComponent: {name: 'cil-bell-exclamation'}
    },
    {
      name: 'Profile',
      url: '/dashboard/profile',
      iconComponent: {name: 'cil-user'}
    },
  ];
  @ViewChild('sidebar') sidebar!: SidebarComponent;

  ngAfterViewInit() {
    if (window.innerWidth < 992) {
      this.sidebar.sidebarState.visible = false;
    } else {
      this.sidebar.sidebarState.visible = true;
    }
  }

  @HostListener('window:resize', ['$event'])
  onResize(event: any) {
    if (window.innerWidth < 992) {
      this.sidebar.sidebarState.visible = false;
    } else {
      this.sidebar.sidebarState.visible = true;
    }
  }
}
