import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthService, NotificationService, UsersService } from '../../services';

interface UpdateUserPayload {
  firstName?: string;
  lastName?: string;
  emailAddress?: string;
  phoneNumber?: string;
}

@Component({
  selector: 'fh-dashboard-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
  isLoading = true;
  user: any = {};

  isUpdateProfileModalVisible = false;
  isUpdatingProfile = false;
  updateProfilePayload: UpdateUserPayload = {};

  constructor(title: Title, private userService: UsersService, private authService: AuthService
    , private notificationService: NotificationService) {
    title.setTitle('Profile | Facility Hub');
  }

  async ngOnInit() {
    this.isLoading = false;
    this.user = await this.userService.getProfile();
    this.authService.updateUser(this.user);
  }

  showUpdateProfileModal() {
    this.updateProfilePayload = {
      firstName: this.user.firstName,
      lastName: this.user.lastName,
      emailAddress: this.user.emailAddress,
      phoneNumber: this.user.phoneNumber,
    }
    this.isUpdateProfileModalVisible = true;
  }

  dismissUpdateProfileModal() {
    this.isUpdateProfileModalVisible = false;
    this.updateProfilePayload = {};
  }

  async updateProfile() {
    this.isUpdatingProfile = true;
    this.user = await this.userService.updateProfile(this.updateProfilePayload);
    this.authService.updateUser(this.user);

    this.notificationService.showSuccess('Profile updated successfully');

    this.dismissUpdateProfileModal();
    this.isUpdatingProfile = false;
  }
}
