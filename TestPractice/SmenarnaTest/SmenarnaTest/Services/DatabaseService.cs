using Dapper;
using Microsoft.Data.Sqlite;
using SmenarnaTest.Models;

namespace SmenarnaTest.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString = "Data Source=exchange.db";

        public void UlozSmenu(ExchangeFormModel data, double vyslednaCastka)
        {
            using(var connection = new SqliteConnection(_connectionString))
            {
                connection.Execute(@"
                 CREATE TABLE IF NOT EXISTS Exchange (
                 id INTEGER PRIMARY KEY AUTOINCREMENT,
                 Jmeno TEXT,
                 Email TEXT,
                 ZdrojovaMena TEXT,
                 Castka REAL,
                 VyslednaCastka REAL)
                 ");

                connection.Execute(@"
                 INSERT INTO Exchange (Jmeno, Email, ZdrojovaMena, Castka, VyslednaCastka) VALUES (@Jmeno, @Email, @ZdrojovaMena, @Castka, @VyslednaCastka)
                 ", new {
                                       Jmeno = data.Name,
                                       Email = data.Email,
                                       ZdrojovaMena = data.Currency,
                                       Castka = data.Value,
                                       VyslednaCastka = vyslednaCastka
                });
            }
        }

        public List<ExchangeRecord> VypisSmeny()
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                return connection.Query<ExchangeRecord>("SELECT * FROM Exchange").ToList();
            }
        }
    }
}
