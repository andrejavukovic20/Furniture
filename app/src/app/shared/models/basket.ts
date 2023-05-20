export interface Basket {
  basketId: number
  orderItems: OrderItem[]
}

export interface OrderItem {
  orderItemId: number
  orderId: number
  productId: number
  basketId: string | null
  quantity: number
  pictureUrl: string // Added property
  name: string // Added property
  categoryName: string
  price: number


 
} 


export class Basket implements Basket {
  basketId= Math.floor(Math.random() *100)+2;
  orderItems: OrderItem[] = [];
 
}

export interface BasketTotals {
  subtotal: number
  total: number

}