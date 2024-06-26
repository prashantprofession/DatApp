import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Member } from 'src/app/_models/member';
import { AccountService } from 'src/app/_services/account.service';
import { MembersService } from 'src/app/_services/members.service';

@Component({
  selector: 'app-member-detail',
  templateUrl: './member-detail.component.html',
  styleUrls: ['./member-detail.component.css']
})
export class MemberDetailComponent implements OnInit {
  member: Member | undefined;
constructor(private memberService: MembersService, private router: ActivatedRoute) {}
  ngOnInit(): void {
    this.loadMember();
  }

  loadMember() {
    const username=this.router.snapshot.paramMap.get('username');
    console.log('UN:' + username)
    if (!username) return;
    this.memberService.getMember(username).subscribe({
      next: memberData=> { 
        this.member = memberData; 
      } 
    })
  }

}
