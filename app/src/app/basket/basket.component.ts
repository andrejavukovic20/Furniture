import { Component } from '@angular/core';
import { BasketService } from './basket.service';
import { OrderItem } from '../shared/models/basket';

@Component({
  selector: 'app-basket',
  templateUrl: './basket.component.html',
  styleUrls: ['./basket.component.scss']
})
export class BasketComponent {
  
  constructor(public basketService: BasketService) {}
  

  incrementQuantity(orderItem: OrderItem){
    this.basketService.addItemToBasket(orderItem);
  }

  removeItem(orderItemId: number, quantity: number){
    this.basketService.removeItemFromBasket(orderItemId, quantity);
  }
}
