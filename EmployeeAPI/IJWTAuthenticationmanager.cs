using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI
{   
    public interface IJWTAuthenticationmanager
    {
       string  Authenticate(string userName, string password);
    }
}
