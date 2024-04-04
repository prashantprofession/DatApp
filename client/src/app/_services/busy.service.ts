import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
  busyCounter = 0;
  constructor(private spinnerService: NgxSpinnerService) { }
  
  busy() {
    this.busyCounter++;
    this.spinnerService.show(undefined, {
       type:'line-scale-party',
       bdColor: 'rgba(255,255,255,0)',
       color: '#333333'
    });
  }

  idle() {
    this.busyCounter--;
    if (this.busyCounter <=0 ) {
      this.busyCounter = 0;
      this.spinnerService.hide();
    }
  }
}
