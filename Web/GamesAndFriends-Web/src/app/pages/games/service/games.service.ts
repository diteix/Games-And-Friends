import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class GamesService {
  private baseUrl: string;

  constructor(private http: HttpClient) {
    this.baseUrl = `${environment.apiUrl}/game/`;
  }

  public getAll(): Observable<any> {
    return this.http.get(this.baseUrl);
  }

  public add(game: any): Observable<any> {
    return this.http.post(this.baseUrl, game);
  }

  public edit(id: number, game: any): Observable<any> {
    return this.http.put(this.baseUrl + id, game);
  }

  public delete(id: number): Observable<any> {
    return this.http.delete(this.baseUrl + id);
  }

  public lend(id: number, idFriend: number): Observable<any> {
    return this.http.patch(`${this.baseUrl}${id}/lend`, idFriend);
  }

  public tackBack(id: number): Observable<any> {
    return this.http.patch(`${this.baseUrl}${id}/take-back`, null);
  }
}
