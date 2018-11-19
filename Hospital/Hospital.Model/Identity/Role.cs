using System.Collections.Generic;
using System.Linq;

namespace Hospital.Model.Identity
{
    public static class Role
    {
        public static string Admin = "Admin";
        public static string Doctor = "Doctor";
        public static string Patient = "Patient";

        public static bool Exists(string roleName)
        {
            var list = new List<string>() { Admin, Doctor, Patient };

            return list.Any(role => role == roleName);
        }
    }
}