using NovaScotia.Models;
using System;
using System.Security.Claims;

namespace NovaScotia.Controllers
{
    public class ScotiaCustomer<T>
    {
        internal ScotiaCustomer FindByIdAsync(string userid)
        {
            throw new NotImplementedException();
        }

        internal string GetUserId(ClaimsPrincipal user)
        {
            throw new NotImplementedException();
        }
    }
}