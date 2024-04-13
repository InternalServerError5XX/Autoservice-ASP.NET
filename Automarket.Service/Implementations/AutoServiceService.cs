using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Helpers;
using Automarket.Domain.Responce;
using Automarket.Domain.ViewModels.Order;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Implementations
{
    public class AutoServiceService : IAutoServiceService
    {
        private readonly IBaseRepository<Appointment> _appointmentRepository;
        private readonly IBaseRepository<User> _userRepository;
        private readonly IAccountService _accountService;

        public AutoServiceService (IBaseRepository<Appointment> appointmentRepository, IBaseRepository<User> userRepository, 
            IAccountService accountService)
        {
            _appointmentRepository = appointmentRepository;
            _userRepository = userRepository;
            _accountService = accountService;
        }

        public async Task<BaseResponse<List<Appointment>>> GetAppointments()
        {
            try
            {
                var appointments = await _appointmentRepository.GetAll().ToListAsync();

                return new BaseResponse<List<Appointment>>()
                {
                    Data = appointments,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Appointment>>()
                {
                    Description = $"[GetAppointments] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<AppointmentViewModel>> GetAppointment(long id)
        {
            try
            {
                var appointment = await _appointmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (appointment == null)
                {
                    return new BaseResponse<AppointmentViewModel>()
                    {
                        Description = "Appointment not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var data = new AppointmentViewModel
                {
                    Id = appointment.Id,
                    UserId = appointment.UserId,
                    Name = appointment.Name,
                    PhoneNumber = appointment.PhoneNumber,
                    CallBack = appointment.CallBack,
                    Description = appointment.Description,
                    AppointmentDate = appointment.AppointmentDate,
                    CreationDate = appointment.CreationDate,
                    Checked = appointment.Checked,
                };

                return new BaseResponse<AppointmentViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AppointmentViewModel>()
                {
                    Description = $"[GetAppointment] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }       

        public async Task<BaseResponse<Appointment>> CreateAppointment(AppointmentViewModel appointment, User user)
        {
            try
            {
                var _appointment = await _appointmentRepository.GetAll()
                    .FirstOrDefaultAsync(x => x.AppointmentDate == appointment.AppointmentDate);
                var response = await _accountService.GetIdByEmail();
                var userId = response.Data;

                if (_appointment != null)
                {
                    return new BaseResponse<Appointment>
                    {
                        Description = "An appointment for this date already exists"
                    };
                }

                var newAppointment = new Appointment
                {
                    UserId = userId,
                    Name = appointment.Name,
                    PhoneNumber = appointment.PhoneNumber,
                    CallBack = appointment.CallBack,
                    Description = appointment.Description,
                    AppointmentDate = appointment.AppointmentDate,
                    CreationDate = DateTime.Now,
                    EditDate = DateTime.Now,
                };

                await _appointmentRepository.Create(newAppointment);

                return new BaseResponse<Appointment>
                {
                    Data = newAppointment,
                    Description = "Appointment created",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Appointment>()
                {
                    Description = $"[CreateAppointment] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteAppointment(long id)
        {
            try
            {
                var appointment = await _appointmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (appointment == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Item not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                await _appointmentRepository.Delete(appointment);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = "Appointment deleted",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteAppointment] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<Appointment>> EditAppointment(long id, AppointmentViewModel model)
        {
            try
            {
                var appointment = await _appointmentRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (appointment == null)
                {
                    return new BaseResponse<Appointment>
                    {
                        Description = "Appointment not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                appointment.Name = model.Name;
                appointment.PhoneNumber = model.PhoneNumber;
                appointment.CallBack = model.CallBack;
                appointment.Description = model.Description;
                appointment.AppointmentDate = model.AppointmentDate;
                appointment.EditDate = DateTime.Now;
                appointment.Checked = model.Checked;

                await _appointmentRepository.Update(appointment);

                return new BaseResponse<Appointment>
                {
                    Data = appointment,
                    Description = "Appointment updated",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Appointment>()
                {
                    Description = $"[EditAppointment] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }       
    }
}
