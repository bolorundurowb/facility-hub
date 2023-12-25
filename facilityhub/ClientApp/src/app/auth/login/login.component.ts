import { Component } from "@angular/core";
import { Title } from "@angular/platform-browser";

@Component({
  selector: 'fh-auth-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {
  constructor(title: Title) {
    title.setTitle('Sign In | Facility Hub');
  }
}
