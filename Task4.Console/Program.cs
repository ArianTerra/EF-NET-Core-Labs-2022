using System.Data;
using Microsoft.Data.SqlClient;

using var conn = new SqlConnection("Server=(localdb)\\MSSQLLocalDB;DataBase=Task4.SQL;");
conn.Open();

// PrimeNumbers call example
Console.WriteLine("PRIMES: ");
var cmd  = new SqlCommand("PrimeNumbers", conn);
cmd.CommandType = CommandType.StoredProcedure;
cmd.Parameters.Add(new SqlParameter("@PrintUntil", 50));

using (var rdr = cmd.ExecuteReader()) {
    while (rdr.Read())
    {
        Console.WriteLine($"Number: {rdr["NUM"]}");
    }
}

// ShopItemsInfo example. It reads data from TSQL PRINT statement.
// It also can catch SQL exception (RAISERROR)
Console.WriteLine("READING PRINT DATA: ");
cmd  = new SqlCommand("ShopItemsInfo", conn);
cmd.CommandType = CommandType.StoredProcedure;

conn.InfoMessage += (sender, eventArgs) => Console.WriteLine(eventArgs.Message);

try
{
    cmd.ExecuteNonQuery();
}
catch (Exception e)
{
    Console.WriteLine("Exception happened!");
}