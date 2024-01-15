import { EventEmitter, Injectable } from '@angular/core';
import { NotificationData, ToastType } from '../components';

@Injectable({ providedIn: 'root' })
export class NotificationService {
  public notificationEmitter = new EventEmitter<NotificationData>();

  show(message: string, title: string | undefined = undefined) {
    this.notificationEmitter.emit({
      message,
      title,
    });
  }

  showSuccess(message: string, title: string | undefined = undefined) {
    this.notificationEmitter.emit({
      message,
      title,
      type: ToastType.SUCCESS
    });
  }

  showError(message: string, title: string | undefined = undefined) {
    this.notificationEmitter.emit({
      message,
      title,
      type: ToastType.FAILURE
    });
  }
}
