# eCommerce Project

A modern eCommerce platform built with ASP.NET Core (.NET 8) and Entity Framework Core. This project demonstrates a scalable architecture for online stores, including user authentication, product management, shopping cart, and payment integration.

## Features

- **User Authentication:** ASP.NET Core Identity for secure user management.
- **Product Catalog:** CRUD operations for products and categories.
- **Shopping Cart:** Add, update, and remove items from the cart.
- **Order Processing:** Checkout and order achievement tracking.
- **Payment Methods:** Multiple payment options (Credit Card, PayPal, Bank Transfer, Cash on Delivery).
- **Token Management:** Refresh token support for secure API access.
- **Database Seeding:** Static dummy data for development and testing.

## Technologies Used

- [.NET 8](https://dotnet.microsoft.com/)
- [ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Setup Instructions

1. **Clone the repository:**
2. **Update the database:**
3. **Run the application:**

4. **Access the app:**
   Open your browser and navigate to `https://localhost:5001` (or the port specified in your launch settings).

## Project Structure

- `eCommerce.Domain`: Domain entities and business logic.
- `eCommerce.Infrastructure`: Data access, EF Core context, and migrations.
- `eCommerce.Application`: Application services and use cases.
- `eCommerce.Web`: ASP.NET Core web application (API/UI).

## Database Seeding

The project seeds static dummy data for categories, products, payment methods, achievements, and refresh tokens. This helps with development and testing without manual data entry.

## Contributing

Contributions are welcome! Please fork the repository and submit a pull request.

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

## Contact

For questions or feedback, please open an issue or contact the maintainer at [Mahmoudkolib22@gmail.com](mailto:Mahmoudkolib22@gmail.com).   
