using Automarket.DAL.Interfaces;
using Automarket.DAL.Repositories;
using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Responce;
using Automarket.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Implementations
{
    public class WishlistService : IWishlistService
    {
        private readonly IBaseRepository<Wishlist> _wishlistRepository;
        private readonly IAccountService _accountService;
        private readonly IBaseRepository<Consumable> _consumableRepository;

        public WishlistService(IBaseRepository<Wishlist> wishlistRepository, 
            IAccountService accountService, IBaseRepository<Consumable> consumableRepository)
        {
            _wishlistRepository = wishlistRepository;
            _accountService = accountService;
            _consumableRepository = consumableRepository;
        }

        public async Task<BaseResponse<bool>> AddToWishlist(long id)
        {
            try
            {
                var userId = await _accountService.GetIdByEmail();
                var wishlist = await _wishlistRepository.GetAll().Result
                    .FirstOrDefaultAsync(x => x.ItemId == id && x.UserId == userId.Data);

                if (wishlist != null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "The item is already in wishlist",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var newWishlist = new Wishlist()
                {
                    UserId = userId.Data,
                    ItemId = id,
                    CreationDate = DateTime.Now,
                    EditDate = DateTime.Now
                };

                await _wishlistRepository.Create(newWishlist);

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

        public async Task<BaseResponse<bool>> DeleteFromWishlist(long id)
        {
            try
            {
                var userId = await _accountService.GetIdByEmail();
                var wishlist = await _wishlistRepository.GetAll().Result
                    .FirstOrDefaultAsync(x => x.ItemId == id && x.UserId == userId.Data);               

                if (wishlist == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Data = false,
                        Description = "Wishlist not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                await _wishlistRepository.Delete(wishlist);

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

        public async Task<BaseResponse<List<Consumable>>> GetWishlist()
        {
            try
            {
                var wishItems = new List<Consumable>();
                var userId = await _accountService.GetIdByEmail();
                var wishlist = await _wishlistRepository.GetAll().Result
                    .Where(x => x.UserId == userId.Data)
                    .ToListAsync();

                if (wishlist == null)
                {
                    return new BaseResponse<List<Consumable>>()
                    {
                        Description = "Wishlist is empty",
                        StatusCode = StatusCode.OK
                    };
                }

                foreach (var wish in wishlist)
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
                    Description = $"[GetWishlist] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
