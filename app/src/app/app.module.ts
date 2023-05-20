import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import {HttpClientModule} from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { ShopModule } from './shop/shop.module';
import { BasketModule } from './basket/basket.module';

import { RouterModule } from '@angular/router';




@NgModule({
  declarations: [
    AppComponent,
    

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    CoreModule,
    ShopModule,
    FormsModule,
    HomeModule,
    BasketModule,
    RouterModule


  
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
