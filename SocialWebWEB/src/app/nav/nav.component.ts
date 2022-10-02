import { Component, OnInit } from '@angular/core';
import {AccountService} from "../_services/account.service";
import {observable, Observable} from "rxjs";
import { User } from '../_models/user';
import {Router} from "@angular/router";
import {ToastrService} from 'ngx-toastr'
@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any ={}
  userName: any;

  constructor(public accountService: AccountService, private router: Router,
              private toastr: ToastrService) { }

  ngOnInit(): void {
    this.setUsername()
  }

  login(){
    this.accountService.login(this.model).subscribe(response =>{
      this.setUsername()
      this.router.navigateByUrl('/members')
      console.log(response)
    }, error =>{
      console.log(error)
      this.toastr.error(error.error)
    })
  }

  logout(){
    this.accountService.logout()
    this.router.navigateByUrl('/')
  }

  setUsername(){
    const user: User = JSON.parse(localStorage.getItem('user'))
    if(user){
      this.userName = user.username;
    }
  }
}
