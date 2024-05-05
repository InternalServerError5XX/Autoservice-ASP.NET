using Automarket.Domain.Entity;
using Automarket.Domain.Responce;
using Automarket.Domain.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Interfaces
{
    public interface IWishlistService
    {
        Task<BaseResponse<List<Consumable>>> GetWishlist();

        Task<BaseResponse<bool>> AddToWishlist(long id);

        Task<BaseResponse<bool>> DeleteFromWishlist(long id);

        Task<BaseResponse<Wishlist>> EditWishlist(long id, Wishlist wishlist);
    }
}
