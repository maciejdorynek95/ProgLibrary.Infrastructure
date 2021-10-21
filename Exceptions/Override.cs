using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgLibrary.Infrastructure.Exceptions
{

    public class Override : Exception
    {

        public override string StackTrace => null;

        public Override(string message) : base(message)
        {

        }
    }
}
