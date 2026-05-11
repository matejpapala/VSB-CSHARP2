using Dapper;
using evidenceFirem.Models;
using Microsoft.Data.Sqlite;

namespace evidenceFirem.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString = "Data Source=db.db";

        public void CreateDatabase()
        {
            using(var connection = new SqliteConnection(_connectionString))
            {
                    connection.Execute(@"CREATE TABLE IF NOT EXISTS Firmy (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Nazev TEXT NOT NULL,
                        Dic TEXT NOT NULL,
                        PocetZamestnancu INTEGER NOT NULL,
                        PravniForma INTEGER NOT NULL,
                        Poznamka TEXT,
    
                        -- Databázový constraint pro validaci počtu zaměstnanců
                        CONSTRAINT CHK_PocetZamestnancu CHECK (PocetZamestnancu >= 1)
                    )");

                connection.Execute(@"INSERT INTO Firmy (Nazev, Dic, PocetZamestnancu, PravniForma, Poznamka)
                    VALUES 
                        ('První testovací s.r.o.', 'CZ12345678', 15, 0, 'Toto je první zkušební firma v databázi.'),
    
                        ('Velká Korporace a.s.', 'CZ87654321', 1250, 1, NULL),
    
                        ('Jan Novák - IT Služby', 'CZ8501011234', 1, 2, 'OSVČ pracující jako externí kontraktor.'),
    
                        ('Stavební podnik k.s.', 'CZ11223344', 42, 3, 'Firma bez speciální poznámky, test pro k.s.'),
    
                        ('Inovativní Startup s.r.o.', 'CZ99887766', 5, 0, 'Založeno v minulém roce, rychlý růst.')");
            }
        }

        public IEnumerable<DatabaseModel> SelectCompanies()
        {
            using(var connection = new SqliteConnection(_connectionString))
            {
                var data = connection.Query<DatabaseModel>(@"SELECT * FROM Firmy");
                return data;
            }
        }

        public void DeleteCompany(int companyId)
        {
            using(var connection = new SqliteConnection(_connectionString))
            {
                connection.Execute(@"
                DELETE FROM Firmy WHERE Id = @Id", new
                {
                                       Id = companyId,
                });
            }
        }

        public void AddCompany(PridavaniViewModel model)
        {
            using(var connection = new SqliteConnection(_connectionString))
            {
                connection.Execute(@"INSERT INTO Firmy (Nazev, Dic, PocetZamestnancu, PravniForma, Poznamka) VALUES (@Nazev, @Dic, @PocetZamestnancu, @PravniForma, @Poznamka)", new
                {
                    Nazev = model.Name,
                    Dic = model.Dic,
                    PocetZamestnancu = model.PocetZamestnancu,
                    PravniForma = model.PravniForma,
                    Poznamka = model.Poznamka
                });
            }
        }

    }
}
