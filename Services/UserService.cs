using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogbackend.Models;
using blogbackend.Models.DTO;
using blogbackend.Services.Context;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace blogbackend.Services
{
    public class UserService : ControllerBase
    {
        private readonly DataContext _context;
        private object UserToAdd;

        public UserService(DataContext context){
            _context = context;
        }

        public bool DoesUserExist (string? Username)
        {
            //check the table to see if the user name exist
            //if 1 item matches teh condition, retrun the item
            // if no item matches the condition, return null
            //if multiple item matches, an error will occur

            return _context.UserInfo.SingleOrDefault(user => user.Username == Username ) != null;
                // if something returns true, it will return an object
                // but the fucntion needs a boolean not an object
                // object != null, true
        
        }

    public bool AddUser(CreateAccountDTO userToAdd)
        {
            bool result = false;
            if(!DoesUserExist(userToAdd.Username))
            {
                //the user does not exist
                //creating a new instance of user model (empty object)
                UserModel newUser = new UserModel();
                //create our salt and hash password
                var hashPassword = HashPassword(userToAdd.Password);
                newUser.id = userToAdd.id;
                newUser.Username = userToAdd.Username;
                newUser.Salt = hashPassword.Salt;
                newUser.Hash = hashPassword.Hash;

                // adding newUser to our database
                _context.Add(newUser);
                //this saves to our database and returns the number of entries that was written to the database
                // _context.SaveChanges();
                result = _context.SaveChanges() != 0;
            }
            return result;
            //if the user already exists
            //if they do not exist we can then have account created
            // else throw a false
        }

        private object HashPassword(object password)
        {
            throw new NotImplementedException();
        }

        public PasswordDTO HashPassword(string password)
        {
            PasswordDTO newHashedPassword = new PasswordDTO();
            //new byte array that is length of 64
            byte[] SaltByte = new byte[64];
            var provider = new RNGCryptoServiceProvider();
            //enhanced rng of numbers without using zero
            provider.GetNonZeroBytes(SaltByte);
            //endoing the 64 digits to string
            // salt makes teh hash unique to the user
            //if we only had a has password, every hash password woul be the same
            var Salt = Convert.ToBase64String(SaltByte);

            //ten thousand iterations 
            Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, SaltByte, 10000);

            //encoding our password with Salt
            //if anyone would to brute force this, it would take a decades
            var Hash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            newHashedPassword.Salt = Salt;
            newHashedPassword.Hash = Hash;

            return newHashedPassword;
        }

        public bool VerifyUserPassword(string? Password, string? storedHash, string? storedSalt)
        {
            // get our existing salt, and change it to base 64 string
            var SaltBytes = Convert.FromBase64String(storedSalt);
            //making a password that the user inputed, and using the stored salt
            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(Password, SaltBytes, 10000);

            var newHash = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            return newHash == storedHash;
        }
        public IActionResult login(LoginDTO User){
            //want to return an error code if the user does not have a valid username or password
            IActionResult Result = Unauthorized();

            //check to see if the user exist
            if(DoesUserExist(User.Username)){
                    //true
                    //we want to store the user object
                    //to create another helper function
                    var foundUser = GetUserByUsername(User.Username);
                    //check if the password is correct
                    if(VerifyUserPassword(User.Password, foundUser.Hash, foundUser.Salt)){
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    var tokeOptions = new JwtSecurityToken(
                        issuer: "http://localhost:5000",
                        audience: "http://localhost:5000",
                        claims: new List<Claim>(),
                        expires: DateTime.Now.AddMinutes(30),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                    Result = Ok(new { Token = tokenString });
                    }
            }
            return Result;
        }
        public UserModel GetUserByUsername(string? username){
            return _context.UserInfo.SingleOrDefault(user => user.Username == username);
        }
        public bool UpdateUser(UserModel userToUpdate){
            //This one is sending over the whole object to the updated
            _context.Update<UserModel>(userToUpdate);
            return  _context.SaveChanges() != 0;
        }
        public bool UpdateUsername(int id, string username){
            //This is sending over just the id and the username
            //we have to get the object to then be updated
            UserModel foundUser = GetUserById(id);
            bool result = false;
            if(foundUser != null){
                //this means a user was found

                foundUser.Username = username;
                _context.Update<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public UserModel GetUserById(int id){
            return _context.UserInfo.SingleOrDefault(user => user.id == id);

        }

        public bool DeleteUser(string userToDelete){
                //this is sending over a username
                // we have to get eh object to be deleted
                UserModel foundUser = GetUserByUsername(userToDelete);
                bool result = false;
            if (foundUser != null){
                //A user was found
                _context.Remove<UserModel>(foundUser);
                result = _context.SaveChanges() != 0;
            }
            return result;
        }

        public UserIdDTO GetUserIdDTO (string username) {
            var userInfo = new UserIdDTO();
            var foundUser = _context.UserInfo.SingleOrDefault(UserIdDTO => UserIdDTO.Username == username);
            userInfo.userId = foundUser.id;
            userInfo.PublisherName = foundUser.Username;
            return userInfo;
        }
    }
}
