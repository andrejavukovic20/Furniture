import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Category, Product } from '../shared/models/product';
import { Observable } from 'rxjs';
import { Pagination } from '../shared/models/pagination';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  baseUrl = 'https://localhost:7097/api/'

  constructor(private http: HttpClient) {}

  getProducts(categoryId: number= 0, sort?: string, searchTerm?:string, page:number =1, pageSize: number=10)  {
    let params = new HttpParams();
  
    if (categoryId !== 0) {
      params = params.set('filterOn', 'CategoryId');
      params = params.set('filterQuery', categoryId.toString());
    }
    if(sort) params = params.append('sortBy', sort);
    if(searchTerm){
      params = params.set('search', searchTerm);
    } 

    params = params.set('page', page);
    params = params.set('pageSize', pageSize);
  

    return this.http.get<Pagination<Product[]>>(this.baseUrl + 'products', { params });
  }
  getProduct(productId: number){
    return this.http.get<Product>(this.baseUrl + 'products/' + productId);
  }
  getCategories() {
    return this.http.get<Category[]>(this.baseUrl + 'productCategory');
  }
  
 
}
