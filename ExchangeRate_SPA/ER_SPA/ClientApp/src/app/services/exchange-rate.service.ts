import { Injectable, ErrorHandler } from '@angular/core';
import { HttpClient, HttpParams, HttpErrorResponse } from '@angular/common/http';
import { ExchangeRate } from '../model/exchange-rate';
import { URLSearchParams } from 'url';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { AppConfig } from '../config/config';

@Injectable({
  providedIn: 'root'
})
export class ExchangeRateService implements ErrorHandler {

  constructor(
    private _httpClient: HttpClient,
    private _config: AppConfig
  ){}

  public GetExchangeRate(date: string): Observable<ExchangeRate> {
    return this._httpClient.get<ExchangeRate>(this._config.get('PathAPI') + "exchangerate/" + date).pipe(
      catchError(this.handleError)
    );
  }

  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    console.log(error);
      // server-side error
    switch (error.status) {
      case 404:
        errorMessage += 'There is no entry for this date in db \n';
        break;
    
      default:
        break;
    }    

    errorMessage += `Error Code: ${error.status}\nMessage: ${error.message}`;
    window.alert(errorMessage);
    return throwError(errorMessage);
  }
}
