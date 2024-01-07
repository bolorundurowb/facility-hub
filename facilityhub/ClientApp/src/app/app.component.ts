import { Component } from '@angular/core';
import { IconSetService } from '@coreui/icons-angular';
import { freeSet, brandSet } from '@coreui/icons';

@Component({
  selector: 'fh-root',
  template: `
    <router-outlet></router-outlet>
  `
})
export class AppComponent {
  constructor(public iconSet: IconSetService) {
    iconSet.icons = { ...freeSet, ...brandSet };
  }
}
