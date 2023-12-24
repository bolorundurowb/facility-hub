import {Component} from "@angular/core";

@Component({
  selector: 'fh-auth',
  template: `
    <div class="container">
      <div class="box">
          <router-outlet></router-outlet>
      </div>
    </div>
  `,
  styles: `
    .container {
      display: flex;
      justify-content: center;
      align-items: center;
      height: 100vh;
    }

    .box {
      background-color: #dedede;
      border-radius: 0.5rem;
      padding: 1rem 1.5rem;
      max-width: 400px;
      width: 100%;
      border: 2px solid #cacaca;
    }
  `
})
export class AuthComponent {}
