import { Component } from "@angular/core";
import { Title } from "@angular/platform-browser";

@Component({
  selector: 'fh-auth-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent {
  constructor(title: Title) {
    title.setTitle('Register | Facility Hub');
  }
}
