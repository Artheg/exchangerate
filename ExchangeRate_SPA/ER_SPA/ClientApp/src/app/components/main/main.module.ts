import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MainComponent } from './main.component';
import { MainRoutingModule } from './main-routing.module';
import { MatDatepickerModule,
  MatNativeDateModule,
  MatFormFieldModule,
  MatInputModule, 
  MatSelectModule,
  MatDividerModule,
  MatButtonModule} from '@angular/material';
import { FormsModule } from '@angular/forms';
import { CurrencyService } from 'src/app/services/currency.service';
import { ExchangeRateService } from 'src/app/services/exchange-rate.service';


@NgModule({
  declarations: [MainComponent],
  imports: [
    CommonModule,
    MainRoutingModule,
    MatDatepickerModule,
    MatFormFieldModule,
    MatNativeDateModule,
    MatInputModule,
    MatSelectModule,
    MatDividerModule,
    FormsModule,
    MatButtonModule
  ]
})
export class MainModule { }
