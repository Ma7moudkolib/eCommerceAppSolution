import { Injectable, signal, computed, inject } from '@angular/core';
import { Product } from '../models/product';
import { APIService } from './api.service';
import { finalize } from 'rxjs/operators';

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    private apiService = inject(APIService);

    private productsSignal = signal<Product[]>([]);
    private isLoadingSignal = signal<boolean>(false);
    private searchQuerySignal = signal<string>('');

    products = this.productsSignal.asReadonly();
    isLoading = this.isLoadingSignal.asReadonly();
    searchQuery = this.searchQuerySignal.asReadonly();

    filteredProducts = computed(() => {
        const query = this.searchQuerySignal().toLowerCase();
        const products = this.productsSignal();
        if (!query) return products;
        return products.filter(product =>
            product.title.toLowerCase().includes(query) ||
            product.description.toLowerCase().includes(query)
        );
    });

    constructor() {
        this.loadProducts();
    }

    loadProducts() {
        this.isLoadingSignal.set(true);
        this.apiService.getProducts().pipe(
            finalize(() => this.isLoadingSignal.set(false))
        ).subscribe({
            next: (data) => this.productsSignal.set(data),
            error: (err) => console.error('Failed to load products', err)
        });
    }

    loadProductsByCategory(category: string) {
        this.isLoadingSignal.set(true);
        this.apiService.getProductsByCategory(category).pipe(
            finalize(() => this.isLoadingSignal.set(false))
        ).subscribe({
            next: (data) => this.productsSignal.set(data),
            error: (err) => console.error('Failed to load products by category', err)
        });
    }

    getFeaturedProducts() {
        return this.filteredProducts().filter(p => p.featured); // Client-side filter for now
    }

    setSearchQuery(query: string): void {
        this.searchQuerySignal.set(query);
    }

    getProductById(id: number) {
        return this.apiService.getProductById(id);
    }
}
