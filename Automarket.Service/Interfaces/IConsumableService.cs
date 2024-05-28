using Automarket.Domain.Entity;
using Automarket.Domain.Responce;
using Automarket.Domain.ViewModels.ConsumableViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Interfaces
{
    public interface IConsumableService
    {
        Task<BaseResponse<ConsumableViewModel>> GetItem(long id);

        Task<BaseResponse<List<Consumable>>> GetItems();

        Task<BaseResponse<Consumable>> CreateItem(ConsumableViewModel model);

        Task<BaseResponse<bool>> DeleteItem(long id);
        
        Task<BaseResponse<Consumable>> EditItem(long id, ConsumableViewModel model);

        Task<BaseResponse<int>> GetQuantity(long id);
    }
}
