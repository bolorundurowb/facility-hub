import {Component, OnDestroy, OnInit} from '@angular/core';
import {Router} from "@angular/router";
import {Title} from "@angular/platform-browser";
import {AuthService} from "./services";

@Component({
  selector: 'fh-root',
  template: `
    <c-navbar colorScheme="light" expand="lg" class="bg-light">
      <c-container fluid>
        <a cNavbarBrand routerLink="/">
          <img src="./assets/img/brand/coreui-signet.svg" alt="" width="22" height="24"/>
          <span class="align-middle ms-2">Facility Hub</span>
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
              <a cDropdownToggle cNavLink>
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
  `
})
export class AppComponent implements OnInit, OnDestroy {
  isLoggedIn = false;
  user: any;

  constructor(title: Title, private router: Router, private authService: AuthService) {
    title.setTitle('Home | Facility Hub');

    this.isLoggedIn = authService.isLoggedIn();
    this.user = authService.getUser();
  }

  ngOnInit() {
    this.authService.authenticated.subscribe(this.handleAuthChange);
  }

  async goToLogin() {
    await this.router.navigate(['auth', 'login']);
  }

  async goToRegister() {
    await this.router.navigate(['auth', 'register']);
  }

  async goToDashboard() {
    await this.router.navigate(['dashboard']);
  }

  async logOut() {
    this.authService.logout();
    await this.router.navigate(['/']);
  }

  handleAuthChange(event: boolean) {
    console.log('hello', event);
    this.isLoggedIn = event;
    this.user =  this.authService.getUser();
  }

  ngOnDestroy() {
    this.authService.authenticated.unsubscribe();
  }
}
