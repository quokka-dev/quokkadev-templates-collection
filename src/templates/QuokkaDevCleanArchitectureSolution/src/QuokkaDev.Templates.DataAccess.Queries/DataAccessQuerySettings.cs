using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuokkaDev.Templates.DataAccess.Queries
{
    public class DataAccessQuerySettings
    {
        public string ConnectionString { get; } = "";

        public DataAccessQuerySettings(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}
