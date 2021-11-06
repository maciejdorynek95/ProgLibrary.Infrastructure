using ProgLibrary.Infrastructure.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProgLibrary.Infrastructure.Commands.User
{
    public class Register
    {
        public string Email { get; set; }
        public string Password { get; set; }

    }

}
