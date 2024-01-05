import { Component, OnDestroy, OnInit } from '@angular/core';
import { Title } from "@angular/platform-browser";
import { Router } from "@angular/router";
import { AuthService } from "../../services";

@Component({
  selector: 'fh-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy {
  isLoggedIn = false;
  user: any;

  constructor(title: Title, private router: Router, private authService: AuthService) {
    title.setTitle('Home | Facility Hub');

    this.isLoggedIn = authService.isLoggedIn();
    this.user = authService.getUser();
  }

  ngOnInit() {
    this.authService.authenticated.subscribe(this.handleAuthChange);
  }

  async goToLogin() {
    await this.router.navigate(['auth', 'login']);
  }

  async goToRegister() {
    await this.router.navigate(['auth', 'register']);
  }

  async goToDashboard() {
    await this.router.navigate(['dashboard']);
  }

  async logOut() {
    this.authService.logout();
    await this.router.navigate(['/']);
  }

  handleAuthChange(event: boolean) {
    console.log('hello', event);
    this.isLoggedIn = event;
    this.user =  this.authService.getUser();
  }

  ngOnDestroy() {
    this.authService.authenticated.unsubscribe();
  }
}
