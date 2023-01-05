using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class CustomersController : CommonController
    {
        public CustomersController(IWarehouseCustomersService warehouseCustomersService) : base(warehouseCustomersService)
        {
            
        }
    }
}
