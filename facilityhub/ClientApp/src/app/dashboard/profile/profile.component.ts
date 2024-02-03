import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthService, UsersService } from '../../services';

@Component({
  selector: 'fh-dashboard-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
  isLoading = true;

  user: any;

  constructor(title: Title, private userService: UsersService, private authService: AuthService) {
    title.setTitle('Profile | Facility Hub');
  }

  async ngOnInit() {
    this.isLoading = false;
    this.user = await this.userService.getProfile();
    this.authService.updateUser(this.user);
  }
}
