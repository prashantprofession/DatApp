import { Component, EventEmitter, Input, Output } from '@angular/core';
import { AccountService } from '../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {
@Output() cancelRegisterEmitter=new EventEmitter();
model:any = {};

constructor(private accountService: AccountService,
  private toastrService: ToastrService) {}
register() {
 console.log(this.model);
 this.accountService.register(this.model).subscribe({
  next: () => this.cancel(),
  error: error => this.toastrService.error(error.error)
 })
}

cancel() {
  console.log("Cancelled");
  this.cancelRegisterEmitter.emit(false);
}

}
