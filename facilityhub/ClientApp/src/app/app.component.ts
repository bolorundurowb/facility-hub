import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { AuthService } from './services';
import { IconSetService } from '@coreui/icons-angular';
import { cilBuilding } from '@coreui/icons';

@Component({
  selector: 'fh-root',
  template: `
    <router-outlet></router-outlet>
  `
})
export class AppComponent {
  constructor(public iconSet: IconSetService) {
    iconSet.icons = { cilBuilding };
  }
}
