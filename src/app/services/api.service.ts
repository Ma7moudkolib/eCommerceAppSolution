import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product';
import { User } from '../models/user';
import { RegisterData } from '../models/register-data';
import { baseUrl } from '../apiRoot/baseUrl';
import { CartItem } from '../models/cart-item';

@Injectable({
  providedIn: 'root'
})
export class APIService {

  private http = inject(HttpClient);
  private baseUrl = baseUrl;

  // Products
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/Products`);
  }

  getProductsByCategory(category: string): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.baseUrl}/Products?category=${category}`); // Adusting param to query param as typical in .NET
  }

  getProductById(id: number): Observable<Product> {
    return this.http.get<Product>(`${this.baseUrl}/Products/${id}`);
  }

  getCategories(): Observable<string[]> {
    return this.http.get<string[]>(`${this.baseUrl}/Products/categories`);
  }

  // Auth
  login(credentials: any): Observable<{ token: string; user: User }> {
    return this.http.post<{ token: string; user: User }>(`${this.baseUrl}/Account/login`, credentials);
  }

  register(registerData: RegisterData): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}/Account/register`, registerData);
  }

  getUser(userId: number): Observable<User> {
    return this.http.get<User>(`${this.baseUrl}/Users/${userId}`);
  }

  // Cart (assuming standard endpoints)
  getCart(userId: string): Observable<CartItem[]> {
    return this.http.get<CartItem[]>(`${this.baseUrl}/Cart/${userId}`);
  }

  addToCart(userId: string, productId: number, quantity: number): Observable<any> {
    return this.http.post(`${this.baseUrl}/Cart`, { userId, productId, quantity });
  }

  removeFromCart(userId: string, productId: number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/Cart/${userId}/${productId}`);
  }
}
