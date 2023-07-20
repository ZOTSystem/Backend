﻿using be.DTOs;
using be.Models;

namespace be.Services.UserService
{
    public interface IUserService
    {
        #region HUY - LOGIN/REGISTER/FORGOR PASSWORD/GETINFOR/ UPDATE USER/CHANGE PASSWORD
        object Login(string email, string password, IConfiguration config);
        object Register(Register register);
        Task<object> ForgotPassword(string email);
        Task<object> GetInfo(string token);
        object UpdateUser(UpdateUser user);
        Task<object> ChangePassword(int accountId, string newPassword);
        object LoginByGoogle(Register register, IConfiguration config);
        object ChangeAvatar(int accountId, string newAvatar);
        public Task<object> SearchInforByEmail(string email);

        #endregion

        #region - MANAGE USER
        public object GetAllAccountUser();
        public object UpdateAccountUser();
        public object GetAccountUserById();
        object ChangeStatus(int accountId, string status);

        #endregion
    }
}
