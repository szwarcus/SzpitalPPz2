using Microsoft.Extensions.Configuration;

namespace Hospital.Helpers
{
    public static class ProjectConfigurationHelper
    {
        public static string GetConnectionString(IConfiguration configuration)
        {
            string result = "";

#if Release
            result = configuration.GetConnectionString("DefaultDbConnectionString");
#endif

#if Debug
            result = configuration.GetConnectionString("DefaultDbConnectionString");
#endif

#if KarolDebug
            result = configuration.GetConnectionString("KarolDefaultDbConnectionString");
#endif

#if DawidDebug
            result = configuration.GetConnectionString("DawidDefaultDbConnectionString");
#endif

#if JakubDebug
            result = configuration.GetConnectionString("JakubDefaultDbConnectionString");
#endif

            return result;
        }
    }
}