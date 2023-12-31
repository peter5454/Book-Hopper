﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Book_Hopper.Model
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Fname { get; set; }
        public string LName { get; set; }

        private SecureString _password;

        public void SetSecurePassword(SecureString password)
        {
            _password = password;
        }

        internal SecureString GetSecurePassword()
        {
            return _password;
        }
    }

}