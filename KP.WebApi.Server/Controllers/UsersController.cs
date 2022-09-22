using KP.Common.Model.Models;
using KP.Services.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace KP.Web.Api.Controllers
{
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [AllowAnonymous]
        [Route("/api/Login")]
        [HttpPost]
        public bool Login([FromBody] string base64String)
        {
            var bytes = Convert.FromBase64String(base64String);
            string credentials = Encoding.UTF8.GetString(bytes);
            if (!string.IsNullOrEmpty(credentials))
            {
                string[] array = credentials.Split(":");
                string username = array[0];
                string password = array[1];

                var user = userService.GetUserByUsername(username);
                if (user == null)
                {
                    return false;
                }


                if (!userService.PasswordVerify(user.Username, password))
                {
                    return false;
                }
            }
            return true;
        }

        [Route("/api/Users")]
        [HttpGet]
        public IEnumerable<UserModel> GetUsers()
        {
            try
            {
                var users = userService.GetUsers();

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        //[Route("/api/Users/{UserId}")]
        //[HttpGet]
        //public UserModel GetUserById(Guid userId)
        //{
        //    try
        //    {
        //        var user = userService.GetUserById(userId);

        //        return user;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message.ToString());
        //    }
        //}

        [Route("/api/Users/{username}")]
        [HttpGet]
        public UserModel GetUserByUsername(string username)
        {
            try
            {
                var user = userService.GetUserByUsername(username);

                return user;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Users/{UserId}")]
        [HttpDelete]
        public void DeleteUser(Guid userId)
        {
            try
            {
                userService.DeleteUser(userId);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Users")]
        [HttpPost]
        public UserModel Create([FromBody] UserModel user)
        {
            try
            {
                //var studentModel = JsonSerializer.Deserialize<StudentModel>(student);
                UserModel createdUser = userService.CreateUser(user);
                return createdUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        [Route("/api/Users/{userId}")]
        [HttpPut]
        public UserModel UpdateUser(Guid userId, [FromBody] UserModel user)
        {
            try
            {
                UserModel updateUser = userService.UpdateUser(user);
                return updateUser;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }
    }
}
