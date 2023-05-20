import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/shared/models/product';
import { ShopService } from '../shop.service';
import { ActivatedRoute } from '@angular/router';
import { BasketService } from 'src/app/basket/basket.service';
import { take } from 'rxjs';

@Component({
  selector: 'app-product-details',
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.scss']
})
export class ProductDetailsComponent implements OnInit {
  product?: Product;
  quantity = 1;
  quantityInBasket = 0;

  constructor(private shopService: ShopService, private activatedRoute: ActivatedRoute,
    private basketService: BasketService) {}

  ngOnInit(): void {
    this.loadProduct();
    
  }

  loadProduct(){
    const productId =this.activatedRoute.snapshot.paramMap.get('productId');
    if (productId) this.shopService.getProduct(+productId).subscribe({
      next: product => {
        this.product = product;
       /* this.basketService.basketSource$.pipe(take(1)).subscribe({
          next: basket => {
            const item = basket?.orderItems.find(x =>x.orderItemId === +productId);
            if(item){
              this.quantity = item.quantity;
              item.quantityInBasket = item.quantity;
            }
          }
        })*/
      }, 
      error: error => console.error(error)
       
   
    })
  }
  incrementQuantity(){
    this.quantity++;
  }

  decrementQuantity() {
    this.quantity--;
  }

  updateBasket(){
    if(this.product){
      if(this.quantity > this.quantityInBasket){
        const itemsToAdd = this.quantity - this.quantityInBasket;
        this.quantityInBasket += itemsToAdd;
        this.basketService.addItemToBasket(this.product, itemsToAdd);

      }else{
        const itemsToRemove = this.quantityInBasket - this.quantity;
        this.quantityInBasket -= itemsToRemove;
        this.basketService.removeItemFromBasket(this.product.productId, itemsToRemove);
      }
    }
  }
  get buttonText(){
    return this.quantityInBasket === 0? 'Add to basket' : 'Update basket';
  }


}
