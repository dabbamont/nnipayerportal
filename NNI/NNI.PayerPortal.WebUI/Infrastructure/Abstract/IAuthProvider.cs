using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NNI.PayerPortal.WebUI.Infrastructure.Abstract
{
    public interface IAuthProvider
    {
        bool Authenticate(string username, string password, bool remember);
    }
}
