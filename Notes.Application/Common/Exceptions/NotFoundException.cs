using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Common.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string name,object key) : base($"Entity /{name}/ {key} not found") { }
    }
}
