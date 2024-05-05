using Automarket.DAL.Interfaces;
using Automarket.DAL.Repositories;
using Automarket.Domain.Entity;
using Automarket.Domain.Enum;
using Automarket.Domain.Helpers;
using Automarket.Domain.Responce;
using Automarket.Domain.ViewModels.Account;
using Automarket.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IBaseRepository<User> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountService (IBaseRepository<User> userRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userRepository = userRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsIdentity Authenticate (User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString()),
            };
            return new ClaimsIdentity(claims, "ApplicationCookie", 
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
        }

        public async Task<BaseResponse<ClaimsIdentity>> Signup(SignupViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user != null)
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        Description = "User is already exist",
                    };
                }
                user = new User()
                {
                    Email = model.Email,
                    Name = model.Name,
                    Password = HashPasswordHelper.HashPassword(model.Password),
                    Role = Role.User,
                    CreationDate = DateTime.UtcNow,
                };

                await _userRepository.Create(user);
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>
                {
                    Data = result,
                    Description = "User added",
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[Signup] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user == null)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "User does not exist",
                    };
                }
                
                if (user.Password != HashPasswordHelper.HashPassword(model.Password)
                    || user.Email != model.Email)
                {
                    return new BaseResponse<ClaimsIdentity>()
                    {
                        Description = "Invalid email or password",
                    };
                }

                user.LastLogin = DateTime.UtcNow;
                var result = Authenticate(user);

                return new BaseResponse<ClaimsIdentity>()
                {
                    Data = result,
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<ClaimsIdentity>()
                {
                    Description = $"[Login] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }       

        public async Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user == null)
                {
                    return new BaseResponse<bool>
                    {
                        Description = "User does not exist",
                        StatusCode = StatusCode.NotFound,
                    };
                }

                user.Password = HashPasswordHelper.HashPassword(model.NewPassword);
                await _userRepository.Update(user);

                return new BaseResponse<bool>
                {
                    Data = true,
                    Description = "Data updated",
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[ChangePassword] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<AccountViewModel>> GetProfile(long id)
        {
            try
            {
                var profile = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Id == id);

                if (profile == null)
                {
                    return new BaseResponse<AccountViewModel>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var data = new AccountViewModel()
                {
                    Id = profile.Id,
                    Email = profile.Email,
                    Password = profile.Password,
                    Username = profile.Username,
                    Name = profile.Name,
                    Lastname = profile.Lastname,
                    Age = profile.Age,
                    Role = profile.Role,
                };

                return new BaseResponse<AccountViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AccountViewModel>()
                {
                    Description = $"[GetProfile] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<AccountViewModel>> GetProfile(string email)
        {
            try
            {
                var profile = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Email == email);

                if (profile == null)
                {
                    return new BaseResponse<AccountViewModel>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                var data = new AccountViewModel()
                {
                    Id = profile.Id,
                    Email = profile.Email,
                    Password = profile.Password,
                    Username = profile.Username,
                    Name = profile.Name,
                    Lastname = profile.Lastname,
                    Age = profile.Age,
                    Role = profile.Role,
                };

                return new BaseResponse<AccountViewModel>()
                {
                    Data = data,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<AccountViewModel>()
                {
                    Description = $"[GetProfile] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<User>>> GetUsers()
        {
            try
            {
                var users = await _userRepository.GetAll().Result.ToListAsync();

                return new BaseResponse<List<User>>()
                {
                    Data = users,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<User>>()
                {
                    Description = $"[GetUsers] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<long>> GetIdByEmail()
        {
            try
            {
                var email = GetUserEmail().Result; 
                var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Email == email.ToString());

                if (user == null)
                {
                    return new BaseResponse<long>()
                    {
                        Description = "Not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                long userId = user.Id;
                return new BaseResponse<long>()
                {
                    Data = userId,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<long>()
                {
                    Description = $"[GetIdByEmail] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<User>> CreateAccount(AccountViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Email == model.Email);
                if (user != null)
                {
                    return new BaseResponse<User>
                    {
                        Description = "User is already exist",
                    };
                }

                var newUser = new User()
                {
                    Email = model.Email,
                    Password = HashPasswordHelper.HashPassword(model.Password),
                    Username = model.Username,
                    Name = model.Name,
                    Lastname = model.Lastname,
                    Age = model.Age,
                    Role = model.Role,
                };

                await _userRepository.Create(newUser);

                return new BaseResponse<User>()
                {
                    Data = newUser,
                    Description = $"Account created",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[GetProfile] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<User>> EditAccount(long id, AccountViewModel model)
        {
            try
            {
                var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    return new BaseResponse<User>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                user.Email = model.Email;
                user.Username = model.Username;
                user.Name = model.Name;
                user.Lastname = model.Lastname;
                user.Age = model.Age;
                user.Role = model.Role;

                await _userRepository.Update(user);

                return new BaseResponse<User>()
                {
                    Data = user,
                    Description = $"Profile updated",
                    StatusCode = StatusCode.OK,
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    Description = $"[Edit] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<BaseResponse<bool>> DeleteAccount(long id)
        {
            try
            {
                var user = await _userRepository.GetAll().Result.FirstOrDefaultAsync(x => x.Id == id);

                if (user == null)
                {
                    return new BaseResponse<bool>()
                    {
                        Description = "User not found",
                        StatusCode = StatusCode.NotFound
                    };
                }

                await _userRepository.Delete(user);

                return new BaseResponse<bool>()
                {
                    Data = true,
                    Description = $"Account deleted",
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<bool>()
                {
                    Description = $"[DeleteAccount] : {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<string> GetUserEmail()
        {
            try
            {
                if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = _httpContextAccessor.HttpContext.User;
                    var UserEmail = user.Identity.Name;

                    if (UserEmail != null)
                    {
                        return await Task.FromResult(UserEmail);
                    }
                }

                return await Task.FromResult("0");
            }
            catch (Exception ex)
            {
                return await Task.FromResult(ex.Message);
            }
        }
    }
}
