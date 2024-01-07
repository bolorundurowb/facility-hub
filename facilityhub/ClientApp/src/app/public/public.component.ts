import { Component, OnDestroy, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { AuthService } from '../services';

@Component({
  selector: 'fh-public',
  template: `
    <c-navbar colorScheme="light" expand="lg" class="bg-dark">
      <c-container fluid>
        <a cNavbarBrand routerLink="/">
          <img class="brand-logo" src="/assets/logo/white-transparent.svg" style="margin-top: -1rem; margin-bottom: -1rem;"/>
          <span class="align-middle ms-2 text-light">Facility Hub</span>
        </a>
        <c-navbar-nav class="d-flex">
          <ng-container *ngIf="!isLoggedIn">
            <c-nav-item>
              <button
                cButton
                color="light"
                (click)="goToLogin()">
                Log In
              </button>
            </c-nav-item>
            <c-nav-item>
              <button
                cButton
                color="primary"
                (click)="goToRegister()">
                Get Started
              </button>
            </c-nav-item>
          </ng-container>
          <ng-container *ngIf="isLoggedIn">
            <c-dropdown variant="nav-item" [popper]="false">
              <a cDropdownToggle cNavLink class="text-light">
                {{ user.firstName }} {{ user.lastName }}
              </a>
              <ul cDropdownMenu>
                <li>
                  <button
                    cDropdownItem
                    (click)="goToDashboard()">
                    Dashboard
                  </button>
                </li>
                <li>
                  <button
                    cDropdownItem
                    (click)="logOut()">
                    Log Out
                  </button>
                </li>
              </ul>
            </c-dropdown>
          </ng-container>
        </c-navbar-nav>
      </c-container>
    </c-navbar>

    <router-outlet></router-outlet>
  `,
})
export class PublicComponent {
  isLoggedIn = false;
  user: any;

  constructor(title: Title, private router: Router, private authService: AuthService) {
    title.setTitle('Home | Facility Hub');

    this.isLoggedIn = authService.isLoggedIn();
    this.user = authService.getUser();
  }

  async goToLogin() {
    await this.router.navigate([ 'auth', 'login' ]);
  }

  async goToRegister() {
    await this.router.navigate([ 'auth', 'register' ]);
  }

  async goToDashboard() {
    await this.router.navigate([ 'dashboard' ]);
  }

  logOut() {
    this.authService.logout();
    location.reload();
  }
}
