using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace Book_Hopper.Repositories
{
    public static class DbConnector
    {
        private static readonly string ConnectionString = "User ID=postgres;Password=postgres;Host=localhost;Port=5432;Database=postgres;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";

        public static NpgsqlConnection Connect()
        {
            var connection = new NpgsqlConnection(ConnectionString);
            connection.Open();
            return connection;
        }
    }
}
