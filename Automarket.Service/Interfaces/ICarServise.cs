using Automarket.Domain.Entity;
using Automarket.Domain.Responce;
using Automarket.Domain.ViewModels.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Interfaces
{
    public interface ICarService
    {
        BaseResponse<Dictionary<int, string>> GetTypes();

        Task<IBaseResponse<List<Car>>> GetCars();

        //Task<IBaseResponse<List<Car>>> GetCars(string request);

        Task<IBaseResponse<CarViewModel>> GetCar(long id);

        Task<IBaseResponse<Car>> CreateCar(CarViewModel car);

        Task<IBaseResponse<bool>> DeleteCar(long id);

        Task<IBaseResponse<Car>> Edit(long id, CarViewModel model);
    }
}
