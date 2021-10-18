using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Commands
{
    public class Register
    {
        public Guid UserId { get; set; }
        public string Name { get;  set; }
        public string Email { get;  set; }
        public string Password { get;  set; }
        public string RoleName { get; set; }
    }
}
