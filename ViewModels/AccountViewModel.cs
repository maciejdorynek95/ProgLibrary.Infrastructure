using System;
using System.ComponentModel.DataAnnotations;

namespace ProgLibrary.Infrastructure.ViewModels
{
    public class AccountViewModel
    {
        
        public Guid Id { get; set; }
        [EmailAddress]
        public string Email { get; set; }


    }
}
