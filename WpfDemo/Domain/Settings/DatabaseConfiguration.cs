using WpfDemo.Domain.Settings.Attributes;

namespace WpfDemo.Domain.Settings
{
    public class DatabaseSettings
    {
        [ConfigFileParameter("Database")]
        public string Database { get; set; }

        [ConfigFileParameter("DatabaseUser")]
        [ParameterInformation("User", Description = "")]
        public string User { get; set; }

        [ConfigFileParameter("DatabasePassword")]
        [ParameterInformation("Password", Description = "Database password, use very secure format plox!")]
        public string Password { get; set; }
    }
}
