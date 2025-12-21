import { ProductCartComponent } from './../../components/product-cart/product-cart.component';
import { Component, inject } from '@angular/core';
import { ProductService } from '../../services/product.service';
import { CategoryService } from '../../services/category.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  imports: [CommonModule, ProductCartComponent], // Removed UserNavComponent
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
  productService = inject(ProductService);
  categoryService = inject(CategoryService);

  // Expose signals for template
  filteredProducts = this.productService.filteredProducts;
  isLoading = this.productService.isLoading;
  categories = this.categoryService.categories;
  selectedCategory = this.categoryService.selectedCategory;

  constructor() {
    // Initial load happens in services or we can trigger it here if needed
  }

  selectCategory(categoryId: string) {
    this.categoryService.setSelectedCategory(categoryId);
    // Logic to filter products by category in ProductService or here?
    // ProductService currently filters by "searchQuery".
    // We should probably add "categoryFilter" to ProductService or trigger a fetch.
    if (categoryId === 'all') {
      this.productService.loadProducts(); // helper to reset
    } else {
      // We need a way to filter by category in ProductService, or fetch by category
      // ApiService has getProductsByCategory.
      // ProductService should probably have a method setCategory(cat) which handles fetching.
      // For now, let's call API directly via ProductService if we add a method, or just update the service.
      // Better: ProductService.filterByCategory(cat);
      // Let's implement setCategory in ProductService, or just use the one in CategoryService and react to it?
      // Simpler: Just fetch in this component for now or add method to ProductService.
      // Let's add 'loadProductsByCategory' to ProductService.
      this.productService.loadProductsByCategory(categoryId);
    }
  }
}
