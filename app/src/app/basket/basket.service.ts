import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Basket, BasketTotals, OrderItem } from '../shared/models/basket';
import { HttpClient } from '@angular/common/http';
import { Product } from '../shared/models/product';


@Injectable({
  providedIn: 'root'
})
export class BasketService {
  baseUrl = environment.apiUrl;
  private basketSource = new BehaviorSubject<Basket | null>(null);
  private orderItemSource = new BehaviorSubject<OrderItem | null>(null);
  basketSource$ = this.basketSource.asObservable();
  orderItemSource$ = this.orderItemSource.asObservable();
  private basketTotalSource = new BehaviorSubject<BasketTotals | null>(null);
  basketTotalSource$ = this.basketTotalSource.asObservable();


  constructor(private http: HttpClient) { 
    
  }

  getBasket(basketId: string | null){
    return this.http.get<Basket>(this.baseUrl + 'basket?basketId=' + basketId).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    });

  }

  setBasket(basket: Basket){
    return this.http.post<Basket>(this.baseUrl + 'basket', basket).subscribe({
      next: basket => {
        this.basketSource.next(basket);
        this.calculateTotals();
      }
    });
  }


  addOrderItem(orderItem: OrderItem){
    return this.http.post<OrderItem>(this.baseUrl + 'orderItem', orderItem).subscribe({
      next: orderItem => this.orderItemSource.next(orderItem)
    });
  }

  getCurrentBasketValue(){
    return this.basketSource.value;
  }

  addItemToBasket(item: Product | OrderItem, quantity=1){
    //if('categoryId' in item) this.mapProductItemToOrderItem(item);
    if(this.isProduct(item)) item = this.mapProductItemToOrderItem(item);

    //const itemToAdd = this.mapProductItemToOrderItem(item);
    const basket = this.getCurrentBasketValue() ?? this.createBasket();
    basket.orderItems = this.addOrUpdateItem(basket.orderItems, item, quantity);
    this.addOrderItem(item);

  }

  removeItemFromBasket(orderItemId: number, quantity=1){
    const basket = this.getCurrentBasketValue();
    if(!basket) return;
    const item = basket.orderItems.find(x => x.orderItemId === orderItemId);
    if(item){
      item.quantity -= quantity;
      if(item.quantity === 0){
        basket.orderItems = basket.orderItems.filter(x => x.orderItemId !== orderItemId);
      }
      if(basket.orderItems.length > 0) this.setBasket(basket);
      else this.deleteBasket(basket);
    }

  }
  deleteBasket(basket: Basket) {
    return this.http.delete(this.baseUrl + 'basket?basketId=' + basket.basketId).subscribe({
      next: () => {
        this.basketSource.next(null);
        this.basketTotalSource.next(null);
        localStorage.removeItem('basketId');

      }
    })
  }

  
  private addOrUpdateItem(orderItems: OrderItem[], itemToAdd: OrderItem, quantity: number): OrderItem[] {
    const item = orderItems.find(x => x.orderItemId === itemToAdd.orderItemId);
    if(item) item.quantity += quantity;
    else{
      itemToAdd.quantity = quantity;
      orderItems.push(itemToAdd);
    }

    return orderItems;
  }

  createBasket(): Basket  {
      const basket = new Basket();
      localStorage.setItem('basketId', basket.basketId.toString());
      this.setBasket(basket);
      return basket;
    
   

  }

  private mapProductItemToOrderItem(item: Product) : OrderItem{
    return {
      orderItemId: item.productId,
      orderId: 412900594, 
      productId: item.productId,
      basketId: localStorage.getItem('basketId'), 
      quantity: 1,
      pictureUrl: item.pictureUrl,
      name: item.name,
      categoryName: item.category.name,
      price: item.price,
     
        
    }

  }
  private calculateTotals(){
    const basket = this.getCurrentBasketValue();
    if(!basket) return;
    const subtotal = basket.orderItems.reduce((a, b) => (b.price * b.quantity) + a, 0);
    const total = subtotal;
    this.basketTotalSource.next({total, subtotal});
  }

  private isProduct(item: Product | OrderItem): item is Product{
    return(item as Product).categoryId !== undefined;
  }
}
