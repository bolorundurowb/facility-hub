import { Component, OnDestroy, OnInit } from '@angular/core';
import { cilBell } from '@coreui/icons';
import { Colors } from '@coreui/angular';
import { NotificationService } from '../services';

export enum ToastType {
  SUCCESS,
  FAILURE
}

export interface NotificationData {
  message: string;
  title?: string;
  type?: ToastType;
}

@Component({
  selector: 'fh-notifications',
  template: `
    <c-toaster placement="top-end" position="absolute" style="margin-right: 0.5rem; margin-top: 0.5rem;">
      <ng-container *ngFor="let toast of toasts">
        <c-toast [autohide]="true" [fade]="true" [visible]="true" [color]="getColorForType(toast.type)">
          <c-toast-header>
            <svg [cIcon]="icons.cilBell"></svg>
            &nbsp; {{ toast.title ?? 'Notification' }}
          </c-toast-header>
          <c-toast-body>{{ toast.message }}</c-toast-body>
        </c-toast>
      </ng-container>
    </c-toaster>
  `
})
export class NotificationsComponent implements OnInit, OnDestroy {
  icons = { cilBell };
  toasts: NotificationData[] = [];

  constructor(private notificationService: NotificationService) {
  }

  ngOnInit() {
    this.notificationService.notificationEmitter.subscribe((notification) => {
      this.toasts.push(notification);
    });
  }

  getColorForType(type?: ToastType): Colors | undefined {
    if (type === ToastType.SUCCESS) {
      return 'success';
    }

    if (type === ToastType.FAILURE) {
      return 'danger';
    }

    return undefined;
  }

  ngOnDestroy() {
    this.notificationService.notificationEmitter?.unsubscribe();
  }
}
