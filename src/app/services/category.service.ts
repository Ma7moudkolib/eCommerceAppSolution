import { computed, Injectable, signal, inject } from '@angular/core';
import { Category } from '../models/category';
import { APIService } from './api.service';

@Injectable({
  providedIn: 'root'
})
export class CategoryService {
  private apiService = inject(APIService);

  private categoriesSignal = signal<Category[]>([]);
  private selectedCategorySignal = signal<string>('all');

  categories = this.categoriesSignal.asReadonly();
  selectedCategory = this.selectedCategorySignal.asReadonly();

  activeCategoryDetails = computed(() => {
    const categoryId = this.selectedCategorySignal();
    return this.categoriesSignal().find(cat => cat.id === categoryId);
  });

  constructor() {
    this.loadCategories();
  }

  loadCategories() {
    this.apiService.getCategories().subscribe({
      next: (data) => {
        // Convert string[] to Category[]
        const categories: Category[] = [
          { id: 'all', name: 'All Products', description: 'Browse all products', icon: '🛍️' },
          ...data.map(cat => ({
            id: cat,
            name: cat.charAt(0).toUpperCase() + cat.slice(1),
            description: `Browse ${cat}`,
            icon: '📦'
          }))
        ];
        this.categoriesSignal.set(categories);
      },
      error: (err) => console.error('Failed to load categories', err)
    });
  }

  setSelectedCategory(categoryId: string): void {
    this.selectedCategorySignal.set(categoryId);
  }
}
