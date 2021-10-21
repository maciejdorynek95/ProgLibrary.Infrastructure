using ProgLibrary.Infrastructure.Exceptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProgLibrary.Infrastructure.Commands
{
    public class Register
    {

        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [StringLength(100, ErrorMessage = "hasło powinno posiadać długośc od 14 do 100 znaków", MinimumLength = 14)]
        public string Password { get; set; }

        public Register(string name, string email, string password)
        {
            SetName(name);
            SetEmail(email);
            SetPassword(password);
        }
        private void SetName(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Nie podano nazwy użytkownika");
            }

            Name = name;
        }
        private void SetEmail(string email)
        {
            if (String.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Nie podano e-mail");
            }
            Email = email;
        }
        public virtual void SetPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 14)
            {
                
                throw new PasswordException("błędnie wprowadzone hasło, powinno mieć przynajmniej jedną dużą literę, cyfrę i znak specjalny");
            }
            Password = password;
        }






    }




}
