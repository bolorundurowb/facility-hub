import {Component} from '@angular/core';
import {Router} from "@angular/router";
import {Title} from "@angular/platform-browser";

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
          <c-nav-item>
            <button
              cButton
              color="light"
              (click)="goToLogin()"
            >Log In
            </button>
          </c-nav-item>
          <c-nav-item>
            <button
              cButton
              color="primary"
              (click)="goToRegister()"
            >Get Started
            </button>
          </c-nav-item>
        </c-navbar-nav>
      </c-container>
    </c-navbar>
    <router-outlet></router-outlet>
  `
})
export class AppComponent {
  constructor(title: Title, private router: Router) {
    title.setTitle('Home | Facility Hub');
  }

  async goToLogin() {
    await this.router.navigate(['auth', 'login']);
  }

  async goToRegister() {
    await this.router.navigate(['auth', 'register']);
  }
}
