using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NNI.PayerPortal.WebUI.Infrastructure.Abstract;
using System.Web.Security;

namespace NNI.PayerPortal.WebUI.Infrastructure.Concrete
{
    public class MembershipAuthProvider : IAuthProvider
    {
        public bool Authenticate(string username, string password, bool remember)
        {
            bool result = Membership.ValidateUser(username, password);
            if (result)
            {
                // Set Cookie To Remember User
                FormsAuthentication.SetAuthCookie(username, remember);
            }
            return result;
        }
    }
}