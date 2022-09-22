using KP.Common.Model.Models;

namespace KP.Services.User
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetUsers();
        UserModel GetUserById(Guid UserId);
        void DeleteUser(Guid UserId);
        UserModel GetUserByUsername(string Name);
        UserModel CreateUser(UserModel User);
        UserModel UpdateUser(UserModel User);
        string PasswordHashing(string password);
        bool PasswordVerify(string username, string password);
    }
}
