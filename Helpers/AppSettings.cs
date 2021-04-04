using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CondorHacks.Entities;

namespace CondorHacks.Helpers
{
    public class AppSettings
    {
        public string Secret { get; set; }
    }
    public static class ExtensionMethods
    {
        public static Admin WithoutPassword(this Admin user)
        {
            if (user == null) return null;
            user.Password = null;
            return user;
        }

        public static Users WithoutPassword(this Users user)
        {
            if (user == null) return null;

            user.Password = null;
            return user;
        }
    }
}
