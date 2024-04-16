using Automarket.DAL.Interfaces;
using Automarket.DAL.Repositories;
using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Responce;
using Automarket.Domain.ViewModels.AutoService;
using Automarket.Domain.ViewModels.Order;
using Automarket.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Implementations
{
    public class MaintenanceService : IMaintenanceService
    {
        private readonly IBaseRepository<Maintenance> _maintenanceRepository;
        private readonly IBaseRepository<User> _userRepository;

        public MaintenanceService(IBaseRepository<Maintenance> maintenanceRepository, IBaseRepository<User> userRepository)
        {
            _maintenanceRepository = maintenanceRepository;
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<Maintenance>> CreateMaintenance(MaintenanceViewModel maintenance, long? id)
        {
            try
            {
                var newMaintenance = new Maintenance
                {
                    AppointmentId = (Int64)id,
                    Stage = maintenance.Stage,
                    CreationDate = maintenance.CreationDate,
                    EditDate = maintenance.EditDate,
                    CompletionTime = maintenance.CompletionTime,
                };

                await _maintenanceRepository.Create(newMaintenance);

                return new BaseResponse<Maintenance>
                {
                    Data = newMaintenance,
                    Description = "Maintenance created",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Maintenance>()
                {
                    Description = $"[CreateMaintenance] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteMaintenance(long id)
        {
            try
            {
                var maintenance = await _maintenanceRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Id == id);

                if (maintenance == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Item not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                await _maintenanceRepository.Delete(maintenance);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Maintenance deleted",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteMaintenance] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Maintenance>> EditMaintenance(long id, MaintenanceViewModel model)
        {
            try
            {
                var maintenance = await _maintenanceRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Id == id);

                if (maintenance == null)
                {
                    return new BaseResponse<Maintenance>
                    {
                        Description = "Maintenance not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                maintenance.AppointmentId = model.AppointmentId;
                maintenance.Stage = model.Stage;
                maintenance.CreationDate = model.CreationDate;
                maintenance.EditDate = DateTime.UtcNow;
                maintenance.CompletionTime = model.CompletionTime;

                await _maintenanceRepository.Update(maintenance);

                return new BaseResponse<Maintenance>
                {
                    Data = maintenance,
                    Description = "Maintenance updated",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Maintenance>()
                {
                    Description = $"[EditMaintenance] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<Maintenance>>> GetMaintenances()
        {
            try
            {
                var maintenances = await _maintenanceRepository.GetAll().Result.ToListAsync();

                return new BaseResponse<List<Maintenance>>()
                {
                    Data = maintenances,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Maintenance>>()
                {
                    Description = $"[GetMaintenances] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<Maintenance>>> GetMaintenances(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Id == id);
                if (user == null)
                {
                    return new BaseResponse<List<Maintenance>>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var maintenances = await _maintenanceRepository.GetAll().Result.Where(x => x.UserId == id).ToListAsync();

                return new BaseResponse<List<Maintenance>>()
                {
                    Data = maintenances,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Maintenance>>()
                {
                    Description = $"[GetMaintenances] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<MaintenanceViewModel>> GetMaintenance(long id)
        {
            try
            {
                var maintenance = await _maintenanceRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Id == id);

                if (maintenance == null)
                {
                    return new BaseResponse<MaintenanceViewModel>()
                    {
                        Description = "Maintenance not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var data = new MaintenanceViewModel
                {
                    Id = maintenance.Id,
                    AppointmentId = maintenance.AppointmentId,
                    Stage = maintenance.Stage,
                    CreationDate = maintenance.CreationDate,
                    EditDate = maintenance.EditDate,
                    CompletionTime = maintenance.CompletionTime,
                };

                return new BaseResponse<MaintenanceViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<MaintenanceViewModel>()
                {
                    Description = $"[GetMaintenance] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
