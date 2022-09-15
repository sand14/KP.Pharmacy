﻿using KP.Common.Model.Automapper;
using KP.Common.Model.Models;
using KP.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
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
