using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Extensions;
using Automarket.Domain.Responce;
using Automarket.Domain.ViewModels.Car;
using Automarket.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Automarket.Service.Implementations
{
    public class CarService : ICarService
    {
        private readonly IBaseRepository<Car> _carRepository;

        public CarService(IBaseRepository<Car> carRepository)
        {
            _carRepository = carRepository;
        }

        public BaseResponse<Dictionary<int, string>> GetTypes()
        {
            try
            {
                var types = ((CarType[])Enum.GetValues(typeof(CarType)))
                    .ToDictionary(k => (int)k, t => t.GetDisplayName());

                return new BaseResponse<Dictionary<int, string>>()
                {
                    Data = types,   
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Dictionary<int, string>>()
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<CarViewModel>> GetCar(long id)
        {
            try
            {
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (car == null)
                {
                    return new BaseResponse<CarViewModel>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var data = new CarViewModel()
                {
                    Id = id,
                    Date = car.Date,
                    Description = car.Description,
                    Name = car.Name,
                    Price = car.Price,
                    CarType = car.CarType.GetDisplayName(),
                    Speed = car.Speed,
                    Model = car.Model,

                };

                return new BaseResponse<CarViewModel>()
                {
                    StatusCode = StatusCode.OK,
                    Data = data
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<CarViewModel>()
                {
                    Description = $"[GetCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<Car>>> GetCars()
        {            
            try
            {
                var cars = await _carRepository.GetAll().ToListAsync();

                return new BaseResponse<List<Car>>()
                {
                    Data = cars,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Car>>()
                {
                    Description = $"[GetCars] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        //public async Task<IBaseResponse<List<Car>>> GetCars(string request)
        //{
        //    try
        //    {   
        //        var cars = await _carRepository.GetAll()
        //            .Where(x => (x.Model.Contains(request) || 
        //            x.Name.Contains(request) ||
        //            x.Id.ToString().Contains(request)))
        //            .ToListAsync();


        //        if (!cars.Any())
        //        {
        //            return new BaseResponse<List<Car>>()
        //            {
        //                Description = "0 elements found",
        //                StatusCode = StatusCode.OK
        //            };
        //        }

        //        return new BaseResponse<List<Car>>()
        //        {
        //            Data = cars,
        //            StatusCode = StatusCode.OK
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        return new BaseResponse<List<Car>>()
        //        {
        //            Description = $"[GetCars] : {ex.Message}",
        //            StatusCode = StatusCode.InternalServerError
        //        };
        //    }
        //}

        public async Task<IBaseResponse<Car>> CreateCar(CarViewModel model)
        {          
            try
            {
                var car = new Car()
                {
                    Name = model.Name,
                    Model = model.Model,
                    Description = model.Description,
                    Date = DateTime.Now,
                    Speed = model.Speed,
                    CarType = (CarType)Convert.ToInt32(model.CarType),
                    Price = model.Price,

                };
                await _carRepository.Create(car);

                return new BaseResponse<Car>()
                {
                    Data = car,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[CreateItem] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<bool>> DeleteCar(long id)
        {
            try
            {
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (car == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.NotFound,
                        Data = false
                    };
                }

                await _carRepository.Delete(car);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = $"Item (Id - {id}) deleted",
                    StatusCode = StatusCode.OK
                };

            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteCar] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<Car>> Edit(long id, CarViewModel model)
        {
            try
            {
                var car = await _carRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (car == null)
                {
                    return new BaseResponse<Car>()
                    {
                        Description = "Car not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                car.Name = model.Name;
                car.Model = model.Model;
                car.Description = model.Description;
                car.Speed = model.Speed;
                car.Date = model.Date;
                car.Price = model.Price;

                await _carRepository.Update(car);

                return new BaseResponse<Car>()
                {
                    Data = car,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Car>()
                {
                    Description = $"[EditItem] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
