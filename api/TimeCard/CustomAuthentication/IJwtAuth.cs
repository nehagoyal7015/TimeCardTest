using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TimeCard.CustomAuthentication
{
    public interface IJwtAuth
    {
        string Authentication(string userEmail, string password);
    }
}
    