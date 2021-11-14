using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShopSolution.Core.Entities;
using ShopSolution.Core.Interfaces;

namespace ShopSolution.API.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<ActionResult<CustomerBasket>> GetBasketById(string id)
        {
            var basket = await _repository.GetBasketAsync(id);
            return Ok(basket ?? new CustomerBasket(id));
        }

        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket basket)
        {
            var updatedBasket = await _repository.UpdateBasketAsync(basket);
            return Ok(updatedBasket);
        }

        [HttpDelete]
        public async Task DeleteBasketAsync(string id)
        {
            await _repository.DeleteBasketAsync(id);
        }
    }
}