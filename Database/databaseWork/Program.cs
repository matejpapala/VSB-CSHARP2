using System.Security.Cryptography;
using Dapper;
using Microsoft.Data.Sqlite;

namespace databaseWork;



class Customer
{
    public int Id{get;set;}
    public string Name{get;set;}
    public string Address{get;set;}
}
class Program
{
    static void Main(string[] args)
    {

        SimpleCRUD.SetDialect(SimpleCRUD.Dialect.SQLite);

        string initSql = """
                    CREATE TABLE "Customer" (
                "Id"	INTEGER,
                "Name"	TEXT NOT NULL,
                "Address"	TEXT,
                PRIMARY KEY("Id" AUTOINCREMENT)
            );

            CREATE TABLE "Order" (
                "Id"	INTEGER,
                "CustomerId"	INTEGER NOT NULL,
                "Product"	TEXT NOT NULL,
                "Price"	NUMERIC NOT NULL,
                FOREIGN KEY("CustomerId") REFERENCES "Customer"("Id"),
                PRIMARY KEY("Id" AUTOINCREMENT)
            );
        """;
        string connectionString = "Data Source=mydb.db;";
        using SqliteConnection connection = new SqliteConnection(connectionString);
        connection.Open();

        // using SqliteCommand initCmd = new SqliteCommand();
        // initCmd.CommandText = initSql;
        // initCmd.Connection = connection;
        // initCmd.ExecuteNonQuery();


        using SqliteTransaction transaction = connection.BeginTransaction();


        int? id = connection.Insert(new Customer()
        {
           Name = "Robin",
           Address = "Praha"
        });

        Customer cust = connection.Get<Customer>(id);
        cust.Address = "Olomouc";
        connection.Update(cust);

        // connection.Execute(
        //     "INSERT INTO Customer (Name, Address) VALUES (@Jmeno, @Adresa);",
        //     new {Jmeno = "Tonik", Adresa = "Olomouc"},
        //     transaction
        // );

        // string input = "Zuzana";
        // string input2 = "Praha";
        // using SqliteCommand cmd = new SqliteCommand(
        //     "INSERT INTO Customer (Name, Address) VALUES (@Jmeno, @Adresa);",
        //     connection,
        //     transaction
        // );
        // cmd.Parameters.AddWithValue("Jmeno", input);
        // // cmd.Parameters.AddWithValue("Adresa", input2);
        // cmd.Parameters.Add(new SqliteParameter()
        // {
        //    ParameterName = "Adresa",
        //    Value = DBNull.Value,
        //    DbType = System.Data.DbType.AnsiString 
        // });
        // cmd.ExecuteNonQuery();


        foreach(Customer c in connection.Query<Customer>("select * from Customer", null, transaction))
        {
            Console.WriteLine($"{c.Id} {c.Name} {c.Address}");
        }

        // foreach(Customer c in connection.Query<Customer>("select * from Customer", null, transaction))
        // {
        //     Console.WriteLine($"{c.Id} {c.Name} {c.Address}");
        // }
        
        // using SqliteCommand selectCommand = new SqliteCommand(
        //     "SELECT * FROM Customer;",
        //     connection,
        //     transaction
        // );


        // using SqliteDataReader reader = selectCommand.ExecuteReader();
        // while(reader.Read())
        // {
        //     int id = reader.GetInt32(reader.GetOrdinal("Id"));
        //     string name = reader.GetString(reader.GetOrdinal("Name"));
        //     string address = null;
        //     if(!reader.IsDBNull(reader.GetOrdinal("Address"))){
        //         address = reader.GetString(reader.GetOrdinal("Address"));
        //     }
        //     Console.WriteLine($"{id} {name} {address}");
        // }


        long count = connection.RecordCount<Customer>();
        // long count = connection.ExecuteScalar<long>("select count(*) from Customer", null, transaction);
        // Console.WriteLine("Pocet zaznamu: " + count);

        // using SqliteCommand countCmd = new SqliteCommand(
        //     "select count(*) from Customer",
        //     connection,
        //     transaction
        // );
        // long count = (long)countCmd.ExecuteScalar();
        // Console.WriteLine("Pocet zaznamu: " + count);


        transaction.Commit();
        // connection.Close();
        // connection.Dispose();
    }
}
