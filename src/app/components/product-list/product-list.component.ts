import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProductService } from '../../services/product.service';
import { ProductCartComponent } from '../product-cart/product-cart.component';
import { CategoryService } from '../../services/category.service';

@Component({
  selector: 'app-product-list',
  imports: [CommonModule, ProductCartComponent],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent {
  productService = inject(ProductService);
  categoryService = inject(CategoryService);

  products = this.productService.filteredProducts;
  isLoading = this.productService.isLoading;

  constructor() {
    // Ensure products are loaded
    this.productService.loadProducts();
  }
}
