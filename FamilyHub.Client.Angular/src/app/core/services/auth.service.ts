import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { delay, tap } from 'rxjs/operators';
import { Router } from '@angular/router';

@Injectable()
export class AuthService {
  isLoggedIn: boolean = false;

  user = {
    demo: 'demo'
  };

  // store the URL so we can redirect after logging in
  redirectUrl: string;

  /**
   *
   */
  constructor(private router: Router) {
    
  }

  // login(): Observable<boolean> {
  //   return of(true)
  //     .pipe(
  //       delay(1000),
  //       tap(val => this.isLoggedIn = true)
  //     )

  // }
  
  login(email: any, password): any {   
    if (email && password && this.user[email] && this.user[email]==password) {
      this.isLoggedIn = true;

      this.router.navigate(this.redirectUrl ? [this.redirectUrl] : ['./home']);
    } else {
      console.log('user credentials not valid.');      
    }
  }

  logout(): void {
    this.isLoggedIn = false;
  }
}
