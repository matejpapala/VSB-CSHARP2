using Dapper;
using Microsoft.Data.Sqlite;
using variantaF.Models;

namespace variantaF.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString = "Data Source=db.db";
        
        public void SaveToDatabase(FormViewModel formModel)
        {
            using (var connection = new SqliteConnection(_connectionString))
            {
                string sql = @"INSERT INTO Orders (Date, AltId, Name) VALUES (@Date, @AltId, @Name)";
                connection.Execute(sql, new
                {
                                       Date = formModel.Date,
                                       AltId = formModel.AltId,
                                       Name = formModel.Name,
                });
            }
        }
    }
}
