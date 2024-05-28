using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Responce;
using Automarket.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Automarket.Service.Implementations
{
    public class BasketService : IBasketService
    {
        private readonly IBaseRepository<Basket> _basketRepository;
        private readonly IAccountService _accountService;
        private readonly IBaseRepository<Consumable> _consumableRepository;

        public BasketService(IBaseRepository<Basket> basketRepository,
            IAccountService accountService, IBaseRepository<Consumable> consumableRepository)
        {
            _basketRepository = basketRepository;
            _accountService = accountService;
            _consumableRepository = consumableRepository;
        }

        public async Task<BaseResponse<bool>> AddToBasket(long id)
        {
            try
            {
                var userId = await _accountService.GetIdByEmail();
                var basket = await _basketRepository.GetAll().Result
                    .FirstOrDefaultAsync(x => x.ItemId == id && x.UserId == userId.Data);

                if (basket != null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "The item is already in basket",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var newBasket = new Basket()
                {
                    UserId = userId.Data,
                    ItemId = id,
                    Quantity = 1,
                    CreationDate = DateTime.Now,
                    EditDate = DateTime.Now
                };

                await _basketRepository.Create(newBasket);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Item created",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = $"[AddToBasket] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteFromBasket(long id)
        {
            try
            {
                var userId = await _accountService.GetIdByEmail();
                var basket = await _basketRepository.GetAll().Result
                    .FirstOrDefaultAsync(x => x.ItemId == id && x.UserId == userId.Data);

                if (basket == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Basket not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                await _basketRepository.Delete(basket);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Item deleted",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Data = false,
                    Description = $"[DeleteFromWishlist] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<Consumable>>> GetBasket()
        {
            try
            {
                var wishItems = new List<Consumable>();
                var userId = await _accountService.GetIdByEmail();
                var basket = await _basketRepository.GetAll().Result
                    .Where(x => x.UserId == userId.Data)
                    .ToListAsync();         

                if (basket == null)
                {
                    return new BaseResponse<List<Consumable>>()
                    {
                        Description = "Basket is empty",
                        StatusCode = StatusCode.OK
                    };
                }

                foreach (var basketItem in basket)
                {
                    var item = await _consumableRepository.GetAll().Result
                    .FirstOrDefaultAsync(x => x.Id == basketItem.ItemId);

                    if (basketItem.Quantity > item!.Quantity)
                    {
                        basketItem.Quantity = item!.Quantity;

                        await _consumableRepository.Update(item!);
                    }
                }

                foreach (var wish in basket)
                {
                    var item = await _consumableRepository.GetAll().Result
                    .FirstOrDefaultAsync(item => wish.ItemId == item.Id);

                    wishItems.Add(item!);
                }

                return new BaseResponse<List<Consumable>>()
                {
                    Data = wishItems,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Consumable>>()
                {
                    Description = $"[GetBasket] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> Minus(long id)
        {
            try
            {
                var basket = await _basketRepository.GetAll().Result
                    .FirstOrDefaultAsync(x => x.ItemId == id);

                if (basket == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Basket not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                basket.Quantity -= 1;
                basket.EditDate = DateTime.Now;

                if (basket.Quantity < 1)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Quantity must be between 1 and 99",
                        StatusCode = StatusCode.InternalServerError
                    };
                }

                await _basketRepository.Update(basket);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[Minus] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> Plus(long id)
        {
            try
            {
                var basket = await _basketRepository.GetAll().Result
                    .FirstOrDefaultAsync(x => x.ItemId == id);

                if (basket == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Basket not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                basket.Quantity += 1;
                basket.EditDate = DateTime.Now;

                if (basket.Quantity > 99)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Quantity must be between 1 and 99",
                        StatusCode = StatusCode.InternalServerError
                    };
                }

                await _basketRepository.Update(basket);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[Plus] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<int>> GetQuantity(long id)
        {
            try
            {
                var basket = await _basketRepository.GetAll().Result
                    .FirstOrDefaultAsync(x => x.ItemId == id);

                if (basket == null)
                {
                    return new BaseResponse<int>()
                    {
                        Description = "Basket not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                return new BaseResponse<int>()
                {
                    Data = basket.Quantity,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<int>()
                {
                    Description = $"[GetQuantity] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<double>> GetTotalPrice()
        {
            try
            {
                double price = 0;
                var userId = await _accountService.GetIdByEmail();
                var basket = await _basketRepository.GetAll().Result
                    .Where(x => x.UserId == userId.Data)
                    .ToListAsync();

                foreach (var wish in basket)
                {
                    var item = await _consumableRepository.GetAll().Result
                        .FirstOrDefaultAsync (x => x.Id == wish.ItemId);
                    price += wish.Quantity * item!.Price;
                }

                return new BaseResponse<double>()
                {
                    Data = price,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<double>()
                {
                    Description = $"[GetTotalPrice] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
