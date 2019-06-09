using OnlineMarketPlace.Application.DataTransfer;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineMarketPlace.API
{
    public class ModelsDocumentFilter : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, DocumentFilterContext context)
        {
            context.SchemaRegistry.GetOrRegister(typeof(CategoryDto));
            context.SchemaRegistry.GetOrRegister(typeof(CouponDto));
            context.SchemaRegistry.GetOrRegister(typeof(CreateOrderDto));
            context.SchemaRegistry.GetOrRegister(typeof(CreateProductDto));
            context.SchemaRegistry.GetOrRegister(typeof(CreateUserDto));
            context.SchemaRegistry.GetOrRegister(typeof(GetOrderDto));
            context.SchemaRegistry.GetOrRegister(typeof(GetProductDto));
            context.SchemaRegistry.GetOrRegister(typeof(GetUserDto));
            context.SchemaRegistry.GetOrRegister(typeof(OrderProductsDto));
            context.SchemaRegistry.GetOrRegister(typeof(RoleDto));
            context.SchemaRegistry.GetOrRegister(typeof(ShipperDto));
            context.SchemaRegistry.GetOrRegister(typeof(ShippingAddressDto));
            context.SchemaRegistry.GetOrRegister(typeof(SubCategoryDto));
        }
    }
}
