import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthService, StatisticsService } from '../../services';

interface StatsRes {
  facilitiesRented?: number;
  facilitiesOwned?: number;
  facilitiesManaged?: number;
  issuesFiled?: number;
  issuesManaged?: number;
  issuesResolved?: number;
}

@Component({
  selector: 'fh-dashboard-home',
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent implements OnInit {
  isLoading = true;
  userName: string | undefined;
  stats: StatsRes = {};

  constructor(title: Title, private authService: AuthService, private statService: StatisticsService) {
    title.setTitle('Dashboard | Facility Hub');
  }

  async ngOnInit() {
    const { firstName, lastName } = this.authService.getUser();
    this.userName = `${firstName} ${lastName}`;

    this.stats = await this.statService.get();
    this.isLoading = false;
  }
}
