import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
@Output() cancelRegisterEmitter=new EventEmitter();
model:any = {};

constructor(private accountService: AccountService) {}
register() {
 console.log(this.model);
 this.accountService.register(this.model).subscribe({
  next: () => this.cancel(),
  error: error => console.log(error)
 })
}

cancel() {
  console.log("Cancelled");
  this.cancelRegisterEmitter.emit(false);
}

}
