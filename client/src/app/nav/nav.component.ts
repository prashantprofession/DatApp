import { Component, OnInit } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit{

  constructor(public accountService: AccountService) {}
  ngOnInit(): void {
  }

  model:any={};
  
  login() {
    this.accountService.login(this.model).subscribe({
      next: response=> {        
        console.log(response);
      },
      error: error=> console.log(error)      
    });
  }

  logout() {
    this.accountService.logout();
  }

  // getCurrentUser() {
  //   this.accountService.currentUser$.subscribe({
  //     next: user => this.loggedIn = !!user,
  //     error: error => console.log(error)
  //   })
  // }
}
