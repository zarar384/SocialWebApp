import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { getPaginatedResult, getPaginationHeaders } from './paginationHelper';
import { Message } from '../_models/message';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { User } from '../_models/user';
import { BehaviorSubject, take } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  baseUrl = environment.apiUrl;
  hubUrl = environment.hubUrl;
  private hubConnection?: HubConnection;
  private messageThreadSource = new BehaviorSubject<Message[]>([]);
  messageThread$ = this.messageThreadSource.asObservable();

  constructor(private http: HttpClient) {}

  createHubConnection(user: User, otherUsername: string) {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl + 'message?users=' + otherUsername, {
        accessTokenFactory: () => user.token,
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start().catch((err) => console.log(err));
    this.hubConnection.on('ReceiveMessageThread', (messeges) => {
      this.messageThreadSource.next(messeges);
    });

    this.hubConnection.on('NewMessage', (message) => {
      this.messageThread$.pipe(take(1)).subscribe({
        next: (messages) => {
          this.messageThreadSource.next([...messages, message]);
        },
      });
    });
  }

  stopHubConnection() {
    if (this.hubConnection) {
      this.hubConnection?.stop();
    }
  }

  getMessages(pageNumber: number, pageSize: number, container: string) {
    let params = getPaginationHeaders(pageNumber, pageSize);
    params = params.append('Container', container);
    return getPaginatedResult<Message[]>(
      this.baseUrl + 'messages',
      params,
      this.http
    );
  }

  getMessageThread(username: string) {
    return this.http.get<Message[]>(
      this.baseUrl + 'messages/thread/' + username
    );
  }

  async sendMessage(userName: string, content: string) {
    // return this.http.post<Message>(this.baseUrl + 'messages', {
    //   recipientUsername: userName,
    //   content,
    // });

    return this.hubConnection
      ?.invoke('SendMessage', {
        recipientUsername: userName,
        content,
      })
      .catch((err) => console.log(err));
  }

  deleteMessage(id: number) {
    return this.http.delete(this.baseUrl + 'messages/' + id);
  }
}
