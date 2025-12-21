import { Component, input } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Product } from '../../models/product';


@Component({
  selector: 'app-product-cart',
  imports: [CommonModule],
  templateUrl: './product-cart.component.html',
  styleUrl: './product-cart.component.css'
})
export class ProductCartComponent {
 product = input.required<Product>();
}
