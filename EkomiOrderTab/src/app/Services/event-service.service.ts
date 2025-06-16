import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EventServiceService {

  constructor() { }

  private userDataUpdated = new Subject<void>();
  userDataUpdated$ = this.userDataUpdated.asObservable();
  triggerUserDataUpdate() {
    this.userDataUpdated.next();
  }

}
