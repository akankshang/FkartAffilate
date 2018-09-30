using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace OldAPI.Models
{
    public class GNF
    {
        public static string RandomPassword(int length)
        {
            return Membership.GeneratePassword(length, 0);
        }

    }
}