﻿namespace eCommerce.Application.DTOs.Identity
{
    public class CreateUser : BaseModel
    {
        public required string Role { get; set; }
        public required string FullName { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}
