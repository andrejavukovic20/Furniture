export interface Product {
  productId: number;
  name: string;
  description: string;
  color: string;
  pictureUrl: string;
  price: number;
  available: boolean;
  quantity: number;
  categoryId: number;
  category: Category;
  orderItems: any[];
}
export interface Category {
  categoryId: number;
  name: string;
  products: string;
}

