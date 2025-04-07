namespace eCommerce.Application.DTOs.Favourite
{
    public class CreateFavourite
    {
        public required Guid ProductId { get; set; }
        public required Guid UserId { get; set; }
    }
}
