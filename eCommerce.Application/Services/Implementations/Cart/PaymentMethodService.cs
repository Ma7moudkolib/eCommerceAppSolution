using AutoMapper;
using eCommerce.Application.DTOs.Cart;
using eCommerce.Application.Services.Interfaces.Cart;
using eCommerce.Domain.Interfaces.Cart;
using System;
using System.Collections.Generic;
namespace eCommerce.Application.Services.Implementations.Cart
{
    public class PaymentMethodService(IPaymentMethod paymentMethod ,IMapper mapper ) : IPaymentMethodService
    {
        public async Task<IEnumerable<GetPaymentMethod>> GetPaymentMethods()
        {
            var methods = await paymentMethod.GetPaymentMethodAsync();
            if(!methods.Any()) { return[];  };

            return  mapper.Map<IEnumerable< GetPaymentMethod>>( methods );
        }
    }
}
