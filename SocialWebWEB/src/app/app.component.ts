import {Component, OnInit} from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  title = 'SocialWebWEB';
  users: any;

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.getUsers()
  }

  getUsers(){
    this.http.get('https://localhost:5001/api/users')
      .subscribe({
        next: res => this.users = res,
        error: error => console.log(error)
      })
  }
}
