import { Component } from "@angular/core";

@Component({
  selector: 'fh-auth',
  template: `
    <div class="container">
      <div class="box">
        <div class="logo">
          <img src="/assets/logo/main-transparent.svg" />
        </div>
        <router-outlet></router-outlet>
      </div>
    </div>
  `,
  styleUrl: './auth.component.scss'
})
export class AuthComponent {
}
