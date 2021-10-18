using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Commands.Roles
{
    public class DeleteRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
