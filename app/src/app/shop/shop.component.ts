import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { Product } from '../shared/models/product';
import { ShopService } from './shop.service';
import { Category } from '../shared/models/category';


@Component({
  selector: 'app-shop',
  templateUrl: './shop.component.html',
  styleUrls: ['./shop.component.scss']
})
export class ShopComponent implements OnInit {
  @ViewChild('searchTerm') searchTerm?: ElementRef;
  products: Product[] = [];
  categories: Category[] =[];
  categoryIdSelected = 0;
  sortSelected = 'price';
  search ='';

  sortOptions = [
    {name: 'Name', value: 'name'},
    {name: 'Price: Low to high', value: 'price'},
    {name: 'Price: High to low', value: 'price desc'},
  ]
  
  page= 1;
  pageSize =10;
  totalItems = 0;

  constructor(private shopServices: ShopService){}

  ngOnInit(): void {
    this.getProducts();
    this.getCategories();
    
  }

  getProducts(){
    this.shopServices.getProducts(this.categoryIdSelected, this.sortSelected, this.searchTerm?.nativeElement.value, this.page, this.pageSize).subscribe({
      next: response  => { 
        this.products = response.items;
        this.totalItems = response.totalItems;

      console.log('Received products:', this.products);
      console.log('Total items:', this.totalItems);
    }, 
      error: error=> console.error(error)
    })
  }

  getCategories(){
    this.shopServices.getCategories().subscribe({
      //...response uzim niz kategorija, ako korisnik hoce da selektuje sve kategorije
      next: response => this.categories =[{categoryId: 0, name:'All'}, ...response],
      error: error=> console.error(error)
    })

  }

  onCategorySelected(categoryId: number){
    this.categoryIdSelected = categoryId === 0 ? 0 : categoryId;
    this.getProducts();
  }

  onSortSelected(event: any){
    this.sortSelected = event.target.value;
    console.log('Selected sort option:', this.sortSelected);
    this.getProducts();
  }

  searchProducts(){
    this.page =1;
    this.getProducts();
  }
 /*searchProducts(){
    this.shopServices.getProducts(this.searchTerm?.nativeElement.value).subscribe({
      next: response => this.products = response.items,
      error: error => console.error(error)
    });
    console.log(this.searchTerm);
  }
*/
  onReset(){
    if(this.searchTerm) this.searchTerm.nativeElement.value='';
    console.log(this.searchTerm);
    this.searchProducts();
  }
  onPageChange(event: any) {
    if(this.page !== event.page){
      this.page = event.page
      console.log('New page:', this.page);
      this.getProducts();
    }
    
    
  }

}
