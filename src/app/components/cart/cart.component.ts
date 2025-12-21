import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CartService } from '../../services/cart.service';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { InputNumberModule } from 'primeng/inputnumber';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-cart',
  imports: [CommonModule, TableModule, ButtonModule, InputNumberModule, FormsModule, RouterLink],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {
  cartService = inject(CartService);

  cartItems = this.cartService.cartItems;
  cartTotal = this.cartService.cartTotal;
  cartCount = this.cartService.cartCount;

  updateQuantity(id: number, quantity: number) {
    this.cartService.updateQuantity(id, quantity);
  }

  removeItem(id: number) {
    this.cartService.removeFromCart(id);
  }

  checkout() {
    // Implement checkout logic
    alert('Proceeding to checkout...');
  }
}
