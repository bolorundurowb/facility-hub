import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { AuthService, NotificationService, UsersService } from '../../services';

interface UpdateUserPayload {
  firstName?: string;
  lastName?: string;
  emailAddress?: string;
  phoneNumber?: string;
}

interface UpdatePasswordPayload {
  currentPassword?: string;
  password?: string;
  confirmPassword?: string;
}

@Component({
  selector: 'fh-dashboard-profile',
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.scss'
})
export class ProfileComponent implements OnInit {
  passwordRegex = /^(?=.*[A-Z])(?=.*\d)(?=.*[^a-zA-Z\d\s]).{8,}$/;

  isLoading = true;
  user: any = {};

  hasError = false;
  errorMessage: string | undefined;

  isUpdateProfileModalVisible = false;
  isUpdatingProfile = false;
  updateProfilePayload: UpdateUserPayload = {};

  isUpdatingPassword = false;
  isUpdatePasswordModalVisible = false;
  updatePasswordPayload: UpdatePasswordPayload = {};

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
    };
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

  showUpdatePasswordModal() {
    this.isUpdatePasswordModalVisible = true;
  }

  dismissUpdatePasswordModal() {
    this.isUpdatePasswordModalVisible = false;
    this.updatePasswordPayload = {};
  }

  async updatePassword() {
    this.isUpdatingPassword = true;

    try {
      const hasError = this.validatePasswordPayload();

      if (!hasError) {
        await this.userService.updatePassword(this.updatePasswordPayload);
        this.notificationService.showSuccess('Password updated successfully');
        this.dismissUpdatePasswordModal();
      }
    } catch (e: any) {
      this.errorMessage = e;
      this.hasError = true;
    } finally {
      this.isUpdatingPassword = false;
    }
  }

  dismissError(): void {
    this.hasError = false;
    this.errorMessage = undefined;
  }

  private validatePasswordPayload(): boolean {
    let message = null;
    if (!this.updatePasswordPayload.currentPassword) {
      message = 'Your current password is required';
    } else if (!this.updatePasswordPayload.password) {
      message = 'A new password is required';
    } else if (!this.updatePasswordPayload.confirmPassword) {
      message = 'A password confirmation is required';
    } else if (!this.passwordRegex.test(this.updatePasswordPayload.password)) {
      message = 'A password must be at least 8 chars long with a capital letter, number and special char';
    } else if (this.updatePasswordPayload.password !== this.updatePasswordPayload.confirmPassword) {
      message = 'The new password and confirmation do not match';
    }

    if (message) {
      this.errorMessage = message;
      this.hasError = true;
    } else {
      this.dismissError();
    }

    return this.hasError;
  }
}
