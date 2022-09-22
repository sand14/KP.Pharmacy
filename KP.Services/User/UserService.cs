using KP.Common.Model.Automapper;
using KP.Common.Model.Models;
using KP.Core.Data;

namespace KP.Services.User
{
    public class UserService : IUserService
    {
        private readonly IRepository<Core.DomainModels.User> userRepository;

        public UserService(IRepository<Core.DomainModels.User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public UserModel CreateUser(UserModel User)
        {
            if (User == null) return null;

            KP.Core.DomainModels.User userEntity = User.ToEntity();
            userEntity.Password = PasswordHashing(userEntity.Password);
            userRepository.Insert(userEntity);

            UserModel createdUser = GetUserById(userEntity.UserId);
            return createdUser;
        }

        public void DeleteUser(Guid UserId)
        {
            var databaseEntity = userRepository.Table.FirstOrDefault(x => x.UserId == UserId);
            if (databaseEntity == null) return;
            userRepository.Delete(databaseEntity);
        }

        public UserModel GetUserById(Guid UserId)
        {
            var user = userRepository.Table.FirstOrDefault(x => x.UserId == UserId);
            return user.ToModel();
        }

        public IEnumerable<UserModel> GetUsers()
        {
            var users = userRepository.Table.Select(x => x.ToModel()).ToList();
            return users;
        }

        public UserModel UpdateUser(UserModel User)
        {
            if (User == null) return null;
            var databaseEntity = userRepository.TableNoTracking.FirstOrDefault(s => s.UserId == User.UserId);
            if (databaseEntity == null) return null;
            if (!User.Password.StartsWith("$2a$11$"))
            {
                var decodedpassword = User.Password;
                User.Password = PasswordHashing(decodedpassword);
            }


            userRepository.Update(User.ToEntity());

            return GetUserById(User.UserId);
        }

        public UserModel GetUserByUsername(string Name)
        {
            var user = userRepository.Table.FirstOrDefault(x => x.Username == Name);
            return user.ToModel();
        }

        public string PasswordHashing(string password)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);
            return passwordHash;
        }

        public bool PasswordVerify(string username, string password)
        {
            var userEntity = userRepository.Table.FirstOrDefault(x => x.Username == username);
            string passwordHash = userEntity.Password;
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }
    }
}
