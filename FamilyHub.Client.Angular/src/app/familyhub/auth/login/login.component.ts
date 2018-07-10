import { AuthService } from '../../../core/services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html'
})
export class LoginComponent implements OnInit {
  public email;
  public password;

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login(event){
    event.preventDefault();
    this.authService.login(this.email, this.password);    
  }

}
