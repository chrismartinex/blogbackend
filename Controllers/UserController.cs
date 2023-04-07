using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogbackend.Models;
using blogbackend.Models.DTO;
using blogbackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace blogbackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _data;
        public UserController(UserService dataFromService)
        {
            _data = dataFromService;
        }
        //login 
        [HttpPost]
        [Route("Login")]
        public IActionResult login([FromBody] LoginDTO User){
            return _data.login(User);
        }



        //add a user endpoint
          //c for crud

          [HttpPost("AddUser")]
        //   [Route("AddUser")]

        public bool AddUser(CreateAccountDTO userToAdd)
        {
            return _data.AddUser(userToAdd);
            }

        
        //update user account
        [HttpPost]
        [Route("UpdateUser")]
        public bool UpdateUser(UserModel userToUpdate){
            return _data.UpdateUser(userToUpdate);
        }


        [HttpPost]
        [Route("UpdateUser/{id}/{username}")]
        public bool UpdateUser(int id, string username){
            return _data.UpdateUsername(id, username);
        }


        //delete user account
        [HttpDelete]
        [Route("DeleteUser/{userToDelete}")]
        public bool DeleteUser(string userToDelete){
            return _data.DeleteUser(userToDelete);
        }

        [HttpGet]
        [Route("UserByUsername/{username}")]

        public UserIdDTO GetUserByusername(string username){
    return _data.GetUserIdDTO(username);
        }
       
    }
}