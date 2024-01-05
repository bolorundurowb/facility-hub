import { Component, OnDestroy, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { Title } from "@angular/platform-browser";
import { AuthService } from "./services";

@Component({
  selector: 'fh-root',
  template: `
    <router-outlet></router-outlet>
  `
})
export class AppComponent {
}
