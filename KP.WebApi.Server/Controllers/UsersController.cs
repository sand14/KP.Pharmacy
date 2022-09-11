using KP.Common.Model.Models;
using KP.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KP.Web.Api.Controllers
{
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        [Route("/api/Login/{basictoken}")]
        [HttpGet]
        public bool Login( )
        {
            return false;
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

        [Route("/api/Users/{UserId}")]
        [HttpGet]
        public UserModel GetUserById(Guid userId)
        {
            try
            {
                var user = userService.GetUserById(userId);

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
    }
}
