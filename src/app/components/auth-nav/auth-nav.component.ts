import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-auth-nav',
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './auth-nav.component.html',
  styleUrl: './auth-nav.component.css'
})
export class AuthNavComponent {
//  cartService = inject(CartService);
//   productService = inject(ProductService);
//   authService = inject(AuthService);

//   searchQuery = '';
//   showCart = false;

//   onSearch(): void {
//     // this.productService.setSearchQuery(this.searchQuery);
//   }

//   toggleCart(): void {
//     this.showCart = !this.showCart;
//   }

//   updateQuantity(productId: number, delta: number): void {
//     const item = this.cartService.cartItems().find(i => i.product.id === productId);
//     if (item) {
//       this.cartService.updateQuantity(productId, item.quantity + delta);
//     }
//   }

//   removeItem(productId: number): void {
//     this.cartService.removeFromCart(productId);
//   }
}
