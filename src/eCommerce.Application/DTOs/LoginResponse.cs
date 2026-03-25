using eCommerce.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.DTOs
{
  public record LoginResponse(bool Success = false 
      , string massage = null! 
      , string Token = null! 
      , string refreshToken =null!);
}
