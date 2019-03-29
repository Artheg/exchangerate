import { ErrorHandler, Injectable } from '@angular/core';
import { Currency } from '../model/currency';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppConfig } from '../config/config';

@Injectable({
  providedIn: 'root'
})

export class CurrencyService {

  constructor(
    private _httpClient: HttpClient,
    private _config: AppConfig
){}

  public GetList(date: string): Observable<Currency[]> {
    return this._httpClient.get<Currency[]>(this._config.get('PathAPI') + "/currency/" + date).pipe();
  }
}
