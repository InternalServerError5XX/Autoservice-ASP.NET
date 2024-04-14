using Automarket.Domain.Entity;
using Automarket.Domain.Responce;
using Automarket.Domain.ViewModels.ConsumableViewModel;
using Automarket.Domain.ViewModels.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Interfaces
{
    public interface IAppointmentService
    {
        Task<BaseResponse<AppointmentViewModel>> GetAppointment(long id);

        Task<BaseResponse<List<Appointment>>> GetAppointments();

        Task<BaseResponse<Appointment>> CreateAppointment(AppointmentViewModel appointment, User user);

        Task<BaseResponse<bool>> DeleteAppointment(long id);

        Task<BaseResponse<Appointment>> EditAppointment(long id, AppointmentViewModel model);
    }
}
