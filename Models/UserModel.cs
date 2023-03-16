using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blogbackend.Models
{
    public class UserModel
    {

        public int id { get; set; }     
        
        public string? Username { get; set; }

        public string? Salt { get; set; }

        public string? Hash { get; set; }

        public UserModel() {}
    }
}