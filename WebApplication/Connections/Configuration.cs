using System.Configuration;


namespace WebApplication.Connections
{
    public class ConnectionName
    {
        public string _connection;

        public string connection
        {
            get { return _connection; }
            set { _connection = value; }
        }
    }
    public class ConfigurationUSERMANAGEMENT
    {
        private string connectionString;

        public ConfigurationUSERMANAGEMENT()
        {
            this.setConnectionString("USERMANAGEMENTContext");
            this.connectionString = this.getConnectionString();
        }

        public void setConnectionString(string ConnectionName)
        {
            if (string.IsNullOrEmpty(ConnectionName)) ConnectionName = "USERMANAGEMENTContext";
            this.connectionString = ConfigurationManager.ConnectionStrings[ConnectionName].ToString();
        }

        public string getConnectionString()
        {
            return this.connectionString;

        }
    }
}