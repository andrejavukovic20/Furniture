import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { AccountService } from '../account.service';
import { User } from 'src/app/shared/models/user';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {

  loginForm = new FormGroup({
    username: new FormControl('', Validators.required),
    password: new FormControl('', Validators.required),
  })

  constructor(private accountService: AccountService, private router: Router){}
  
  onSubmit(){
    this.accountService.login(this.loginForm.value).subscribe({
      next: () => this.router.navigateByUrl('/shop')
    })

    
  }
}
