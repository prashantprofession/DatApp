import { CanActivateFn } from '@angular/router';
import { AccountService } from '../_services/account.service';
import { inject } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { map } from 'rxjs';

export const AuthGuard: CanActivateFn = (route, state) => {
  const accountService = inject(AccountService);
  const toastService = inject(ToastrService);
  return accountService.currentUser$.pipe(
    map(user => {
      if(user) return true;
      else {
        toastService.error("You are not allowed to access this page");
        return false;
      }
    })
  )
};
