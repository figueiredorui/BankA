import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject, catchError, lastValueFrom, map, of } from 'rxjs';
import { UserInfo } from '../models/user.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  constructor(private http: HttpClient) { }

  private _authStateChanged: Subject<boolean> = new BehaviorSubject<boolean>(false);

  public onStateChanged() {
    return this._authStateChanged.asObservable();
  }

  public async signIn(email: string, password: string) {
    const req = {
      email: email,
      password: password
    };
    const resp = this.http.post(`/api/identity/login?useCookies=true`, req)
      .pipe(
        map(() => {
          this._authStateChanged.next(true);
          return true;
        })
      );

    return await lastValueFrom<any>(resp);
  }

  public user() {
    return this.http.get<UserInfo>(`/api/identity/manage/info`, {
      withCredentials: true
    }).pipe(
      catchError(() => {
        return of({} as UserInfo);
      }));
  }

  public isSignedIn(): Observable<boolean> {
    return this.user().pipe(
      map((userInfo) => {
        const valid = !!(userInfo && userInfo.Email && userInfo.Email.length > 0);
        return valid;
      }),
      catchError(() => {
        return of(false);
      }));
  }
}
