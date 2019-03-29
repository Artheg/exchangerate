import { Component, OnInit } from '@angular/core';
import { CurrencyService } from 'src/app/services/currency.service';
import { Currency } from 'src/app/model/currency';
import { ExchangeRateService } from '../../services/exchange-rate.service';
import { ExchangeRate } from '../../model/exchange-rate';
import { formatDate } from '@angular/common';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss']
})
export class MainComponent implements OnInit {

  
  public currencyList: Currency[];
  public currentDate: Date;
  public currentCurrency: number;

  public currencyResult: string;

  constructor(private _currencyService: CurrencyService,
    private _exchangeRateService: ExchangeRateService
  ) {
  
  }

  ngOnInit() {
   this.updateCurrencies();
  }

  public onDateChange(): void {
    this.clear();
    this.updateCurrencies();
  }

  private updateCurrencies(): void {
    console.log("currency update");
    const dateParam: string = formatDate(this.currentDate, "dd.MMM yyyy", "en-US");

    this._currencyService.GetList(dateParam).subscribe(
      (currencyList) => {
        this.currencyList = currencyList;
      }
    )
  }

  private clear() {
    this.currencyList = null;
    this.currentCurrency = null;
    this.currencyResult = null;
  }

  public getExchangeRate() {
    const dateParam: string = formatDate(this.currentDate, "dd.MMM yyyy", "en-US");
    console.log();
    this._exchangeRateService.GetExchangeRate(dateParam).subscribe(
      result => {
        this.currencyResult = result.ratesByID[this.currentCurrency];
        console.log("currency is " + result);
      }
    )
  }

}
