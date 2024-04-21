using Automarket.Domain.Entity;
using Automarket.Domain.Responce;
using Automarket.Domain.ViewModels.AutoService;
using Automarket.Domain.ViewModels.Order;

namespace Automarket.Service.Interfaces
{
    public interface IMaintenanceService
    {
        Task<BaseResponse<MaintenanceViewModel>> GetMaintenance(long id);

        Task<BaseResponse<List<Maintenance>>> GetMaintenances();

        Task<BaseResponse<List<Maintenance>>> GetMaintenances(long id);

        Task<BaseResponse<Maintenance>> CreateMaintenance(MaintenanceViewModel maintenance);

        Task<BaseResponse<bool>> DeleteMaintenance(long id);

        Task<BaseResponse<Maintenance>> EditMaintenance(long id, MaintenanceViewModel maintenance);
    }
}
