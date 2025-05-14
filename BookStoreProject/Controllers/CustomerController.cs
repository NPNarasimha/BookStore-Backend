using ManagerLayer.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreProject.Controllers
{
    [Route("api/Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerManager customerManager;
        public CustomerController(ICustomerManager customerManager)
        {
            this.customerManager = customerManager;
        }

    }
}
