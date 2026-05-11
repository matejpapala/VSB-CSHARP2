using Dapper;
using Microsoft.Data.Sqlite;
using System.Configuration;
using System.Data;
using System.Security.Cryptography;
using System.Windows;

namespace firmyWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            SQLitePCL.Batteries_V2.Init();
            EnsureDatabase();
        }

        private void EnsureDatabase()
        {
            using (var connection = new SqliteConnection("Data Source=db.db"))
            {
                connection.Execute(@"CREATE TABLE IF NOT EXISTS Company (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Nazev TEXT NOT NULL,
                    Dic TEXT,
                    Obec TEXT NOT NULL,
                    Poznamka TEXT
                )");
            }
        }
    }

}
