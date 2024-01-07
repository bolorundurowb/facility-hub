import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthService } from '../../services';

@Component({
  selector: 'fh-dashboard-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  userName: string | undefined;

  constructor(title: Title, private authService: AuthService) {
    title.setTitle('Dashboard | Facility Hub');
  }

  ngOnInit() {
    const {firstName, lastName} = this.authService.getUser();
    this.userName = `${firstName} ${lastName}`;
  }
}
