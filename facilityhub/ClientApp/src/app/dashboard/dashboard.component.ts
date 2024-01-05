import {Component} from "@angular/core";

@Component({
  selector: 'fh-dashboard',
  template: `
    <router-outlet></router-outlet>
  `,
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent {
}
