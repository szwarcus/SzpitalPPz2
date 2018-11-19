using System.Linq;
using System.Collections.Generic;

namespace Hospital.Model.Identity
{
    public static class Role
    {
        private static List<string> _roles = new List<string>() { Admin, Doctor, Patient };

        public static string Admin = "Admin";
        public static string Doctor = "Doctor";
        public static string Patient = "Patient";
        
        public static bool Exists(string roleName)
        {
            return _roles.Any(role => role == roleName);
        }
    }
}