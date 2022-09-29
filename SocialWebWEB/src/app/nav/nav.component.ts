import { Component, OnInit } from '@angular/core';
import {AccountService} from "../_services/account.service";
import {observable, Observable} from "rxjs";
import { User } from '../_models/user';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any ={}
  userName: any;

  constructor(public accountService: AccountService) { }

  ngOnInit(): void {
    this.setUsername()
  }

  login(){
    this.accountService.login(this.model).subscribe(response =>{
      this.setUsername()
      console.log(response)
    }, error =>{
      console.log(error)
    })
  }

  logout(){
    this.accountService.logout()
  }

  setUsername(){
    const user: User = JSON.parse(localStorage.getItem('user'))
    if(user){
      this.userName = user.username;
    }
  }
}
