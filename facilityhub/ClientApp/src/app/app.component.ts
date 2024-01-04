import { Component } from '@angular/core';

@Component({
  selector: 'fh-root',
  template: `
    <c-navbar colorScheme="light" expand="lg" class="bg-light">
      <c-container fluid>
        <a cNavbarBrand>
          <img src="./assets/img/brand/coreui-signet.svg" alt="" width="22" height="24" />
          <span class="align-middle ms-2">Facility Hub</span>
        </a>
          <c-navbar-nav class="d-flex">
            <c-nav-item>
              <button>Log In</button>
            </c-nav-item>
            <c-nav-item>
              <button>Get Started</button>
            </c-nav-item>
          </c-navbar-nav>
      </c-container>
    </c-navbar>
    <router-outlet></router-outlet>
  `
})
export class AppComponent {
}
