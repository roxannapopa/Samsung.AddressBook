import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { catchError, Observable, throwError } from 'rxjs';
import { Contact } from '../models/contact.model';
import { API_URL } from '../constants';

@Injectable({
  providedIn: 'root'
})
export class ContactsService {  

  constructor(private http: HttpClient) {}

  getContacts(): Observable<Contact[]> {
    return this.http.get<Contact[]>(API_URL).pipe(
      catchError(this.handleError)
    ); 
  }

  private handleError(error: HttpErrorResponse) {    
    return throwError('Something went wrong, please try again later.');
  }
}
