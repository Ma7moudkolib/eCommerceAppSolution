import { Component, inject, output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { ProductService } from '../../services/product.service';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { AvatarModule } from 'primeng/avatar';
import { MenubarModule } from 'primeng/menubar';
import { BadgeModule } from 'primeng/badge';
import { MenuModule } from 'primeng/menu';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-user-nav',
  imports: [
    FormsModule,
    CommonModule,
    InputTextModule,
    ButtonModule,
    AvatarModule,
    MenubarModule,
    BadgeModule,
    MenuModule
  ],
  templateUrl: './user-nav.component.html',
  styleUrl: './user-nav.component.css'
})
export class UserNavComponent {
  authService = inject(AuthService);
  productService = inject(ProductService);

  items: MenuItem[] = [
    { label: 'Home', icon: 'pi pi-home', routerLink: '/' },
    { label: 'Products', icon: 'pi pi-shopping-bag', routerLink: '/products' }
  ];

  userMenuItems: MenuItem[] = [
    { label: 'Profile', icon: 'pi pi-user' },
    { label: 'Orders', icon: 'pi pi-box' },
    { separator: true },
    { label: 'Logout', icon: 'pi pi-sign-out', command: () => this.authService.logout() }
  ];

  onSearch(event: Event) {
    const target = event.target as HTMLInputElement;
    this.productService.setSearchQuery(target.value);
  }
}
