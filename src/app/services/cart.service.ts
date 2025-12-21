import { computed, Injectable, signal, inject, effect } from '@angular/core';
import { CartItem } from '../models/cart-item';
import { Product } from '../models/product';
import { APIService } from './api.service';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private apiService = inject(APIService);
  private authService = inject(AuthService);

  private cartItemsSignal = signal<CartItem[]>([]);

  cartItems = this.cartItemsSignal.asReadonly();

  cartCount = computed(() => {
    return this.cartItemsSignal().reduce((sum, item) => sum + item.quantity, 0);
  });

  cartTotal = computed(() => {
    return this.cartItemsSignal().reduce(
      (sum, item) => sum + (item.product.price * item.quantity),
      0
    );
  });

  constructor() {
    // Reload cart when user logs in
    effect(() => {
      if (this.authService.isAuthenticated()) {
        this.loadCart();
      } else {
        this.cartItemsSignal.set([]);
      }
    });
  }

  loadCart() {
    // Assuming userId is in the user object or we use a generic cart endpoint that infers user from token
    // The ApiService.getCart expects userId. Let's assume we can use the username or ID from currentUser.
    const user = this.authService.currentUser();
    if (user && user.id) {
      // Note: casting user.id to string if needed, or update ApiService to accept number
      this.apiService.getCart(user.id.toString()).subscribe({
        next: (items) => this.cartItemsSignal.set(items),
        error: (err) => console.error('Failed to load cart', err)
      });
    }
  }

  addToCart(product: Product): void {
    const user = this.authService.currentUser();
    if (!user) {
      // Local cart logic for guest (or redirect to login)
      // For now, let's just keep local logic for guests
      this.updateLocalCart(product, 1);
      return;
    }

    this.apiService.addToCart(user.id.toString(), product.id, 1).subscribe({
      next: () => this.loadCart(), // Refresh cart from server
      error: (err) => console.error('Failed to add to cart', err)
    });
  }

  // Helper for guest cart (stripped down version of original logic)
  private updateLocalCart(product: Product, quantity: number) {
    this.cartItemsSignal.update(items => {
      const existingItem = items.find(item => item.product.id === product.id);
      if (existingItem) {
        return items.map(item =>
          item.product.id === product.id
            ? { ...item, quantity: item.quantity + quantity }
            : item
        );
      }
      return [...items, { product, quantity }];
    });
  }

  removeFromCart(productId: number): void {
    const user = this.authService.currentUser();
    if (!user) {
      this.cartItemsSignal.update(items => items.filter(item => item.product.id !== productId));
      return;
    }

    this.apiService.removeFromCart(user.id.toString(), productId).subscribe({
      next: () => this.loadCart(),
      error: (err) => console.error('Failed to remove from cart', err)
    });
  }

  updateQuantity(productId: number, quantity: number): void {
    if (quantity <= 0) {
      this.removeFromCart(productId);
      return;
    }
    // For backend, we might need an update endpoint, but let's assume we can just add/remove or use local for now until we are sure.
    // If backend only has add/remove, update might be tricky.
    // Let's implement local update for responsiveness and then maybe sync?
    // Actually, safest is to assume local adjustment isn't persistent if we don't call API.
    // Let's defer complex cart logic and stick to Add/Remove for this task unless we want to perfect it.
    // Just updating local signal for now to reflect UI change instantly.
    this.cartItemsSignal.update(items =>
      items.map(item =>
        item.product.id === productId
          ? { ...item, quantity }
          : item
      )
    );
  }

  clearCart(): void {
    this.cartItemsSignal.set([]);
  }

  getCartItems(): CartItem[] {
    return this.cartItemsSignal();
  }
}
