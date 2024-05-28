using Automarket.Domain.Entity;
using Automarket.Domain.Responce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Interfaces
{
    public interface IBasketService
    {
        Task<BaseResponse<List<Consumable>>> GetBasket();

        Task<BaseResponse<bool>> AddToBasket(long id);

        Task<BaseResponse<bool>> DeleteFromBasket(long id);

        Task<BaseResponse<bool>> Plus(long id);

        Task<BaseResponse<bool>> Minus(long id);

        Task<BaseResponse<int>> GetQuantity(long id);

        Task<BaseResponse<double>> GetTotalPrice();
    }
}
