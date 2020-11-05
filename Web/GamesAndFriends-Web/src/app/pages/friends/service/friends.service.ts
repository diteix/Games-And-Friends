import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FriendsService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = `${environment.apiUrl}/friend/`;
  }

  public getAll(): Observable<any> {
    return this.http.get(this.baseUrl);
  }

  public add(friend: any): Observable<any> {
    return this.http.post(this.baseUrl, friend);
  }

  public edit(id: number, friend: any): Observable<any> {
    return this.http.put(this.baseUrl + id, friend);
  }

  public delete(id: number): Observable<any> {
    return this.http.delete(this.baseUrl + id);
  }
}
