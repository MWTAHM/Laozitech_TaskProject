using System;
using System.Collections.Generic;
using System.Text;
using Core.DTO;
using Core.Models;
using DAL.Project;

namespace BLL.Project
{
    public class UserController
    {
        public static bool Register(UserModel user)
        {
            return UserService.InsertUser(user);
        }

        public static bool DeleteUser(string userId)
        {
            return UserService.DeleteUser(userId);
        }

        public static bool DeleteUser(UserModel user)
        {
            return UserService.DeleteUser(user.UserId);
        }

        public static bool UpdateProject(UserModel user)
        {
            return UserService.UpdateUser(user);
        }

        public static string GetUserName(string Id)
        {
            return UserService.GetUserName(Id);
        }

        public static List<UserModel> GetAllUsers()
        {
            return UserService.GetAllUsers();
        }

        public static List<DTO_UserInfo_DropDown> GetAllUsersDropDownInfo()
        {
            return UserService.GetAllUsersDropDownInfo();
        }

        public static UserModel GetUserById(string Id)
        {
            return UserService.GetUserById(Id);
        }
    }
}
