using Automarket.Domain.Entity;
using Automarket.Domain.Responce;
using Automarket.Domain.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Automarket.Service.Interfaces
{
    public interface IAccountService
    {
        Task<BaseResponse<ClaimsIdentity>> Signup(SignupViewModel model);

        Task<BaseResponse<ClaimsIdentity>> Login(LoginViewModel model);      

        Task<BaseResponse<bool>> ChangePassword(ChangePasswordViewModel model);

        Task<BaseResponse<AccountViewModel>> GetProfile(long id);

        Task<BaseResponse<AccountViewModel>> GetProfile(string email);

        Task<IBaseResponse<List<User>>> GetUsers();

        Task<BaseResponse<long>> GetIdByEmail();

        Task<BaseResponse<User>> CreateAccount(AccountViewModel user);

        Task<BaseResponse<User>> EditAccount(long id, AccountViewModel model);

        Task<BaseResponse<bool>> DeleteAccount(long id);

        Task<string> GetUserEmail();
    }
}
