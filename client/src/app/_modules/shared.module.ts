import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ToastrModule } from 'ngx-toastr';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { NgxSpinnerModule } from 'ngx-spinner';
import { BsDatepickerModule} from 'ngx-bootstrap/datepicker';



@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    TabsModule.forRoot(),
    ToastrModule.forRoot({positionClass:'toast-bottom-right'}),
    BsDropdownModule.forRoot(),
    NgxSpinnerModule.forRoot({
      type:"ball-atom"
    }),
    BsDatepickerModule.forRoot(),
  ],
  exports:[
    ToastrModule,
    BsDropdownModule,
    TabsModule,
    NgxSpinnerModule,
    BsDatepickerModule
  ]
})
export class SharedModule { }
