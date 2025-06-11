using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.ICurrentUserContext
{
    public interface ICurrentUserContext
    {
        string IpAddress { get; }
        string UserAgent { get; }
    }
}
