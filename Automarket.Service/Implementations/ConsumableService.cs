using Automarket.DAL.Interfaces;
using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Responce;
using Automarket.Domain.ViewModels.ConsumableViewModel;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Automarket.Service.Implementations
{
    public class ConsumableService : IConsumableService
    {
        private readonly IBaseRepository<Consumable> _consumableRepository;

        public ConsumableService(IBaseRepository<Consumable> consumableRepository)
        {
            _consumableRepository = consumableRepository;
        }

        public async Task<BaseResponse<Consumable>> CreateItem(ConsumableViewModel model)
        {
            try
            {
                var item = await _consumableRepository.GetAll().FirstOrDefaultAsync(x => x.Brand == model.Brand &&
                    x.Model == model.Model && x.Year == model.Year && x.Country == model.Country);

                if (item != null)
                {
                    return new BaseResponse<Consumable>
                    {
                        Description = "Item is already exist"
                    };
                }

                var photoPath = await SavePhotoAsync(model.Photo, "img/items");

                var newItem = new Consumable
                {
                    ConsumableType = model.ConsumableType,
                    Subcategory = model.Subcategory,
                    Brand = model.Brand,
                    Model = model.Model,
                    Country = model.Country,
                    Year = model.Year,
                    Price = model.Price,
                    Quantity = model.Quantity,
                    CreationDate = DateTime.Now,
                    EditDate = DateTime.Now,
                    PhotoPath = photoPath,
                };

                await _consumableRepository.Create(newItem);

                return new BaseResponse<Consumable>
                {
                    Data = newItem,
                    Description = "Item created",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Consumable>()
                {
                    Description = $"[CreateItem] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteItem(long id)
        {
            try
            {
                var item = await _consumableRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (item == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "Item not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                string relativePhotoPath = Path.Combine("wwwroot", item.PhotoPath.TrimStart('\\', '/'));
                string absolutePhotoPath = Path.Combine(Directory.GetCurrentDirectory(), relativePhotoPath);                

                await _consumableRepository.Delete(item);

                if (File.Exists(absolutePhotoPath))
                {
                    File.Delete(absolutePhotoPath);
                }

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
                    Description = $"[DeleteItem] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }


        public async Task<BaseResponse<Consumable>> EditItem(long id, ConsumableViewModel model)
        {
            try
            {
                var item = await _consumableRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (item == null)
                {
                    return new BaseResponse<Consumable>()
                    {
                        Description = "Item not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                item.ConsumableType = model.ConsumableType;
                item.Subcategory = model.Subcategory;
                item.Brand = model.Brand;
                item.Model = model.Model;
                item.Country = model.Country;
                item.Year = model.Year;
                item.Price = model.Price;
                item.Quantity = model.Quantity;
                item.EditDate = DateTime.Now;

                if (model.Photo != null)
                {
                    var newPhotoPath = await SavePhotoAsync(model.Photo, "img/items");

                    if (!string.IsNullOrEmpty(item.PhotoPath))
                    {
                        string relativePhotoPath = Path.Combine("wwwroot", item.PhotoPath.TrimStart('\\', '/'));
                        string absolutePhotoPath = Path.Combine(Directory.GetCurrentDirectory(), relativePhotoPath);

                        if (File.Exists(absolutePhotoPath))
                        {
                            File.Delete(absolutePhotoPath);
                        }
                    }

                    item.PhotoPath = newPhotoPath;
                }

                await _consumableRepository.Update(item);

                return new BaseResponse<Consumable>()
                {
                    Description = "Item updated",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Consumable>()
                {
                    Description = $"[EditItem] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ConsumableViewModel>> GetItem(long id)
        {
            try
            {
                var item = await _consumableRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);

                if (item == null)
                {
                    return new BaseResponse<ConsumableViewModel>()
                    {
                        Description = "Item not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var data = new ConsumableViewModel()
                {
                    Id = item.Id,
                    ConsumableType = item.ConsumableType,
                    Subcategory = item.Subcategory,
                    Brand = item.Brand,
                    Model = item.Model,
                    Country = item.Country,
                    Year = item.Year,
                    Price = item.Price,
                    Quantity= item.Quantity,
                    PhotoPath = item.PhotoPath
                };

                return new BaseResponse<ConsumableViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ConsumableViewModel>()
                {
                    Description = $"[GetItem] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<List<Consumable>>> GetItems()
        {
            try
            {
                var items = await _consumableRepository.GetAll().ToListAsync();

                return new BaseResponse<List<Consumable>>()
                {
                    Data = items,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Consumable>>()
                {
                    Description = $"[GetItems] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
        public async Task<string> SavePhotoAsync(IFormFile photo, string relativeFolderPath)
        {
            string uniqueFileName = Guid.NewGuid().ToString();

            string absoluteFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", relativeFolderPath);

            if (!Directory.Exists(absoluteFolderPath))
            {
                Directory.CreateDirectory(absoluteFolderPath);
            }

            string fileExtension = Path.GetExtension(photo.FileName);

            string filePath = Path.Combine(absoluteFolderPath, uniqueFileName + fileExtension);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await photo.CopyToAsync(fileStream);
            }

            string relativeHtmlPath = Path.Combine("/", relativeFolderPath, uniqueFileName + fileExtension)
                                            .Replace(Path.DirectorySeparatorChar, '/');

            return relativeHtmlPath;
        }


    }
}
